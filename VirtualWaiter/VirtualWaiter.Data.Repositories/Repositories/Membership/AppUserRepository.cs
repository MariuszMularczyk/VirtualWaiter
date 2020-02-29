using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using VirtualWaiter.Dictionaries;
using VirtualWaiter.Domain;
using VirtualWaiter.EntityFramework;
using VirtualWaiter.Infrastructure;
using VirtualWaiter.Utils;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWaiter.Data
{
    public class AppUserRepository : Repository<AppUser, MainDatabaseContext>, IAppUserRepository
    {

        public AppUserRepository(MainDatabaseContext context)
         : base(context)
        {

        }

        public object GetUsersLookup(DataSourceLoadOptionsBase loadOptions)
        {
            IQueryable<SelectModelBinder<int>> query = _dbset.Select(x => new SelectModelBinder<int>() { Value = x.Id, Text = x.LastName + " " + x.FirstName });
            LoadResult result = DataSourceLoader.Load(query, loadOptions);
            return result.data;
        }
        public object GetUsersToList(DataSourceLoadOptionsBase loadOptions)
        {
            var query = _dbset.Select(x => new
            {
                x.Id,
                x.FirstName,
                x.LastName,
                x.IsActive,
                x.Email,
                x.Login,
                Role = x.AppUserRoles.Select(r => r.AppRole.AppRoleType)
            });
            LoadResult result = DataSourceLoader.Load(query, loadOptions);
            return result;
        }
        public string GetActiveUserIdByEmail(string email)
        {
            email = email.Trim().ToLower();
            return null;
            //return _dbset.Where(x => x.IsActive && x.Email == email).Select(x => x.WebUserId).FirstOrDefault();
        }

        public int GetUnknownUserId()
        {
            return Context.SystemUsers.FirstOrDefault(x => x.Email == SystemUsers.UnknownUserEmail).Id;
        }
    }
}
