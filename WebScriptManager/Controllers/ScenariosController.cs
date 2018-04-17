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
        private ScriptModelContainer1 db = new ScriptModelContainer1();
        private AdminRepository adRep = AdminRepository.GetRepository();
        private ScenarioRepository scRep = ScenarioRepository.GetRepository();
        // GET: Scenarios
        public ActionResult Index()
        {
            return View(db.ScenarioSet.ToList());
        }

        // GET: Scenarios/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Scenario scenario = db.ScenarioSet.Find(id);
            if (scenario == null)
            {
                return HttpNotFound();
            }
            return View(scenario);
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
                Admin me = adRep["system_author"];
                scRep.AddScenario(scenario.Name, scenario.ScriptFile, scenario.Access, _description: scenario.Description, _admin: me);
                return RedirectToAction("Index");
            }

            return View(scenario);
        }

        // GET: Scenarios/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Scenario scenario = db.ScenarioSet.Find(id);
            if (scenario == null)
            {
                return HttpNotFound();
            }
            return View(scenario);
        }

        // POST: Scenarios/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,ScriptFile,Access,LastUpdate")] Scenario scenario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(scenario).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(scenario);
        }

        // GET: Scenarios/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Scenario scenario = db.ScenarioSet.Find(id);
            if (scenario == null)
            {
                return HttpNotFound();
            }
            return View(scenario);
        }

        // POST: Scenarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Scenario scenario = db.ScenarioSet.Find(id);
            db.ScenarioSet.Remove(scenario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
