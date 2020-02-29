namespace VirtualWaiter.EntityFramework.ErrorDatabaseMigrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<VirtualWaiter.EntityFramework.ErrorDatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsDirectory = @"ErrorDatabaseMigrations";
        }

        protected override void Seed(VirtualWaiter.EntityFramework.ErrorDatabaseContext context)
        {
        }
    }
}
