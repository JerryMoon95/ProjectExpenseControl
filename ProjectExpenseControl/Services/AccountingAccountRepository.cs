using ProjectExpenseControl.DataAccess;
using ProjectExpenseControl.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProjectExpenseControl.Services
{
    public class AccountingAccountRepository
    {
        public List<AccountingAccount> GetAll()
        {
            using (AuthenticationDB db = new AuthenticationDB())
            {
                return db.AccountingAccounts.ToList();
            }
        }

        public bool Create(AccountingAccount model)
        {
            if (model != null)
            {
                using (AuthenticationDB db = new AuthenticationDB())
                {
                    db.AccountingAccounts.Add(model);
                    return (db.SaveChanges() > 0) ? true : false ;
                }
                
            }
            return false;
        }

        public AccountingAccount GetOne(string id)
        {
            if(id != null)
            {
                using (AuthenticationDB db = new AuthenticationDB())
                {
                    AccountingAccount accountingAccount = db.AccountingAccounts.Find(id);
                    if(accountingAccount != null)
                        return accountingAccount;
                }
            }
            return null;
        }

        public Boolean Update(AccountingAccount accountingAccount)
        {
            if(accountingAccount != null)
            {
                using (AuthenticationDB db = new AuthenticationDB())
                {
                    db.AccountingAccounts.Attach(accountingAccount);
                    db.Entry(accountingAccount).Property(ob => ob.ACC_DES_ACCOUNT).IsModified = true;
                    return (db.SaveChanges() > 0) ? true : false;
                }
            }
            return false;
        }

        public Boolean Delete(string id)
        {
            if(id != null)
            {
                AccountingAccount accountingAccount = GetOne(id);
                if (accountingAccount != null)
                {
                    using(AuthenticationDB db = new AuthenticationDB())
                    {
                        db.AccountingAccounts.Attach(accountingAccount);
                        //db.AccountingAccounts.DeleteObject(accountingAccount);
                        db.Entry(accountingAccount).State = System.Data.Entity.EntityState.Deleted;
                        return ( db.SaveChanges() > 0 ) ? true : false;
                    }
                }
            }
            return false;
        }
    }
}