using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Antelope.Data.Models;

namespace Antelope.Web.Controllers
{
    public class GeneralConfigsController : Controller
    {
        private MainModel db = new MainModel();

        // GET: GeneralConfigs
        public ActionResult Index()
        {
            return View(db.GeneralConfigs.ToList());
        }

        // GET: GeneralConfigs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GeneralConfig generalConfig = db.GeneralConfigs.Find(id);
            if (generalConfig == null)
            {
                return HttpNotFound();
            }
            return View(generalConfig);
        }

        // GET: GeneralConfigs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GeneralConfigs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DefaultMonitorPeriod,DefaultRetryTimes")] GeneralConfig generalConfig)
        {
            if (ModelState.IsValid)
            {
                db.GeneralConfigs.Add(generalConfig);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(generalConfig);
        }

        // GET: GeneralConfigs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GeneralConfig generalConfig = db.GeneralConfigs.Find(id);
            if (generalConfig == null)
            {
                return HttpNotFound();
            }
            return View(generalConfig);
        }

        // POST: GeneralConfigs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DefaultMonitorPeriod,DefaultRetryTimes")] GeneralConfig generalConfig)
        {
            if (ModelState.IsValid)
            {
                db.Entry(generalConfig).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(generalConfig);
        }

        // GET: GeneralConfigs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GeneralConfig generalConfig = db.GeneralConfigs.Find(id);
            if (generalConfig == null)
            {
                return HttpNotFound();
            }
            return View(generalConfig);
        }

        // POST: GeneralConfigs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GeneralConfig generalConfig = db.GeneralConfigs.Find(id);
            db.GeneralConfigs.Remove(generalConfig);
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
