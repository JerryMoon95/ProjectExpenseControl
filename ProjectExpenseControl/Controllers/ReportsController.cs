using ProjectExpenseControl.DataAccess;
using ProjectExpenseControl.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectExpenseControl.Controllers
{
    public class ReportsController : Controller
    {
        private AuthenticationDB db = new AuthenticationDB();

        public ActionResult ViewGetAreas()
        {
            return View();
        }

        public JsonResult GetAreas()
        {
            var dbResult = db.Areas.Select(x => new {
                ARE_IDE_AREA = x.ARE_IDE_AREA,
                ARE_DES_NAME = x.ARE_DES_NAME,
                ARE_FH_CREATED = x.ARE_FH_CREATED
            }).ToList();
            return Json(dbResult, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewGetAccountingAccounts()
        {
            return View();
        }

        public JsonResult GetAccountingAccounts()
        {
            var dbResult = db.AccountingAccounts.Select(x => new {
                ACC_IDE_ACCOUNT = x.ACC_IDE_ACCOUNT,
                ACC_DES_ACCOUNT = x.ACC_DES_ACCOUNT,
                ACC_FH_CREATED = x.ACC_FH_CREATED
            }).ToList();
            return Json(dbResult, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewGetBudgets()
        {
            return View();
        }

        public JsonResult GetBudgets()
        {
            var dbResult = db.Budgets.Select(x => new
            {
                BUD_IDE_BUDGET = x.BUD_IDE_BUDGET,
                BUD_IDE_USER = x.BUD_IDE_USER,
                BUD_IDE_ACCOUNT = x.BUD_IDE_ACCOUNT,
                BUD_IDE_AREA = x.BUD_IDE_AREA,
                BUD_DES_QUANTITY = x.BUD_DES_QUANTITY,
                BUD_DES_PERIOD = x.BUD_DES_PERIOD,
                BUD_FH_CREATED = x.BUD_FH_CREATED
            }).ToList();
            return Json(dbResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetInvoices()
        {
            var dbResult = db.Invoices.Select(x => new
            {
                INV_ID_INVOICE = x.INV_ID_INVOICE,
                INV_DES_SERIE = x.INV_DES_SERIE,
                INV_DES_FOLIO = x.INV_DES_FOLIO,
                INV_FH_FECHA = x.INV_FH_FECHA,
                INV_DES_TOTAL = x.INV_DES_TOTAL,
                INV_DES_LUGAR_EXPEDICION = x.INV_DES_LUGAR_EXPEDICION,
                INV_DES_EMISOR_RFC = x.INV_DES_EMISOR_RFC,
                INV_DES_EMISOR_NOMBRE = x.INV_DES_EMISOR_NOMBRE,
                INV_DES_UUID = x.INV_DES_UUID
            }).ToList();
            return Json(dbResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUsers()
        {
            var dbResult = db.Users.Select(x => new
            {
                USR_IDE_USER = x.USR_IDE_USER,
                USR_IDE_AREA = x.USR_IDE_AREA,
                USR_DES_POSITION = x.USR_DES_POSITION,
                USR_DES_NAME = x.USR_DES_NAME,
                USR_DES_FIRST_NAME = x.USR_DES_FIRST_NAME,
                USR_DES_LAST_NAME = x.USR_DES_LAST_NAME,
                USR_DES_PASSWORD = x.USR_DES_PASSWORD,
                USR_DES_PHONE = x.USR_DES_PHONE,
                USR_DES_EMAIL = x.USR_DES_EMAIL,
                USR_FH_CREATED = x.USR_FH_CREATED
            }).ToList();
            return Json(dbResult, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult GetUsersTmp()
        //{
        //    db.Database.SqlQuery<>
        //    return 
        //}

        public JsonResult GetRequest()
        {
            var dbResult = db.Requests.Select(x => new
            {
                REQ_IDE_REQUEST = x.REQ_IDE_REQUEST,
                REQ_IDE_USER = x.REQ_IDE_USER,
                REQ_DES_TYPE_GASTO = x.REQ_DES_TYPE_GASTO,
                REQ_DES_CONCEPT = x.REQ_DES_CONCEPT,
                REQ_DES_QUANTITY = x.REQ_DES_QUANTITY,
                REQ_DES_OBSERVATIONS = x.REQ_DES_OBSERVATIONS,
                REQ_IDE_STATUS_APROV = x.REQ_IDE_STATUS_APROV,
                REQ_FH_CREATED = x.REQ_FH_CREATED
            }).ToList();
            return Json(dbResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStatusApprov()
        {
            var dbResult = db.StatusAprovs.Select(x => new
            {
                STA_IDE_STATUS_APROV = x.STA_IDE_STATUS_APROV,
                STA_DES_STATUS = x.STA_DES_STATUS
            }).ToList();
            return Json(dbResult, JsonRequestBehavior.AllowGet);
        }
    }
}