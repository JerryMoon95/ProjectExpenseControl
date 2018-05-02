using ProjectExpenseControl.DataAccess;
using ProjectExpenseControl.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProjectExpenseControl.Services
{
    public class UserRepository
    {
        public List<User> GetAll()
        {
            using (AuthenticationDB db = new AuthenticationDB())
            {
                return db.Users.ToList();
            }
        }

        public void Create(User model)
        {
            using (AuthenticationDB db = new AuthenticationDB())
            {
                db.Users.Add(model);
                db.SaveChanges();
            }
        }

        public User GetOne(string id)
        {
            using (AuthenticationDB db = new AuthenticationDB())
            {
                User User = db.Users.Find(id);
                return User;
            }
        }

        public Boolean Update(User User)
        {
            if (User != null)
            {
                using (AuthenticationDB db = new AuthenticationDB())
                {
                    db.Entry(User).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return true;
            }
            return false;
        }

        public Boolean Delete(string id)
        {
            User User = GetOne(id);
            if (User != null)
            {
                using (AuthenticationDB db = new AuthenticationDB())
                {
                    db.Users.Remove(User);
                    db.SaveChanges();
                }
                return true;

            }
            return false;
        }
    }
}