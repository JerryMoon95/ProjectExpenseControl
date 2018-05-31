using ProjectExpenseControl.CustomAuthentication;
using ProjectExpenseControl.Models;
using ProjectExpenseControl.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProjectExpenseControl.Controllers
{
    [CustomAuthorize(Roles = "Administrador, JefeArea, Usuario, CuentasXPagar")]
    public class RequestsController : Controller
    {
        private RequestsRepository _db;
        private BudgetRepository _bud;
        public RequestsController()
        {
            _db = new RequestsRepository();
            _bud = new BudgetRepository();
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
                        return View(_db.GetAll());
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
                var requests = _db.GetWithFilter(user.UserId, option);

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
            Request request = _db.GetOne(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            return View(request);
        }

        // GET: Requests/Create
        public ActionResult Create()
        {
            CustomSerializeModel user = (CustomSerializeModel)Session["user"];
            if (user != null)
            { 
                ViewBag.Budget = _bud.GetAllByArea(user.IdArea);
            }

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
                    int id = _db.Create(request);
                    if (id > 0)
                    {
                        FilesRepository _repo = new FilesRepository();
                        _repo.CrearDirectorio(id.ToString(), Server.MapPath("~"));
                        //
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Budget = _bud.GetAllByArea(user.IdArea);
                        return View(request);
                    }
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
            Request request = _db.GetOne(id);
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
                if(_db.Update(request))
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
            Request request = _db.GetOne(id);
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
            if(_db.Delete(id))
                return RedirectToAction("Index");
            else
                return RedirectToAction("Delete/"+id);
        }
        
        public ActionResult Prove(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Request request = _db.GetOne(id);
            if (request == null)
            {
                return HttpNotFound();
            }
            ViewBag.solicitud = id.ToString();
            ViewBag.archivos = CargarTabla(id.ToString());
            return View(request);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Prove(HttpPostedFileBase file, string request)
        {
            if (file.ContentLength > 0)
            {
                try
                {
                    if (file.ContentType == "text/xml")
                    {
                        string _FileName = request + ".xml";
                        string _path = Path.Combine(Server.MapPath("~/Xml/") + request, _FileName);
                        if (System.IO.File.Exists(_path))
                        {
                            int i = 1;
                            while (System.IO.File.Exists(_path))
                            {
                                _path = Server.MapPath("~/Xml/") + request + @"\" + Path.GetFileNameWithoutExtension(_FileName) + i.ToString() + ".xml";
                                i++;
                            }
                            file.SaveAs(_path);
                        }
                        else
                        {
                            file.SaveAs(_path);
                        }

                        DataSet ds = new DataSet();
                        ds.ReadXml(_path);
                        Invoice invoice = new Invoice();
                        invoice.INV_IDE_REQUEST = int.Parse(request);
                        if (ds.Tables["Comprobante"].Columns.Contains("serie"))
                        {
                            invoice.INV_DES_SERIE = ds.Tables["Comprobante"].Rows[0]["serie"].ToString();
                        }
                        if (ds.Tables["Comprobante"].Columns.Contains("folio"))
                        {
                            invoice.INV_DES_FOLIO = ds.Tables["Comprobante"].Rows[0]["folio"].ToString();
                        }
                        invoice.INV_FH_FECHA = DateTime.Parse(ds.Tables["Comprobante"].Rows[0]["fecha"].ToString());
                        invoice.INV_DES_TOTAL = Decimal.Parse(ds.Tables["Comprobante"].Rows[0]["total"].ToString());
                        invoice.INV_DES_LUGAR_EXPEDICION = ds.Tables["Comprobante"].Rows[0]["LugarExpedicion"].ToString();
                        if (ds.Tables["Emisor"].Columns.Contains("nombre"))
                        {
                            invoice.INV_DES_EMISOR_NOMBRE = ds.Tables["Emisor"].Rows[0]["nombre"].ToString();
                        }

                        invoice.INV_DES_EMISOR_RFC = ds.Tables["Emisor"].Rows[0]["rfc"].ToString();
                        invoice.INV_DES_UUID = ds.Tables["TimbreFiscalDigital"].Rows[0]["uuid"].ToString();

                        InvoicesRepository _repository = new InvoicesRepository();
                        if (_repository.Create(invoice))
                        {

                        }

                        ViewBag.Message = "¡El archivo se cargó satisfactoriamente!";
                        ViewBag.archivos = CargarTabla(request);
                        ViewBag.solicitud = request;
                        return RedirectToAction("Prove/" + request);
                    }
                    else
                    {
                        ViewBag.Message = "¡El archivo no es un XML!";
                        ViewBag.archivos = CargarTabla(request);
                        ViewBag.solicitud = request;
                        return RedirectToAction("Prove/" + request);
                    }
                }
                catch (Exception e)
                {
                    e.Message.ToString();
                    ViewBag.Message = e + "¡Falló la carga del archivo!";
                    ViewBag.archivos = CargarTabla(request);
                    ViewBag.solicitud = request;
                    return RedirectToAction("Prove/" + request);
                }
            }
            else
            {
                ViewBag.Message = "No se selecciono ningun archivo.";
                ViewBag.archivos = CargarTabla(request);
                ViewBag.solicitud = request;
                return RedirectToAction("Prove/" + request);
            }
        }

        private List<string> CargarTabla(string request)
        {
            //string solictud = "01";
            FilesRepository filesManagement = new FilesRepository();
            List<string> ficheros;
            ficheros = filesManagement.ArchivosDirectorio(request, Server.MapPath("~"));
            return ficheros;
        }


        [CustomAuthorize(Roles = "Administrador, JefeArea")]
        /*TYPES APPROVE
         * 1 -> Aprobar Solicitud
         * 2 -> Comprobar XML
         */
        public ActionResult Approve(int id, int type)
        {
            if (_db.Approve(id, type))
                ViewBag.Msg = "Exito al aprobar";
            else
                ViewBag.Msg = "Algo ocurrió... Vuelve a intentarlo. Sino contacta a soporte";

            return RedirectToAction("Index");
        }

        /*TYPES REJECT
         * 1 -> rechazar Solicitud
         * 2 -> Rechazar XML
         */
        public ActionResult Reject(int id, int type)
        {

            if (_db.Reject(id, type))
                ViewBag.Msg = "Exito al " + ((type == 1) ? "Rechazar la Aprobación" : "Rechazar la Comprobación");
            else
                ViewBag.Msg = "Algo ocurrió... Vuelve a intentarlo. Sino contacta a soporte";
            return RedirectToAction("Index");
        }


        [CustomAuthorize(Roles = "Administrador, CuentasXPagar")]
        public ActionResult ApproveCXP(int id)
        {

            if (_db.ApproveCXP(id))
                ViewBag.Msg = "Exito al Rechazar la Comprobación";
            else
                ViewBag.Msg = "Algo ocurrió... Vuelve a intentarlo. Sino contacta a soporte";
            return View("Index");
        }

        public ActionResult RejectCXP(int id)
        {

            if (_db.RejectCXP(id))
                ViewBag.Msg = "Exito al Rechazar la Comprobación";
            else
                ViewBag.Msg = "Algo ocurrió... Vuelve a intentarlo. Sino contacta a soporte";
            return View("Index");
        }
    }
}
