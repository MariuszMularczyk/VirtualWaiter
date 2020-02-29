using VirtualWaiter.Dictionaries;
using VirtualWaiter.Domain;
using VirtualWaiter.Utils;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace VirtualWaiter.EntityFramework.MainDatabaseMigrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<VirtualWaiter.EntityFramework.MainDatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsDirectory = @"MainDatabaseMigrations";
        }

        protected override void Seed(MainDatabaseContext context)
        {
            AddCoreData(context);
            AddAppSeetings(context);
            AddFunctionalities(context);
            AddAppRoles(context);
            AddAppUsers(context);
        }

        #region CoreData
        private void AddCoreData(MainDatabaseContext context)
        {
            if (!context.Languages.Any())
            {
                Language polish = new Language()
                {
                    CultureSymbol = "pl-PL",
                    LanguageDictionary = LanguageDictionary.Polish
                };
                context.Languages.Add(polish);
                context.SaveChanges();
            }
            if (!context.SystemUsers.Any())
            {
                Language polish = context.Languages.FirstOrDefault(x => x.LanguageDictionary == LanguageDictionary.Polish);
                SystemUser admin = new SystemUser()
                {
                    CreatedDate = DateTime.Now,
                    Email = SystemUsers.SystemUserEmail,
                    FirstName = SystemUsers.SystemUserName,
                    IsActive = true,
                    LastName = "",
                    LanguageId = polish.Id,
                    Name = SystemUsers.SystemUserName
                };
                context.SystemUsers.Add(admin);
                context.SaveChanges();
                admin.CreatedById = admin.Id;
                context.Entry(admin).State = EntityState.Modified;

                SystemUser unknownUser = new SystemUser()
                {
                    CreatedDate = DateTime.Now,
                    CreatedById = admin.Id,
                    Email = SystemUsers.UnknownUserEmail,
                    FirstName = SystemUsers.UnknownUserName,
                    IsActive = true,
                    LastName = "",
                    LanguageId = polish.Id,
                    Name = SystemUsers.UnknownUserName
                };
                context.SystemUsers.Add(unknownUser);

                context.SaveChanges();
            }
        }
        #endregion

        #region UsersRoles

        private void AddAppRoles(MainDatabaseContext context)
        {
            SystemUser sysAdmin = context.SystemUsers.Where(x => x.Name == SystemUsers.SystemUserName).FirstOrDefault();
            if (!context.AppRoles.Any())
            {
                AppRole administrators = new AppRole()
                {
                    AppRoleType = AppRoleType.Administrator,
                    CreatedById = sysAdmin.Id,
                    CreatedDate = DateTime.Now,
                    Name = "Administratorzy",
                    Description = "Grupa administratorów systemu",
                };
                context.AppRoles.Add(administrators);

                context.SaveChanges();
            }
        }

        private void AddAppUsers(MainDatabaseContext context)
        {

            SystemUser sysAdmin = context.SystemUsers.Where(x => x.Name == SystemUsers.SystemUserName).FirstOrDefault();
            if (!context.AppUsers.Any())
            {
                Language polish = context.Languages.FirstOrDefault(x => x.LanguageDictionary == LanguageDictionary.Polish);
                AppRole adminRole = context.AppRoles.FirstOrDefault(x => x.AppRoleType == AppRoleType.Administrator);
                string currentlogin = $"{Environment.MachineName}\\{Environment.UserName}";

                var admin = new AppUser()
                {
                    CreatedById = sysAdmin.Id,
                    CreatedDate = DateTime.Now,
                    Email = "admin@pl.pl",
                    IsActive = true,
                    LastName = "Administrator",
                    FirstName = "",
                    LanguageId = polish.Id,
                    Login = currentlogin
                };
                context.AppUsers.Add(admin);

                var adminAdmin = new AppUserRole()
                {
                    CreatedById = sysAdmin.Id,
                    CreatedDate = DateTime.Now,
                    AppRole = adminRole,
                    AppUser = admin
                };
                context.AppUserRoles.Add(adminAdmin);

                context.SaveChanges();
            }
        }
        #endregion UsersRoles

        #region AppSetting
        private void AddAppSeetings(MainDatabaseContext context)
        {
            if (!context.AppSettings.Any(r => r.Type == AppSettingEnum.ApplicationWebAddress))
            {
                AppSetting setting = new AppSetting();
                setting.Type = AppSettingEnum.ApplicationWebAddress;
                setting.Value = "http://localhost:18828/";
                context.AppSettings.Add(setting);
            }
            context.SaveChanges();
        }
        #endregion AppSetting

        #region Functionalities
        private void AddFunctionalities(MainDatabaseContext context)
        {

            if (!context.Functionalities.Any(x => x.FunctionalityType == FunctionalityType.GeneralSettings))
            {
                var functionality = new Functionality()
                {
                    FunctionalityType = FunctionalityType.GeneralSettings,
                    Name = "Name",
                    Description = "Description",
                };
                context.Functionalities.Add(functionality);
            }

            context.SaveChanges();
        }
        #endregion Functionalities
    }
}
