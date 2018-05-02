using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectExpenseControl.DataAccess;
using ProjectExpenseControl.Models;
using ProjectExpenseControl.Services;

namespace ProjectExpenseControl.Controllers
{
    public class StatusAprovsController : Controller
    {
        private StatusAprovRepository _db;
        public StatusAprovsController()
        {
            _db = new StatusAprovRepository();
        }
        // GET: StatusAprovs
        public ActionResult Index()
        {
            return View(_db.GetAll());
        }

        // GET: StatusAprovs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusAprov statusAprov = _db.GetOne(id);
            if (statusAprov == null)
            {
                return HttpNotFound();
            }
            return View(statusAprov);
        }

        // GET: StatusAprovs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StatusAprovs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "STA_IDE_STATUS_APROV,STA_DES_STATUS")] StatusAprov statusAprov)
        {
            if (ModelState.IsValid)
            {
                if(_db.Create(statusAprov))
                    return RedirectToAction("Index");
            }

            return View(statusAprov);
        }

        // GET: StatusAprovs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusAprov statusAprov = _db.GetOne(id);
            if (statusAprov == null)
            {
                return HttpNotFound();
            }
            return View(statusAprov);
        }

        // POST: StatusAprovs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "STA_IDE_STATUS_APROV,STA_DES_STATUS")] StatusAprov statusAprov)
        {
            if (ModelState.IsValid)
            {
                if(_db.Update(statusAprov))
                    return RedirectToAction("Index");
            }
            return View(statusAprov);
        }

        // GET: StatusAprovs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StatusAprov statusAprov = _db.GetOne(id);
            if (statusAprov == null)
            {
                return HttpNotFound();
            }
            return View(statusAprov);
        }

        // POST: StatusAprovs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (_db.Delete(id))
                return RedirectToAction("Index");
            else
                return RedirectToAction("Delete/"+id);
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
