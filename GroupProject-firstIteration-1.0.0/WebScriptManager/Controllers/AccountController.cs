using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebScriptManager.Models;

namespace WebScriptManager.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Index()
        {
            if ((Session["role"] as string) == null)
                return RedirectToAction("Login");
            if ((Session["role"] as string) == "Admin" || (Session["role"] as string) == "SimpleUser")
                return RedirectToAction("Login", "Integrators");
            User integr = ContainerSingleton.UserRepository[Int64.Parse(Session["userId"] as string)];
            UserGroup group = ContainerSingleton.UserGroupRepository[integr.UserGroup.Id];
            IEnumerable<User> usersInGroup = group.Users.ToList();
            usersInGroup = usersInGroup.Intersect(ContainerSingleton.UserRepository.SimpleUsers);
            return View(usersInGroup);
        }
        public ActionResult Create()
        {
            if (Session["returnUrl"] == null)
                Session["returnUrl"] = "~/Home/Index";
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "Id,Login,Mail,Password,Phone,FIO")] Models.User user)
        {
           
            try
            {
                if ((Session["role"] as string) =="Admin")
                    RedirectToAction("RegisterIntegrator");
                if (Session["role"] as string != "Integrator")
                    return new Views.Shared.HtmlExceptionView("Пользователи не могут регистрировать других пользователей");
            }
            catch (Exception e)
            {
                RedirectToAction("Login");
            }
            try
            {
                if (ModelState.IsValid && (from c in Models.ContainerSingleton.UserRepository.AllUsers where c.Login == user.Login || c.Mail == user.Mail select c).Count() == 0)
                {

                    Models.ContainerSingleton.UserRepository.AddUser(user.Login, user.Password, user.FIO, user.UserType, user.UserGroup, user.Phone, user.Mail);
                   // HttpContext.Response.Cookies["userId"].Value = (from c in Models.ContainerSingleton.UserRepository.Users where c.Login == user.Login select c).First().Id.ToString();
                    //HttpContext.Response.Cookies["roles"].Value = "Integrator";
                    return RedirectToAction("Index");
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
                return View(Models.ContainerSingleton.UserRepository[Int64.Parse(Session["userId"] as string)]);
            }
            catch (Exception e)
            {
                return RedirectToAction("Login");
            }
        }


        public ActionResult Edit([Bind(Include = "Id,Login,Mail,Password,Phone,FIO")] Models.User user)
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

        public ActionResult Login()
        {

            if (Session["returnUrl"] == null)
                Session["returnUrl"] = "~/Home/Index";
           Session["userId"] = null;

            Session["role"] = null;
            
            return View();
        }

        [HttpPost]
        public ActionResult Login([Bind(Include = "Login,Password")] Models.ViewAdaptors.UserLoginViewAdapter user)
        {
            if (Session["returnUrl"] == null)
                Session["returnUrl"] = "~/Home/Index";
            try
            {
                if (ModelState.IsValid)
                {
                    var cur = Models.ContainerSingleton.UserRepository[user.Login];
                    if (cur.Password != user.Password)
                        ModelState.AddModelError("Password", "Неверный логин или пароль");

                    else
                    {
                        Session["userId"]= cur.Id.ToString();
                        if (cur.UserType == Models.UserType.Integrator)
                        {
                            Session["role"] = "Integrator";
                            Session["returnUrl"] = "~/Integrators/Details/";
                        }
                        else
                        {
                            Session["role"] = "SimpleUser";
                            Session["returnUrl"] = "~/Account/Details/";
                        }
                        Session["returnUrl"] += cur.Id.ToString();
                        return Redirect(Session["returnUrl"] as string);
                    }

                }
                else
                {
                    ModelState.Clear();
                    ModelState.AddModelError("Password", "Неверный логин или пароль");
                }
                return View(user);
            }
            catch (Models.Exceptions.NoElementException e)
            {
                ModelState.AddModelError("Password", "Неверный логин или пароль");
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