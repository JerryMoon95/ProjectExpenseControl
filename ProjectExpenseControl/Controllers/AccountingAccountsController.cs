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
    //[Authorize]
    public class AccountingAccountsController : Controller
    {
        private AccountingAccountRepository _db;
        public AccountingAccountsController()
        {
            _db = new AccountingAccountRepository();
        }

        // GET: AccountingAccounts
        public ActionResult Index()
        {
            return View(_db.GetAll());
        }

        // GET: AccountingAccounts/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountingAccount accountingAccount = _db.GetOne(id);
            if (accountingAccount == null)
            {
                return HttpNotFound();
            }
            return View(accountingAccount);
        }

        // GET: AccountingAccounts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountingAccounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ACC_IDE_ACCOUNT,ACC_DES_ACCOUNT")] AccountingAccount accountingAccount)
        {
            if (ModelState.IsValid)
            {
                accountingAccount.ACC_FH_CREATED = DateTime.Now;
                if(_db.Create(accountingAccount))
                    return RedirectToAction("Index");
            }

            return View(accountingAccount);
        }

        // GET: AccountingAccounts/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountingAccount accountingAccount = _db.GetOne(id);
            if (accountingAccount == null)
            {
                return HttpNotFound();
            }
            return View(accountingAccount);
        }

        // POST: AccountingAccounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ACC_IDE_ACCOUNT,ACC_DES_ACCOUNT,ACC_FH_CREATED")] AccountingAccount accountingAccount)
        {
            if (ModelState.IsValid)
            {
                if(_db.Update(accountingAccount))
                    return RedirectToAction("Index");
            }
            return View(accountingAccount);
        }

        // GET: AccountingAccounts/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountingAccount accountingAccount = _db.GetOne(id);
            if (accountingAccount == null)
            {
                return HttpNotFound();
            }
            return View(accountingAccount);
        }

        // POST: AccountingAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            if(_db.Delete(id))
                return RedirectToAction("Index");
           
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
