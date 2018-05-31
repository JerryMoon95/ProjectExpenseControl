using ProjectExpenseControl.DataAccess;
using ProjectExpenseControl.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

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

        public int Create(Request model)
        {
            if (model != null)
            {
                using (AuthenticationDB db = new AuthenticationDB())
                {
                    db.Requests.Add(model);
                    db.SaveChanges();
                    //(db.SaveChanges() > 0) ? true : false;
                    return model.REQ_IDE_REQUEST;
                }
            }
            return 0;
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

        public bool Approve(int? id, int? type)
        {
            if (id != null && type != null)
            {
                using (AuthenticationDB db = new AuthenticationDB())
                {
                    var req = db.Requests.Find(id);
                    if(req != null)
                    {
                        switch (type)
                        { 
                            case 1:
                            req.REQ_IDE_STATUS_APROV = 7;
                                break;
                            case 2:
                                req.REQ_IDE_STATUS_APROV = 11;
                                break;
                            default:
                                return false;
                        }
                        return (db.SaveChanges() > 0) ? true : false; 

                    }
                }
            }
            return false;
        }

        public bool Reject(int? id, int? type)
        {
            if (id != null && type != null)
            {
                using (AuthenticationDB db = new AuthenticationDB())
                {
                    var req = db.Requests.Find(id);
                    if (req != null)
                    {
                        switch (type)
                        {
                            case 1:
                                req.REQ_IDE_STATUS_APROV = 6;
                                break;
                            case 2:
                                req.REQ_IDE_STATUS_APROV = 10;
                                break;
                            default:
                                return false;
                        }
                        return (db.SaveChanges() > 0) ? true : false;

                    }
                }
            }
            return false;
        }

        public bool ApproveCXP(int? id)
        {
            if (id != null)
            {
                using (AuthenticationDB db = new AuthenticationDB())
                {
                    var req = db.Requests.Find(id);
                    if (req != null)
                    {
                        req.REQ_IDE_STATUS_APROV = 13;
                        return (db.SaveChanges() > 0) ? true : false;

                    }
                }
            }
            return false;
        }

        public bool RejectCXP(int? id)
        {
            if (id != null)
            {
                using (AuthenticationDB db = new AuthenticationDB())
                {
                    var req = db.Requests.Find(id);
                    if (req != null)
                    {
                        req.REQ_IDE_STATUS_APROV = 12;
                        return (db.SaveChanges() > 0) ? true : false;

                    }
                }
            }
            return false;
        }
    }

}