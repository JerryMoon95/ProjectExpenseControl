using ProjectExpenseControl.DataAccess;
using ProjectExpenseControl.Models;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ProjectExpenseControl.Controllers
{
    public class AccountingAccountsController : Controller
    {
        private AuthenticationDB db = new AuthenticationDB();

        // GET: AccountingAccounts
        public ActionResult Index()
        {
            return View(db.AccountingAccounts.ToList());
        }

        // GET: AccountingAccounts/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountingAccount accountingAccount = db.AccountingAccounts.Find(id);
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
        public ActionResult Create([Bind(Include = "ACC_IDE_ACCOUNT,ACC_DES_ACCOUNT,ACC_FH_CREATED")] AccountingAccount accountingAccount)
        {
            if (ModelState.IsValid)
            {
                db.AccountingAccounts.Add(accountingAccount);
                db.SaveChanges();
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
            AccountingAccount accountingAccount = db.AccountingAccounts.Find(id);
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
                db.Entry(accountingAccount).State = EntityState.Modified;
                db.SaveChanges();
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
            AccountingAccount accountingAccount = db.AccountingAccounts.Find(id);
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
            AccountingAccount accountingAccount = db.AccountingAccounts.Find(id);
            db.AccountingAccounts.Remove(accountingAccount);
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

        public JsonResult GetAccountinAccounts()
        {
            using (AuthenticationDB db = new AuthenticationDB())
            {
                var dbResult = db.AccountingAccounts.ToList();
                var accounts = (from account in dbResult
                                select new
                                {
                                    account.ACC_IDE_ACCOUNT,
                                    account.ACC_DES_ACCOUNT,
                                    account.ACC_FH_CREATED
                                });
                return Json(accounts, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
