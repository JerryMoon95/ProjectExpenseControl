using ProjectExpenseControl.DataAccess;
using ProjectExpenseControl.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
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
                    db.Entry(model).Property(ob => ob.REQ_DES_TYPE_GASTO).IsModified = true;
                    db.Entry(model).Property(ob => ob.REQ_DES_CONCEPT).IsModified = true;
                    db.Entry(model).Property(ob => ob.REQ_DES_QUANTITY).IsModified = true;
                    db.Entry(model).Property(ob => ob.REQ_DES_OBSERVATIONS).IsModified = true;
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
            
        public List<Request> GetWithFilter(int? UserId, int? Option)
        {
            if (UserId != null && Option != null)
            {
                using (AuthenticationDB db = new AuthenticationDB())
                {

                    var rows = db.Database.SqlQuery<Request>(ResourceSQL.SP_GetRequests,
                        new SqlParameter("@IDE_USER", UserId),
                        new SqlParameter("@TYPE_USER", Option)
                    ).ToList();


                    return rows;
                }
            }
            

            return null;
        }
    }

}