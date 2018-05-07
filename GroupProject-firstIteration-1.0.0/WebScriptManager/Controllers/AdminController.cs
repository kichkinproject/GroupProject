using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebScriptManager.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return RedirectToAction("Logout");
        }

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
                return View(admin);
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
    }
}