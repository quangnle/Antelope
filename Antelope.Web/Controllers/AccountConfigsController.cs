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
    public class AccountConfigsController : Controller
    {
        private MainModel db = new MainModel();

        // GET: AccountConfigs
        public ActionResult Index()
        {
            return View(db.AccountConfigs.ToList());
        }

        // GET: AccountConfigs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountConfig accountConfig = db.AccountConfigs.Find(id);
            if (accountConfig == null)
            {
                return HttpNotFound();
            }
            return View(accountConfig);
        }

        // GET: AccountConfigs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountConfigs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdAccount,MinRemain,NotifyThreshold,StartEffectiveDate,EndEffectiveDate,AutoActionThreshold,NumberOfRetries,MonitorPeriod")] AccountConfig accountConfig)
        {
            if (ModelState.IsValid)
            {
                db.AccountConfigs.Add(accountConfig);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(accountConfig);
        }

        // GET: AccountConfigs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountConfig accountConfig = db.AccountConfigs.Find(id);
            if (accountConfig == null)
            {
                return HttpNotFound();
            }
            return View(accountConfig);
        }

        // POST: AccountConfigs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdAccount,MinRemain,NotifyThreshold,StartEffectiveDate,EndEffectiveDate,AutoActionThreshold,NumberOfRetries,MonitorPeriod")] AccountConfig accountConfig)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accountConfig).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(accountConfig);
        }

        // GET: AccountConfigs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountConfig accountConfig = db.AccountConfigs.Find(id);
            if (accountConfig == null)
            {
                return HttpNotFound();
            }
            return View(accountConfig);
        }

        // POST: AccountConfigs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccountConfig accountConfig = db.AccountConfigs.Find(id);
            db.AccountConfigs.Remove(accountConfig);
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
