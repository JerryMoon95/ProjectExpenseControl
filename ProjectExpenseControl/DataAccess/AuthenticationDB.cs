using ProjectExpenseControl.DataAccess;
using ProjectExpenseControl.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace ProjectExpenseControl.DataAccess
{
    public class AuthenticationDB : DbContext
    {
        public AuthenticationDB()
            : base("AuthenticationDB")
        {

        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<AccountingAccount> AccountingAccounts { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<StatusAprov> StatusAprovs { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .Map(m =>
                {
                    m.ToTable("UserRoles");
                    m.MapLeftKey("USR_IDE_USER");
                    m.MapRightKey("TUSR_IDE_RESOURCE");
                });
        }

    }
}