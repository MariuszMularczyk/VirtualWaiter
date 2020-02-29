using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using VirtualWaiter.Domain;
using VirtualWaiter.EntityFramework;
using VirtualWaiter.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualWaiter.Dictionaries;

namespace VirtualWaiter.Data
{
    public class AppUserRoleRepository : Repository<AppUserRole, MainDatabaseContext>, IAppUserRoleRepository
    {
        public AppUserRoleRepository(MainDatabaseContext context)
         : base(context)
        {
        }

        public List<FunctionalityType> GetUserFunctionalities(int userId)
        {
            return _dbset.Where(x => x.Id == userId).SelectMany(x => x.AppRole.FunctionalityAppRoles).Select(x => x.Functionality.FunctionalityType).Distinct().ToList();
        }

        public bool CheckIfIsAdmin(int userId)
        {
            return _dbset.Where(x => x.Id == userId).Any(x => x.AppRole.AppRoleType == AppRoleType.Administrator);
        }

        public object GetUserRoles(DataSourceLoadOptionsBase loadOptions, int userId)
        {
            IQueryable<UserRoleListDTO> query = _dbset.Where(x => x.AppUserId == userId).Select(x => new UserRoleListDTO()
            {
                UserRoleId = x.Id,
                RoleName = x.AppRole.Name
            });
            LoadResult result = DataSourceLoader.Load(query, loadOptions);
            return result;
        }
        public object GetRoleUsers(DataSourceLoadOptionsBase loadOptions, int roleId)
        {
            IQueryable<RoleUserListDTO> query = _dbset.Where(x => x.AppRoleId == roleId).Select(x => new RoleUserListDTO()
            {
                UserRoleId = x.Id,
                FirstName = x.AppUser.FirstName,
                LastName = x.AppUser.LastName,
                FullName = x.AppUser.LastName + " " + x.AppUser.FirstName
            });
            LoadResult result = DataSourceLoader.Load(query, loadOptions);
            return result;
        }
    }
}
