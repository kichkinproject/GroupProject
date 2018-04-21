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
                        HttpContext.Response.Cookies["userId"].Value = Models.ContainerSingleton.AdminRepository[admin.Login].Id.ToString();
                        HttpContext.Response.Cookies["role"].Value = "Admin";
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
            HttpContext.Response.Cookies["userId"].Value = "noId";

            return RedirectToAction("Login");
        }
    }
}