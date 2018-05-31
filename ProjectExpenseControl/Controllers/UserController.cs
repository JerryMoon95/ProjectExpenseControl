using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectExpenseControl.CustomAuthentication;
using ProjectExpenseControl.DataAccess;
using ProjectExpenseControl.Services;

namespace ProjectExpenseControl.Controllers
{
    [CustomAuthorize(Roles = "Administrador")]
    public class UserController : Controller
    {
        private UserRepository _db;
        private AreaRepository _area;
        public UserController()
        {
            _db = new UserRepository();
            _area = new AreaRepository();
        }
        // GET: Users
        public ActionResult Index()
        {
            var model = _db.GetAll();
            return View(model);
        }
        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = _db.GetOne(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User User = _db.GetOne(id);
            if (User == null)
            {
                return HttpNotFound();
            }
            ViewBag.listaAreas = _area.GetAll();
            return View(User);
        }

        // POST: Users/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                if (_db.Update(user))
                    return RedirectToAction("Index");
            }
            ViewBag.listaAreas = _area.GetAll();
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User User = _db.GetOne(id);
            if (User == null)
            {
                return HttpNotFound();
            }
            return View(User);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (_db.Delete(id))
                return RedirectToAction("Index");
            else
                return RedirectToAction("Delete/" + id);
        }

        // GET: Users/UserRoles/5
        public ActionResult UserRoles(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User User = _db.GetOne(id);
            if (User == null)
            {
                return HttpNotFound();
            }
            return View(User);
        }

        // POST: Users/UserRoles/5
        [HttpPost, ActionName("UserRoles")]
        [ValidateAntiForgeryToken]
        public ActionResult ModifyUserRoles(int? id)
        {
            return RedirectToAction("UserRoles/" + id);
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
        public JsonResult GetUsers()
        {
            using (AuthenticationDB db = new AuthenticationDB())
            {
                var requests = db.Database.SqlQuery<QueryUsers>("SP_QueryUsers").ToList();
                return Json(requests, JsonRequestBehavior.AllowGet);
            }
        }

        private class QueryUsers
        {
            public int USR_IDE_USER { get; set; }
            public string ARE_DES_NAME { get; set; }
            public string USR_DES_POSITION { get; set; }
            public string USR_DES_NAME { get; set; }
            public string USR_DES_FIRST_NAME { get; set; }
            public string USR_DES_LAST_NAME { get; set; }
            public string USR_DES_EMAIL { get; set; }
        }
    }
}