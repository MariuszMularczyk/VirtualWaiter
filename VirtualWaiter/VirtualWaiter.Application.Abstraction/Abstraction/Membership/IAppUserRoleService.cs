using VirtualWaiter.Dictionaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualWaiter.Application.Abstraction;
using DevExtreme.AspNet.Data;

namespace VirtualWaiter.Application
{
    public interface IAppUserRoleService : IService
    {
        List<FunctionalityType> GetPermissions(int userId);
        object GetUserRoles(DataSourceLoadOptionsBase loadOptions, int userId);
        void Delete(int userRoleId);
        object GetRoleUsers(DataSourceLoadOptionsBase loadOptions, int roleId);
        AppUserRoleAddToRoleVM GetAppUserRoleAddToRoleVM(int roleId);
        bool IsUserInRole(int userId, int roleId);
        void AddUserToRole(int userId, int roleId);
        AppUserRoleAddToUserVM GetAppUserRoleAddToUserVM(int userId, AppUserRoleAddToUserVM model = null);
    }
}
