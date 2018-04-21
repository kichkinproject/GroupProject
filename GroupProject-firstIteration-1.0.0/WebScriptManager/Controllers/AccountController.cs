using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebScriptManager.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Models.User user)
        {
            try
            {
                if (ModelState.IsValid && (from c in Models.ContainerSingleton.UserRepository.Users where c.Login == user.Login || c.Mail == user.Mail select c).Count() == 0)
                {
                    Models.ContainerSingleton.UserRepository.AddUser(user.Login, user.Password, user.FIO, user.UserType, user.UserGroup, user.Phone, user.Mail);
                    HttpContext.Response.Cookies["userId"].Value = (from c in Models.ContainerSingleton.UserRepository.Users where c.Login == user.Login select c).First().Id.ToString();
                    HttpContext.Response.Cookies["roles"].Value = "Integrator";
                    return Redirect("~/Home/Index");
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
        }

        // GET: Account

        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.User user, string returnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var list = from c in Models.ContainerSingleton.UserRepository.Users where c.Login == user.Login select c;
                    if (list.Count() == 0 || list.First().Password != user.Password)
                        ModelState.AddModelError("", "Неверный логин или пароль");

                    else
                    {
                        HttpContext.Response.Cookies["userId"].Value = list.First().Id.ToString();
                        HttpContext.Response.Cookies["role"].Value = "Integrator";
                        return Redirect(returnUrl);
                    }

                }
                ViewBag.ReturnUrl = returnUrl;
                return View(user);
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