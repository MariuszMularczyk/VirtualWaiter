using VirtualWaiter.Dictionaries;
using VirtualWaiter.Domain;
using DevExtreme.AspNet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWaiter.Data
{
    public interface IAppUserRoleRepository : IRepository<AppUserRole>
    {
        List<FunctionalityType> GetUserFunctionalities(int userId);
        bool CheckIfIsAdmin(int userId);
        object GetUserRoles(DataSourceLoadOptionsBase loadOptions, int userId);
        object GetRoleUsers(DataSourceLoadOptionsBase loadOptions, int roleId);
    }
}
