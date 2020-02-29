using VirtualWaiter.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWaiter.EntityFramework
{
    public class ErrorDatabaseContext : DbContext
    {
        public ErrorDatabaseContext()
            : base("Name=ErrorDatabaseContext")
        {
            Database.SetInitializer<ErrorDatabaseContext>(null);
        }

        public static ErrorDatabaseContext Create()
        {
            return new ErrorDatabaseContext();
        }

        //Update-Database -configuration VirtualWaiter.EntityFramework.ErrorDatabaseMigrations.Configuration -Verbose

        public DbSet<LogEntry> LogEntries { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
