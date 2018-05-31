using ProjectExpenseControl.DataAccess;
using ProjectExpenseControl.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectExpenseControl.Services
{
    public class BudgetRepository
    {
        public List<Budget> GetAll()
        {
            using (AuthenticationDB db = new AuthenticationDB())
            {
                return db.Budgets.ToList();
            }
        }

        public Boolean Create(Budget model)
        {
            if (model != null)
            {
                using (AuthenticationDB db = new AuthenticationDB())
                {
                    db.Budgets.Add(model);
                    return (db.SaveChanges() > 0) ? true : false;
                }
            }
            return false;
        }

        public Budget GetOne(int? id)
        {
            if (id != null)
            {
                using (AuthenticationDB db = new AuthenticationDB())
                {
                    Budget Budget = db.Budgets.Find(id);
                    if (Budget != null)
                        return Budget;
                }
            }
            return null;
        }

        public Boolean Update(Budget model)
        {
            if (model != null)
            {
                using (AuthenticationDB db = new AuthenticationDB())
                {
                    db.Budgets.Attach(model);
                    db.Entry(model).Property(ob => ob.BUD_IDE_ACCOUNT).IsModified = true;
                    db.Entry(model).Property(ob => ob.BUD_IDE_AREA).IsModified = true;
                    db.Entry(model).Property(ob => ob.BUD_DES_QUANTITY).IsModified = true;
                    db.Entry(model).Property(ob => ob.BUD_DES_PERIOD).IsModified = true;
                    return (db.SaveChanges() > 0) ? true : false;
                }
            }
            return false;
        }

        public Boolean Delete(int? id)
        {
            if (id != null)
            {
                Budget model = GetOne(id);
                if (model != null)
                {
                    using (AuthenticationDB db = new AuthenticationDB())
                    {
                        db.Budgets.Attach(model);
                        db.Entry(model).State = System.Data.Entity.EntityState.Deleted;
                        return (db.SaveChanges() > 0) ? true : false; ;
                    }
                }
            }
            return false;
        }

        public List<Budget> GetAllByArea(string idArea)
        {
            if (!String.IsNullOrEmpty(idArea))
            {
                using (AuthenticationDB dbContext = new AuthenticationDB())
                {
                    var Buds = (from budget in dbContext.Budgets
                                where string.Compare(idArea, budget.BUD_IDE_AREA, StringComparison.OrdinalIgnoreCase) == 0
                                select budget).ToList<Budget>();
                    return Buds;
                }
            }
            return null;
        }
    }
}