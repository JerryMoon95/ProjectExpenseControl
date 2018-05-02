using ProjectExpenseControl.DataAccess;
using ProjectExpenseControl.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProjectExpenseControl.Services
{
    public class InvoicesRepository
    {
        public List<Invoice> GetAll()
        {
            using (AuthenticationDB db = new AuthenticationDB())
            {
                return db.Invoices.ToList();
            }
        }

        public Boolean Create(Invoice model)
        {
            if (model != null)
            {
                using (AuthenticationDB db = new AuthenticationDB())
                {
                    db.Invoices.Add(model);
                    return (db.SaveChanges() > 0) ? true : false;
                }
            }
            return false;
        }

        public Invoice GetOne(string id)
        {
            if (id != null)
            {
                using (AuthenticationDB db = new AuthenticationDB())
                {
                    Invoice Invoice = db.Invoices.Find(id);
                    if (Invoice != null)
                        return Invoice;
                }
            }
            return null;
        }

        public Boolean Update(Invoice model)
        {
            if (model != null)
            {
                using (AuthenticationDB db = new AuthenticationDB())
                {
                    db.Invoices.Attach(model);
                    //TODO: Ver cuales son los que se tendrán que estar actualizando
                    //db.Entry(model).Property(ob => ob.STA_DES_STATUS).IsModified = true;
                    return (db.SaveChanges() > 0) ? true : false;
                }
            }
            return false;
        }

        public Boolean Delete(string id)
        {
            if (id != null)
            {
                Invoice model = GetOne(id);
                if (model != null)
                {
                    using (AuthenticationDB db = new AuthenticationDB())
                    {
                        db.Invoices.Attach(model);
                        db.Entry(model).State = System.Data.Entity.EntityState.Deleted;
                        return (db.SaveChanges() > 0) ? true : false; ;
                    }
                }
            }
            return false;
        }
    }
}