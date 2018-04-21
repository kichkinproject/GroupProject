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
   
    public class ScenariosController : Controller
    {
        const string wrongDataMistackeString = "Ошибка со сценарием, убедитесь, что у вас доступные Cookies и повторите попытку";
        // GET: Scenarios
        public ActionResult Index()
        {


            try
            {
                var userId = Int64.Parse(HttpContext.Request.Cookies["userId"].Value);
                var user = ContainerSingleton.UserRepository[userId];

                return View(from c in ContainerSingleton.GetContainer().ScenarioSet where c.User == user select c);

            }
            catch (Models.Exceptions.NoElementException e)
            {
                return new Views.Shared.HtmlExceptionView(e.Message);
            }
            catch (Exception e)
            {
                return Redirect("~/Account/Index");
            }
        }

        // GET: Scenarios/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new Views.Shared.HtmlExceptionView(wrongDataMistackeString);
            }

            try
            {
                var scenario = ContainerSingleton.ScenarioRepository[(long)id];
                return View(scenario);
            }
            catch(Models.Exceptions.NoElementException e)
            {
                return new Views.Shared.HtmlExceptionView(e.Message);
            }
        }

        // GET: Scenarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Scenarios/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,ScriptFile,Access,LastUpdate")] Scenario scenario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    User user = ContainerSingleton.UserRepository[Int64.Parse(HttpContext.Request.Cookies["userId"].Value)];
                    ControlBox controlBox = ContainerSingleton.ControlBoxRepository[Int64.Parse(HttpContext.Request.Cookies["controlBoxId"].Value)];
                    ContainerSingleton.ScenarioRepository.AddScenario(scenario.Name, scenario.ScriptFile, scenario.Access, scenario.Description, scenario.ControlBoxes, _integrator: user);
                    return RedirectToAction("Index");
                }
                catch(Models.Exceptions.NoElementException e)
                {
                    return new Views.Shared.HtmlExceptionView(e.Message);
                }
                catch(Models.Exceptions.CreatingException e)
                {
                    return new Views.Shared.HtmlExceptionView(e.Message);
                }
                catch(Exception e)
                {
                    return new Views.Shared.HtmlExceptionView(wrongDataMistackeString);
                }
            }

            return View(scenario);
        }

        // GET: Scenarios/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new Views.Shared.HtmlExceptionView(wrongDataMistackeString);
            }
            try
            {
                var scenario = ContainerSingleton.ScenarioRepository[(long)id];

                return View(scenario);

            }
            catch (Models.Exceptions.NoElementException e)
            {
                return new Views.Shared.HtmlExceptionView(e.Message);
            }
        }

        // POST: Scenarios/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,ScriptFile,Access,LastUpdate")] Scenario scenario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ContainerSingleton.ScenarioRepository.EditScenario(scenario.Id, scenario.Name, scenario.ScriptFile, scenario.Access, scenario.Description);
                    return RedirectToAction("Index");
                }
                return View(scenario);
            }
            catch (Models.Exceptions.NoElementException e)
            {
                return new Views.Shared.HtmlExceptionView(e.Message);
            }
        }

        // GET: Scenarios/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new Views.Shared.HtmlExceptionView(wrongDataMistackeString);
            }
            try
            {
                var scenario = ContainerSingleton.ScenarioRepository[(long)id];
                return View(scenario);
            }
            catch (Models.Exceptions.NoElementException e)
            {
                return new Views.Shared.HtmlExceptionView(e.Message);
            }
        }

        // POST: Scenarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            try
            {
                ContainerSingleton.ScenarioRepository.DeleteScenario(id);
                return RedirectToAction("Index");
            }
            catch (Models.Exceptions.NoElementException e)
            {
                return new Views.Shared.HtmlExceptionView(e.Message);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
