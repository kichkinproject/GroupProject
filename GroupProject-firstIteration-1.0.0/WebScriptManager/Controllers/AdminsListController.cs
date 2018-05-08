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
    public class AdminsListController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                if(Session["role"] as string !="Admin")
                    return Redirect("~/Admin/Login");
                return View(ContainerSingleton.AdminRepository.Admins);
            }
            catch (Exception e)
            {
                Session["returnUrl"] = "~/AdminsList/Index";
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
            try
            {

                if (ModelState.IsValid && (from c in Models.ContainerSingleton.AdminRepository.Admins where c.Login == user.Login || c.Mail == user.Mail select c).Count() == 0)
                {
                   ContainerSingleton.AdminRepository.AddAdmin(user.Login, user.Password, user.FIO,   user.Phone, user.Mail);
                    // HttpContext.Response.Cookies["userId"].Value = (from c in Models.ContainerSingleton.UserRepository.Users where c.Login == user.Login select c).First().Id.ToString();
                    //HttpContext.Response.Cookies["roles"].Value = "Integrator";
                    ModelState.AddModelError("", "Администратор добавлен");
                    return View(user);
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
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Integrators/Delete/5
        /*public ActionResult Delete(long? id)
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

        // POST: Integrators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            ContainerSingleton.AdminRepository.Delete(id);
            return RedirectToAction("Index");
        }*/


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
