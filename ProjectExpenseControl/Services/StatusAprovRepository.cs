using ProjectExpenseControl.DataAccess;
using ProjectExpenseControl.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProjectExpenseControl.Services
{
    public class StatusAprovRepository
    {
        public List<StatusAprov> GetAll()
        {
            using (AuthenticationDB db = new AuthenticationDB())
            {
                return db.StatusAprovs.ToList();
            }
        }

        public Boolean Create(StatusAprov model)
        {
            if(model != null)
            {
                using (AuthenticationDB db = new AuthenticationDB())
                {
                    db.StatusAprovs.Add(model);
                    return ( db.SaveChanges() > 0 ) ? true : false;
                }
            }
            return false;
        }

        public StatusAprov GetOne(int? id)
        {
            if(id != null)
            {
                using (AuthenticationDB db = new AuthenticationDB())
                {
                    StatusAprov StatusAprov = db.StatusAprovs.Find(id);
                    if(StatusAprov != null)
                        return StatusAprov;
                }
            }
            return null;
        }

        public Boolean Update(StatusAprov StatusAprov)
        {
            if (StatusAprov != null)
            {
                using (AuthenticationDB db = new AuthenticationDB())
                {
                    db.StatusAprovs.Attach(StatusAprov);
                    db.Entry(StatusAprov).Property(ob => ob.STA_DES_STATUS).IsModified = true;
                    return (db.SaveChanges() > 0) ? true : false;
                }
            }
            return false;
        }

        public Boolean Delete(int? id)
        {
            if(id != null)
            {
                StatusAprov StatusAprov = GetOne(id);
                if (StatusAprov != null)
                {
                    using (AuthenticationDB db = new AuthenticationDB())
                    {
                        db.StatusAprovs.Attach(StatusAprov);
                        db.Entry(StatusAprov).State = System.Data.Entity.EntityState.Deleted;
                        return (db.SaveChanges() > 0) ? true : false; ;
                    }
                }
            }
            return false;
        }
    }
}