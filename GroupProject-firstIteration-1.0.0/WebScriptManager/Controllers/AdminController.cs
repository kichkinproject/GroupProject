using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebScriptManager.Models;

namespace WebScriptManager.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Login()

        {
            if (Session["returnUrl"] == null)
                Session["returnUrl"] = "~/Home/Index";
            Session["userId"] = null;
            Session["role"] = null;
            return View();
        }

        [HttpPost]
        public ActionResult Login([Bind(Include = "Login,Password")]Models.ViewAdaptors.AdminLoginViewAdapter admin)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Models.ContainerSingleton.AdminRepository.IsAdmin(admin.Login, admin.Password))
                    {
                        Session["userId"] = Models.ContainerSingleton.AdminRepository[admin.Login].Id.ToString();
                        Session["role"] = "Admin";
                        Session["returnUrl"] = "~/Admin/Details";
                        Session["returnUrl"] += "/" + (Session["userId"] as string);
                        return Redirect(Session["returnUrl"] as string);
                    }
                    else
                    {
                        ModelState.AddModelError("Password", "Неверный логин или пароль");
                    }
                }
                else
                {
                    ModelState.Clear();
                    ModelState.AddModelError("Password", "Неверный логин или пароль");
                }
                if (Session["returnUrl"] == null)
                    Session["returnUrl"] = "~/Admin/Details";
                return View();
            }
            catch (Exception e)
            {
                return new Views.Shared.HtmlExceptionView(e.Message);
            }
        }

        
        public ActionResult Logout()
        {
            return RedirectToAction("Login");
        }
        public ActionResult AdminList()
        {
            try
            {
                if (Session["role"] as string != "Admin")
                    return Redirect("~/Admin/Login");
                return View(ContainerSingleton.AdminRepository.Admins);
            }
            catch (Exception e)
            {
                Session["returnUrl"] = "~/Admin/Index";
                return Redirect("~/Admin/Login");
            }
        }

        // GET: Integrators/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                var admin = ContainerSingleton.AdminRepository[(long)id];
                return View(admin);
            }
            catch (Models.Exceptions.NoElementException e)
            {
                return new Views.Shared.HtmlExceptionView(e.Message);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "Login,Mail,Password,Phone,FIO")] Models.Admin user)
        {
            if(Session["role"] as string != "Admin")
                    return Redirect("~/Admin/Login");
            try
            {

                if (ModelState.IsValid && (from c in Models.ContainerSingleton.AdminRepository.Admins where c.Login == user.Login || c.Mail == user.Mail select c).Count() == 0)
                {
                    ContainerSingleton.AdminRepository.AddAdmin(user.Login, user.Password, user.FIO, user.Phone, user.Mail);
                    ModelState.AddModelError("", "Администратор добавлен");
                    return RedirectToAction("AdminList");
                }
                else
                {
                    ModelState.AddModelError("", "Такой логин или Email уже заняты");
                    return View(user);
                }
            }
            catch (Models.Exceptions.CreatingException e)
            {
                return new Views.Shared.HtmlExceptionView(e.Message);
            }
            catch (Models.Exceptions.NoElementException e)
            {
                return new Views.Shared.HtmlExceptionView(e.Message);
            }
            catch (Exception e)
            {
                Session["returnUrl"] = "~/Integrators/Create";
                return Redirect("~/Admin/Login");
            }
        }


        // GET: Integrators/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                var admin = ContainerSingleton.AdminRepository[(long)id];
                return View(admin);
            }
            catch (Models.Exceptions.NoElementException e)
            {
                return new Views.Shared.HtmlExceptionView(e.Message);
            }
        }

        // POST: Integrators/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Login,Password,FIO,Phone,Mail")] Admin user)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(user).State = EntityState.Modified;
                ContainerSingleton.AdminRepository.EditAdmin(user.Id, user.Password, user.FIO, user.Phone, user.Mail);
                return RedirectToAction("AdminList");
            }
            return View(user);
        }
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}