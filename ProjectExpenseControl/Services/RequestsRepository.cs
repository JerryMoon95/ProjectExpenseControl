using ProjectExpenseControl.DataAccess;
using ProjectExpenseControl.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProjectExpenseControl.Services
{
    public class RequestsRepository
    {
        public List<Request> GetAll()
        {
            using (AuthenticationDB db = new AuthenticationDB())
            {
                return db.Requests.ToList();
            }
        }

        public Boolean Create(Request model)
        {
            if (model != null)
            {
                using (AuthenticationDB db = new AuthenticationDB())
                {
                    db.Requests.Add(model);
                    return (db.SaveChanges() > 0) ? true : false;
                }
            }
            return false;
        }

        public Request GetOne(int? id)
        {
            if (id != null)
            {
                using (AuthenticationDB db = new AuthenticationDB())
                {
                    Request Request = db.Requests.Find(id);
                    if (Request != null)
                        return Request;
                }
            }
            return null;
        }

        public Boolean Update(Request model)
        {
            if (model != null)
            {
                using (AuthenticationDB db = new AuthenticationDB())
                {
                    db.Requests.Attach(model);
                    //TODO: Ver cuales son los que se tendrán que estar actualizando
                    //db.Entry(model).Property(ob => ob.STA_DES_STATUS).IsModified = true;
                    return (db.SaveChanges() > 0) ? true : false;
                }
            }
            return false;
        }

        public Boolean Delete(int? id)
        {
            if (id != null)
            {
                Request model = GetOne(id);
                if (model != null)
                {
                    using (AuthenticationDB db = new AuthenticationDB())
                    {
                        db.Requests.Attach(model);
                        db.Entry(model).State = System.Data.Entity.EntityState.Deleted;
                        return (db.SaveChanges() > 0) ? true : false; ;
                    }
                }
            }
            return false;
        }
    }
}