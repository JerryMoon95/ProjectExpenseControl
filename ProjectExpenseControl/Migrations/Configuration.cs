namespace ProjectExpenseControl.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ProjectExpenseControl.DataAccess.AuthenticationDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;//TODO: poner en false
            AutomaticMigrationDataLossAllowed = true; //TODO: Poner en false
        }

        protected override void Seed(ProjectExpenseControl.DataAccess.AuthenticationDB context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
