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
    public class RequestsController : Controller
    {
        private RequestsRepository db;
        public RequestsController()
        {
            db = new RequestsRepository();
        }

        // GET: Requests
        public ActionResult Index()
        {
            return View(db.GetAll());
        }

        // GET: Requests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.GetOne(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // GET: Requests/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Requests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "REQ_IDE_REQUEST,REQ_IDE_USER,REQ_IDE_AREA,REQ_DES_TYPE_GASTO,REQ_DES_CONCEPT,REQ_DES_QUANTITY,REQ_DES_OBSERVATIONS,REQ_IDE_STATUS_APROV,REQ_FH_CREATED")] Request request)
        {
            if (ModelState.IsValid)
            {
                request.REQ_FH_CREATED = DateTime.Now;
                if (db.Create(request))
                    return RedirectToAction("Index");
            }

            return View(request);
        }

        // GET: Requests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.GetOne(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // POST: Requests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "REQ_IDE_REQUEST,REQ_IDE_USER,REQ_IDE_AREA,REQ_DES_TYPE_GASTO,REQ_DES_CONCEPT,REQ_DES_QUANTITY,REQ_DES_OBSERVATIONS,REQ_IDE_STATUS_APROV,REQ_FH_CREATED")] Request request)
        {
            if (ModelState.IsValid)
            {
                if(db.Update(request))
                    return RedirectToAction("Index");
            }
            return View(request);
        }

        // GET: Requests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = db.GetOne(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // POST: Requests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if(db.Delete(id))
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
