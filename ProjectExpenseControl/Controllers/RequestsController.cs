using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectExpenseControl.CustomAuthentication;
using ProjectExpenseControl.DataAccess;
using ProjectExpenseControl.Models;
using ProjectExpenseControl.Services;

namespace ProjectExpenseControl.Controllers
{
    [CustomAuthorize(Roles = "Administrador, JefeArea, Usuario, CuentasXPagar")]
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
            CustomSerializeModel user = (CustomSerializeModel)Session["user"];
            if (user != null)
            {
                int option = 0;
                
                switch (user.RoleName[0]) {
                    case "Administrador":
                        option = 1;
                        ViewBag.typeUser = option;
                        return View(db.GetAll());
                    case "Usuario":
                        option = 2;
                        break;
                    case "JefeArea":
                        option = 3;
                        break;
                    case "CuentasXPagar":
                        option = 4;
                        break;
                    default:
                        return RedirectToAction("Logout", "Account");
                }
                var requests = db.GetWithFilter(user.UserId, option);

                ViewBag.Option = option;
                return View(requests);
            }
            return RedirectToAction("Logout", "Account");
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
                var user = (CustomSerializeModel)Session["user"];

                if (user != null)
                {
                    request.REQ_FH_CREATED = DateTime.Now;
                    request.REQ_IDE_USER = user.UserId;
                    request.REQ_IDE_STATUS_APROV = 5;
                    if (db.Create(request))
                        return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Logout", "Account");
                }
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

        public ActionResult Prove(int? id)
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
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Prove([Bind(Include = "REQ_IDE_REQUEST,REQ_IDE_USER,REQ_IDE_AREA,REQ_DES_TYPE_GASTO,REQ_DES_CONCEPT,REQ_DES_QUANTITY,REQ_DES_OBSERVATIONS,REQ_IDE_STATUS_APROV,REQ_FH_CREATED")] Request request)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (db.Update(request))
        //            return RedirectToAction("Index");
        //    }
        //    return View(request);
        //}

        [CustomAuthorize(Roles = "Administrador, JefeArea")]
        /*TYPES APPROVE
         * 1 -> Aprobar Solicitud
         * 2 -> Comprobar XML
         */
        public ActionResult Approve(int id, int type)
        {
            if (db.Approve(id, type))
                ViewBag.Msg = "Exito al aprobar";
            else
                ViewBag.Msg = "Algo ocurrió... Vuelve a intentarlo. Sino contacta a soporte";
            return View("Index");
        }

        /*TYPES REJECT
         * 1 -> rechazar Solicitud
         * 2 -> Rechazar XML
         */
        public ActionResult Reject(int id, int type)
        {

            if (db.Reject(id, type))
                ViewBag.Msg = "Exito al " + ((type == 1) ? "Rechazar la Aprobación" : "Rechazar la Comprobación");
            else
                ViewBag.Msg = "Algo ocurrió... Vuelve a intentarlo. Sino contacta a soporte";
            return View("Index");
        }


        [CustomAuthorize(Roles = "Administrador, CuentasXPagar")]
        public ActionResult ApproveCXP(int id)
        {

            if (db.ApproveCXP(id))
                ViewBag.Msg = "Exito al Rechazar la Comprobación";
            else
                ViewBag.Msg = "Algo ocurrió... Vuelve a intentarlo. Sino contacta a soporte";
            return View("Index");
        }

        public ActionResult RejectCXP(int id)
        {

            if (db.RejectCXP(id))
                ViewBag.Msg = "Exito al Rechazar la Comprobación";
            else
                ViewBag.Msg = "Algo ocurrió... Vuelve a intentarlo. Sino contacta a soporte";
            return View("Index");
        }
    }
}
