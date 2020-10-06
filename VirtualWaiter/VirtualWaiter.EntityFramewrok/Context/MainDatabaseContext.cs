using VirtualWaiter.Domain;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using EntityFramework.Functions;
using System.Reflection;

namespace VirtualWaiter.EntityFramework
{
    public class MainDatabaseContext : DbContext
    {
        public MainDatabaseContext()
            : base("Name=MainDatabaseContext")
        {
            Id = Guid.NewGuid();
            Database.SetInitializer<MainDatabaseContext>(null);

        }

        public MainDatabaseContext(bool addMigration)
            : base("Name=MainDatabaseContext")
        {
            Id = Guid.NewGuid();
            Database.SetInitializer<MainDatabaseContext>(new MigrateDatabaseToLatestVersion<MainDatabaseContext, VirtualWaiter.EntityFramework.MainDatabaseMigrations.Configuration>());

        }
        public Guid Id { get; set; }

        public static void MigrateData()
        {
            using (var mainContext = new MainDatabaseContext(true))
            {
                Database.SetInitializer<MainDatabaseContext>(new MigrateDatabaseToLatestVersion<MainDatabaseContext, VirtualWaiter.EntityFramework.MainDatabaseMigrations.Configuration>());
                mainContext.AppUsers.ToList();
            }
        }

        //Update-Database -configuration VirtualWaiter.EntityFramework.MainDatabaseMigrations.Configuration -Verbose

        #region Core

        public DbSet<AppSetting> AppSettings { get; set; }
        public DbSet<Functionality> Functionalities { get; set; }
        public DbSet<FunctionalityAppRole> FunctionalityAppRoles { get; set; }
        public DbSet<Person> Peoples { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<SystemUser> SystemUsers { get; set; }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<AppUserRole> AppUserRoles { get; set; }
        #endregion

        #region Dictionaries

        #endregion

        #region Order
        public DbSet<Order> Orders { get; set; }
        public DbSet<Drink> Drinks { get; set; }
        #endregion


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<decimal>().Configure(c => c.HasPrecision(18, 5));

            #region Membership
            modelBuilder.Configurations.Add(new LanguageConfiguration());
            modelBuilder.Configurations.Add(new PersonConfiguration());
            modelBuilder.Configurations.Add(new AppUserConfiguration());
            modelBuilder.Configurations.Add(new AppUserRoleConfiguration());
            modelBuilder.Configurations.Add(new AppRoleConfiguration());
            modelBuilder.Configurations.Add(new FunctionalityAppRoleConfiguration());
            modelBuilder.Configurations.Add(new FunctionalityConfiguration());

            #endregion

            #region Order
            modelBuilder.Configurations.Add(new OrderConfiguration());
            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}

