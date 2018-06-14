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
    public class IntegratorsController : Controller
    {

        private bool IsAdmin()
        {
            return Session["role"] as string == "Admin" ? true : false;
        }
        private bool IsIntegrator()
        {
            return Session["role"] as string == "Integrator" ? true : false;
        }
        private bool IsLoggedIn()
        {
            if (Session["userId"] == null)
            {
                return false;
            }
            return true;
        }
        private bool IsParent(UserGroup parent, UserGroup son)
        {
            if (son == null)
                return false;
            if (son.Id == parent.Id)
                return true;
            else
                return IsParent(parent, son.Parent);

        }
        private Int64 UserId()
        {
            return Convert.ToInt64(Session["userId"] as string);
        }

        // GET: Integrators
        public ActionResult Index()
        {
            try
            {
                if (this.IsIntegrator())
                {
                    UserGroup userGroup = ContainerSingleton.UserRepository[Int64.Parse(Session["userId"].ToString())].UserGroup;
                   
                    return View(from c in ContainerSingleton.UserRepository.AllUsers where c.UserType==UserType.Integrator&& IsParent(userGroup, c.UserGroup) select c);
                }
                return View(ContainerSingleton.UserRepository.Integrators.ToList());
            }
            catch (Exception e)
            {
                Session["returnUrl"] = "~/Integrators/Index";
                return Redirect("~/Account/Login");
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
                User user = ContainerSingleton.UserRepository[(long)id];
                return View(user);
            }
            catch (Models.Exceptions.NoElementException e)
            {
                return new Views.Shared.HtmlExceptionView(e.Message);
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            List<SelectListItem> groupList = new List<SelectListItem>();
            if (this.IsIntegrator())
            {
                User integr = ContainerSingleton.UserRepository[this.UserId()];
                //groupList.Add(new SelectListItem { Text = "В текущую группу", Value = "0" });
                List<UserGroup> groups = (from c in ContainerSingleton.UserGroupRepository.UserGroups() where IsParent(integr.UserGroup, c) select c).ToList();
                //int i = 1;
                foreach(var gr in groups)
                {
                    groupList.Add(new SelectListItem { Text = gr.Name, Value = gr.Id.ToString() });
                    //i++;
                }
            }
            ViewData["UserGroups"] = groupList;
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "Login,Mail,Password,Phone,FIO")] User user, string userGroups)
        {
            //user.UserGroup = new UserGroup();
            try
            {
                if (ModelState.IsValid && (from c in Models.ContainerSingleton.UserRepository.AllUsers where c.Login == user.Login || c.Mail == user.Mail select c).Count() == 0)
                {
                    if (this.IsIntegrator())
                    {
                        User integr = ContainerSingleton.UserRepository[this.UserId()];
                        switch (userGroups)
                        {
                            case "0":
                                ContainerSingleton.UserRepository.AddUser(user.Login, user.Password, user.FIO, UserType.Integrator, integr.UserGroup, user.Phone, user.Mail);
                                break;
                            default:
                                //int i = 1; 
                                ContainerSingleton.UserRepository.AddUser(user.Login, user.Password, user.FIO, UserType.Integrator, ContainerSingleton.UserGroupRepository[Int64.Parse(userGroups)], user.Phone, user.Mail);
                                break;
                        }
                        ModelState.AddModelError("", "Интегратор добавлен");
                        return Redirect("~/Integrators/Index");
                    }
                    user.UserGroup = new UserGroup();
                    var userGroup = Models.ContainerSingleton.UserGroupRepository.AddGroup(user.Login + " group", Licence.None);
                    Models.ContainerSingleton.UserRepository.AddUser(user.Login, user.Password, user.FIO, UserType.Integrator, userGroup, user.Phone, user.Mail);
                    // HttpContext.Response.Cookies["userId"].Value = (from c in Models.ContainerSingleton.UserRepository.Users where c.Login == user.Login select c).First().Id.ToString();
                    //HttpContext.Response.Cookies["roles"].Value = "Integrator";
                    ModelState.AddModelError("", "Интегратор добавлен");
                    return Redirect("~/Integrators/Index");
                }
                else
                {
                    ModelState.AddModelError("", "Такой логин или Email уже заняты");
                    List<SelectListItem> groupList = new List<SelectListItem>();
                    if (this.IsIntegrator())
                    {
                        User integr = ContainerSingleton.UserRepository[this.UserId()];
                        groupList.Add(new SelectListItem { Text = "В текущую группу", Value = "0" });
                        List<UserGroup> groups = (from c in ContainerSingleton.UserGroupRepository.UserGroups() where IsParent(integr.UserGroup, c) select c).ToList();
                        int i = 1;
                        foreach (var gr in groups)
                        {
                            groupList.Add(new SelectListItem { Text = gr.Name, Value = i.ToString() });
                            i++;
                        }
                    }
                    ViewData["UserGroups"] = groupList;
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
                User user = ContainerSingleton.UserRepository[(long)id];

                return View(user);
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
        public ActionResult Edit([Bind(Include = "Id,Login,Password,FIO,Phone,Mail,UserType")] User user)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(user).State = EntityState.Modified;
                ContainerSingleton.UserRepository.EditUser(user.Id, user.Password, user.FIO, UserType.Integrator, user.Phone, user.Mail);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Integrators/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                User user = ContainerSingleton.UserRepository[(long)id];
                return View(user);
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
            ContainerSingleton.UserRepository.DeleteIntegrator(id);
            return RedirectToAction("Index");
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
