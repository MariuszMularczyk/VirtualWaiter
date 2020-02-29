using VirtualWaiter.Dictionaries;
using VirtualWaiter.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using VirtualWaiter.Application.Abstraction;

namespace VirtualWaiter.Application
{
    public interface IAppRoleService : IService
    {
        object GetRolesToList(DataSourceLoadOptionsBase loadOptions);
        AppRoleDetailsVM GetAppRoleDetailsVM(int roleId);
        int AddRole(AppRoleAddVM model);
        void EditRole(AppRoleEditVM model);
        AppRoleEditVM GetAppRoleEditVM(int id);
        AppRole AddRole(AppRoleType appRoleType, string name, string description = "");
    }
}
