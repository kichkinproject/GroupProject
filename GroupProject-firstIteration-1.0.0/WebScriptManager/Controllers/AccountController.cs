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
            return RedirectToAction("Login");
        }
        public ActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterUser(Models.User user)
        {
            try
            {
                if (HttpContext.Request.Cookies["roles"].Value =="Admin")
                    RedirectToAction("RegisterIntegrator");
                if (HttpContext.Request.Cookies["roles"].Value != "Integrator")
                    return new Views.Shared.HtmlExceptionView("Пользователи не могут регистрировать других пользователей");
            }
            catch (Exception e)
            {
                RedirectToAction("Login");
            }
            try
            {
                if (ModelState.IsValid && (from c in Models.ContainerSingleton.UserRepository.Users where c.Login == user.Login || c.Mail == user.Mail select c).Count() == 0)
                {
                    Models.ContainerSingleton.UserRepository.AddUser(user.Login, user.Password, user.FIO, user.UserType, user.UserGroup, user.Phone, user.Mail);
                   // HttpContext.Response.Cookies["userId"].Value = (from c in Models.ContainerSingleton.UserRepository.Users where c.Login == user.Login select c).First().Id.ToString();
                    //HttpContext.Response.Cookies["roles"].Value = "Integrator";
                    return RedirectToAction("RegisterUser");
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


        public ActionResult RegisterIntegrator()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterIntegrator(Models.User user)
        {
            try
            {
                if (HttpContext.Request.Cookies["roles"].Value != "Admin")
                    RedirectToAction("RegisterUser");
            }
            catch (Exception e)
            {
                RedirectToAction("Login");
            }
            try
            {
                if (ModelState.IsValid && (from c in Models.ContainerSingleton.UserRepository.Users where c.Login == user.Login || c.Mail == user.Mail select c).Count() == 0)
                {
                    Models.ContainerSingleton.UserRepository.AddUser(user.Login, user.Password, user.FIO, Models.UserType.Integrator, user.UserGroup, user.Phone, user.Mail);
                    // HttpContext.Response.Cookies["userId"].Value = (from c in Models.ContainerSingleton.UserRepository.Users where c.Login == user.Login select c).First().Id.ToString();
                    //HttpContext.Response.Cookies["roles"].Value = "Integrator";
                    return RedirectToAction("RegisterIntegrator");
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
        public ActionResult Edit()
        {
            try
            {
                return View(Models.ContainerSingleton.UserRepository[Int64.Parse(HttpContext.Request.Cookies["userId"].Value)]);
            }
            catch (Exception e)
            {
                return RedirectToAction("Login");
            }
        }


        public ActionResult Edit(Models.User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Models.ContainerSingleton.UserRepository.EditUser(user.Id, user.Password, user.FIO, user.UserType, user.Phone, user.Mail);
                    return RedirectToAction("Index");
                }
                return View(user);
            }
            catch (Models.Exceptions.NoElementException e)
            {
                return new Views.Shared.HtmlExceptionView(e.Message);
            }
        }

        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            HttpContext.Response.Cookies["userId"].Value = null;

            HttpContext.Response.Cookies["role"].Value = null;
            
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
                        if (list.First().UserType==Models.UserType.Integrator)
                            HttpContext.Response.Cookies["role"].Value = "Integrator";
                        else
                            HttpContext.Response.Cookies["role"].Value = "simpleUser";
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
            return RedirectToAction("Login");
        }
    }
}