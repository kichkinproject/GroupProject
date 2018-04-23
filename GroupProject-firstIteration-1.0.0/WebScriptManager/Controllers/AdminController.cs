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

        public ActionResult Login(string returnUrl)
        {

            Session["userId"] = null;
            Session["role"] = null;
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.Admin admin, string returnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Models.ContainerSingleton.AdminRepository.IsAdmin(admin.Login, admin.Password))
                    {
                        Session["userId"] = Models.ContainerSingleton.AdminRepository[admin.Login].Id.ToString();
                        Session["role"] = "Admin";
                        return Redirect(returnUrl);
                    }
                    else
                        ModelState.AddModelError("", "Неверный логин или пароль");


                }
                ViewBag.ReturnUrl = returnUrl;
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