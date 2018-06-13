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
    public class UserGroupsController : Controller
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
        // GET: UserGroups
        public ActionResult Index()
        {
            if (!IsLoggedIn())
                return Redirect("~/Account/Login");

            if (IsAdmin())
                return View(ContainerSingleton.UserGroupRepository.UserGroups());
            else if (IsIntegrator())
            {
                var integrator = ContainerSingleton.UserRepository[UserId()];
                return View(from c in ContainerSingleton.UserGroupRepository.UserGroups() where IsParent(integrator.UserGroup, c) select c);

            }
            else return new Views.Shared.HtmlExceptionView("доступно только интеграторам или администраторам");
        }

        // GET: UserGroups/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                if (!IsLoggedIn())
                    return Redirect("~/Account/Login");

                if (IsAdmin())
                    return View(ContainerSingleton.UserGroupRepository.UserGroups());
                else if (IsIntegrator())
                {
                    var integrator = ContainerSingleton.UserRepository[UserId()];
                    var userGroup = ContainerSingleton.UserGroupRepository[(long)id];
                    if (IsParent(integrator.UserGroup, userGroup))
                        return View(userGroup);
                    else
                        return new Views.Shared.HtmlExceptionView("нет доступа к данной группе");

                }
                else return new Views.Shared.HtmlExceptionView("доступно только интеграторам или администраторам");
            }
            catch (Models.Exceptions.NoElementException e)
            {
                return new Views.Shared.HtmlExceptionView(e.Message);
            }
        }

        // GET: UserGroups/Create
        public ActionResult Create()
        {
            if (!IsLoggedIn())
                return Redirect("~/Account/Login");

            if (IsAdmin())
            {
                var list = ContainerSingleton.UserGroupRepository.UserGroups().ToList();
                list.Add(null);
                return View(list);
            }
            else if (IsIntegrator())
            {
                var integrator = ContainerSingleton.UserRepository[UserId()];
                return View(from c in ContainerSingleton.UserGroupRepository.UserGroups() where IsParent(integrator.UserGroup, c) select c);

            }
            else return new Views.Shared.HtmlExceptionView("доступно только интеграторам или администраторам");
        }

        // POST: UserGroups/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Licence")] UserGroup userGroup)
        {
            if (ModelState.IsValid)
            {
                if (!IsLoggedIn())
                    return Redirect("~/Account/Login");

                if (IsAdmin())
                {
                    ContainerSingleton.UserGroupRepository.AddGroup(userGroup.Name, userGroup.Licence, userGroup.Parent);
                    return RedirectToAction("Index");
                }
                else if (IsIntegrator())
                {
                    var integrator = ContainerSingleton.UserRepository[UserId()];
                    if (IsParent(integrator.UserGroup, userGroup.Parent))
                    {
                        ContainerSingleton.UserGroupRepository.AddGroup(userGroup.Name, userGroup.Licence, userGroup.Parent);
                        return RedirectToAction("Index");
                    }
                    else
                        return new Views.Shared.HtmlExceptionView("Не возможно создать группу с таким предком");

                }
                else return new Views.Shared.HtmlExceptionView("доступно только интеграторам или администраторам");

            }

            return View(userGroup);
        }

        // GET: UserGroups/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (!IsLoggedIn())
                return Redirect("~/Account/Login");
            try
            { 
                if (IsAdmin())
                {
                    return View(ContainerSingleton.UserGroupRepository[(long)id]);
                }
                else if (IsIntegrator())
                {
                    var integrator = ContainerSingleton.UserRepository[UserId()];
                    var userGroup = ContainerSingleton.UserGroupRepository[(long)id];
                    if (IsParent(integrator.UserGroup, userGroup))
                    {
                        return View(ContainerSingleton.UserGroupRepository[(long)id]);
                    }
                    else
                        return new Views.Shared.HtmlExceptionView("Не возможно создать группу с таким предком");

                }
                else return new Views.Shared.HtmlExceptionView("доступно только интеграторам или администраторам");
            }
            catch (Models.Exceptions.NoElementException e)
            {
                return new Views.Shared.HtmlExceptionView(e.Message);
            }
        } 

        // POST: UserGroups/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Licence")] UserGroup userGroup)
        {
            if (ModelState.IsValid)
            {               
                if (!IsLoggedIn())
                    return Redirect("~/Account/Login");
                try
                {
                    if (IsAdmin())
                    {
                        ContainerSingleton.UserGroupRepository.EditGroup(userGroup.Id, userGroup.Name, userGroup.Licence);
                        return RedirectToAction("Index");
                    }
                    else if (IsIntegrator())
                    {
                        var integrator = ContainerSingleton.UserRepository[UserId()];
                        if (IsParent(integrator.UserGroup, userGroup))
                        {
                            ContainerSingleton.UserGroupRepository.EditGroup(userGroup.Id, userGroup.Name, userGroup.Licence);
                            return RedirectToAction("Index");
                        }
                        else
                            return new Views.Shared.HtmlExceptionView("Не возможно создать группу с таким предком");

                    }
                    else return new Views.Shared.HtmlExceptionView("доступно только интеграторам или администраторам");
                }
                catch (Models.Exceptions.NoElementException e)
                {
                    return new Views.Shared.HtmlExceptionView(e.Message);
                }

            }
            return View(userGroup);
        }

        // GET: UserGroups/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
                var userGroup = ContainerSingleton.UserGroupRepository[(long)id];
                if (userGroup == null)
                {
                    return HttpNotFound();
                }
                return View(userGroup);
            }
            catch (Models.Exceptions.NoElementException e) { return new Views.Shared.HtmlExceptionView(e.Message); }
        }

        // POST: UserGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            if (!IsLoggedIn())
                return Redirect("~/Account/Login");
            UserGroup userGroup;
            try
            {
                userGroup = ContainerSingleton.UserGroupRepository[id];
            }
            catch(Models.Exceptions.NoElementException e)
            {
                return new Views.Shared.HtmlExceptionView(e.Message);
            }
            try
            {
                if (IsAdmin())
                {
                    ContainerSingleton.UserGroupRepository.DeleteGroup(userGroup.Id);
                    return RedirectToAction("Index");
                }
                else if (IsIntegrator())
                {
                    var integrator = ContainerSingleton.UserRepository[UserId()];
                    if (IsParent(integrator.UserGroup, userGroup))
                    {
                        ContainerSingleton.UserGroupRepository.DeleteGroup(userGroup.Id);
                        return RedirectToAction("Index");
                    }
                    else
                        return new Views.Shared.HtmlExceptionView("Не возможно создать группу с таким предком");

                }
                else return new Views.Shared.HtmlExceptionView("доступно только интеграторам или администраторам");
            }
            catch (Models.Exceptions.NoElementException e)
            {
                return new Views.Shared.HtmlExceptionView(e.Message);
            }
        }

        protected override void Dispose(bool disposing)
        {

        }
    }
}
