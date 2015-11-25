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
    public class TargetConfigsController : Controller
    {
        private MainModel db = new MainModel();

        // GET: TargetConfigs
        public ActionResult Index()
        {
            return View(db.TargetConfigs.ToList());
        }

        // GET: TargetConfigs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TargetConfig targetConfig = db.TargetConfigs.Find(id);
            if (targetConfig == null)
            {
                return HttpNotFound();
            }
            return View(targetConfig);
        }

        // GET: TargetConfigs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TargetConfigs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdAccount,IdTarget,MaxLimit")] TargetConfig targetConfig)
        {
            if (ModelState.IsValid)
            {
                db.TargetConfigs.Add(targetConfig);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(targetConfig);
        }

        // GET: TargetConfigs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TargetConfig targetConfig = db.TargetConfigs.Find(id);
            if (targetConfig == null)
            {
                return HttpNotFound();
            }
            return View(targetConfig);
        }

        // POST: TargetConfigs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdAccount,IdTarget,MaxLimit")] TargetConfig targetConfig)
        {
            if (ModelState.IsValid)
            {
                db.Entry(targetConfig).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(targetConfig);
        }

        // GET: TargetConfigs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TargetConfig targetConfig = db.TargetConfigs.Find(id);
            if (targetConfig == null)
            {
                return HttpNotFound();
            }
            return View(targetConfig);
        }

        // POST: TargetConfigs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TargetConfig targetConfig = db.TargetConfigs.Find(id);
            db.TargetConfigs.Remove(targetConfig);
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
