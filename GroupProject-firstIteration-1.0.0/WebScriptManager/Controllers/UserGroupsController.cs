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

        // GET: UserGroups
        public ActionResult Index()
        {
            return View(ContainerSingleton.UserGroupRepository.UserGroups());
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
                return View(ContainerSingleton.UserGroupRepository[(long)id]);
            }
            catch (Models.Exceptions.NoElementException e)
            {
                return new Views.Shared.HtmlExceptionView(e.Message);
            }
        }

        // GET: UserGroups/Create
        public ActionResult Create()
        {
            return View();
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
                ContainerSingleton.UserGroupRepository.AddGroup(userGroup.Name, userGroup.Licence, userGroup.Parent);
                return RedirectToAction("Index");
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
            UserGroup userGroup = ContainerSingleton.UserGroupRepository[(long)id];
            if (userGroup == null)
            {
                return HttpNotFound();
            }
            return View(userGroup);
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
                ContainerSingleton.UserGroupRepository.EditGroup(userGroup.Id, userGroup.Name, userGroup.Licence);
                return RedirectToAction("Index");
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
            var userGroup = ContainerSingleton.UserGroupRepository[(long)id];
            if (userGroup == null)
            {
                return HttpNotFound();
            }
            return View(userGroup);
        }

        // POST: UserGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            ContainerSingleton.UserGroupRepository.DeleteGroup(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {

        }
    }
}
