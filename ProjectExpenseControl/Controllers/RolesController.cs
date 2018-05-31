using ProjectExpenseControl.CustomAuthentication;
using ProjectExpenseControl.DataAccess;
using ProjectExpenseControl.Services;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ProjectExpenseControl.Controllers
{
    [CustomAuthorize(Roles = "Administrador")]
    public class RolesController : Controller
    {
        private RoleRepository _db;

        public RolesController()
        {
            _db = new RoleRepository();
        }

        // GET: Roles
        public ActionResult Index()
        {
            return View(_db.GetAll());
        }

        // GET: Roles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = _db.GetOne(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // GET: Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TUSR_IDE_RESOURCE,TUSR_DES_TYPE,TUSR_FH_CREATED")] Role role)
        {
            if (ModelState.IsValid)
            {
                role.TUSR_FH_CREATED = DateTime.Today;
                if(_db.Create(role))
                    return RedirectToAction("Index");
            }

            return View(role);
        }

        // GET: Roles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = _db.GetOne(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Roles/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TUSR_IDE_RESOURCE,TUSR_DES_TYPE,TUSR_FH_CREATED")] Role role)
        {
            if (ModelState.IsValid)
            {
                if(_db.Update(role))
                    return RedirectToAction("Index");
            }
            return View(role);
        }

        // GET: Roles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = _db.GetOne(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (_db.Delete(id))
                return RedirectToAction("Index");
            else
                return RedirectToAction("Delete/" + id);
        }

        public JsonResult GetRoles()
        {
            AuthenticationDB db = new AuthenticationDB();
            var dbResult = db.Roles.ToList();
            var roles = (from rol in dbResult
                         select new
                         {
                             rol.TUSR_IDE_RESOURCE,
                             rol.TUSR_DES_TYPE,
                             rol.TUSR_FH_CREATED
                         });
            return Json(roles, JsonRequestBehavior.AllowGet);
        }

    }
}
