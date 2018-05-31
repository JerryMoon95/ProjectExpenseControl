using ProjectExpenseControl.CustomAuthentication;
using ProjectExpenseControl.DataAccess;
using ProjectExpenseControl.Models;
using ProjectExpenseControl.Services;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProjectExpenseControl.Controllers
{
    [CustomAuthorize(Roles = "Administrador, JefeArea")]
    public class InvoicesController : Controller
    {
        private InvoicesRepository db;

        public InvoicesController()
        {
            db = new InvoicesRepository();
        }

        // GET: Invoices
        public ActionResult Index()
        {
            return View(db.GetAll());
        }

        // GET: Invoices/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.GetOne(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // GET: Invoices/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "INV_ID_INVOICE,INV_DES_SERIE,INV_DES_FOLIO,INV_FH_FECHA,INV_DES_TOTAL,INV_DES_LUGAR_EXPEDICION,INV_DES_EMISOR_RFC,INV_DES_EMISOR_NOMBRE,INV_DES_UUID")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                invoice.INV_FH_FECHA = DateTime.Now;
                if (db.Create(invoice))
                   return RedirectToAction("Index");
            }

            return View(invoice);
        }

        // GET: Invoices/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.GetOne(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "INV_ID_INVOICE,INV_DES_SERIE,INV_DES_FOLIO,INV_FH_FECHA,INV_DES_TOTAL,INV_DES_LUGAR_EXPEDICION,INV_DES_EMISOR_RFC,INV_DES_EMISOR_NOMBRE,INV_DES_UUID")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                if(db.Update(invoice))
                    return RedirectToAction("Index");
            }
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.GetOne(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
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

        public JsonResult GetInvoices()
        {
            using(AuthenticationDB db = new AuthenticationDB())
            {
                var dbResult = db.Invoices.ToList();
                var invoices = (from invoice in dbResult
                                select new
                                {
                                    invoice.INV_ID_INVOICE,
                                    invoice.INV_IDE_REQUEST,
                                    invoice.INV_DES_SERIE,
                                    invoice.INV_DES_FOLIO,
                                    invoice.INV_FH_FECHA,
                                    invoice.INV_DES_TOTAL,
                                    invoice.INV_DES_LUGAR_EXPEDICION,
                                    invoice.INV_DES_EMISOR_RFC,
                                    invoice.INV_DES_EMISOR_NOMBRE,
                                    invoice.INV_DES_UUID
                                });
                return Json(invoices, JsonRequestBehavior.AllowGet);
            }
                
        }

    }
}
