using VirtualWaiter.Dictionaries;
using VirtualWaiter.Domain;
using VirtualWaiter.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWaiter.Application
{
    public class AppRoleConverter : ConverterBase
    {
        public AppRoleDetailsVM ToAppRoleDetailsVM(AppRole role)
        {
            var result = new AppRoleDetailsVM()
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description,
                AppRoleType = role.AppRoleType
            };

            return result;
        }
        public AppRoleEditVM ToAppRoleEditVM(AppRole role)
        {
            var result = new AppRoleEditVM()
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description
            };

            return result;
        }
        public AppRole FromAppRoleEditVM(AppRoleEditVM model, AppRole role)
        {
            role.Name = model.Name;
            role.Description = model.Description;
            return role;
        }
        public AppRole FromAppRoleAddVM(AppRoleAddVM model)
        {
            AppRole role = new AppRole
            {
                Name = model.Name,
                Description = model.Description,
                AppRoleType = AppRoleType.Administrator
            };
            return role;
        }

        public AppRole FromParamsToAppRole(AppRoleType appRoleType, string name, string description)
        {
            AppRole appRole = new AppRole()
            {
                Name = name,
                Description = description,
                AppRoleType = appRoleType
            };
            return appRole;
        }
    }
}
