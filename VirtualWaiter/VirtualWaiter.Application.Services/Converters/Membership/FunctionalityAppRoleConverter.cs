using VirtualWaiter.Domain;
using VirtualWaiter.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWaiter.Application
{
    public class FunctionalityAppRoleConverter : ConverterBase
    {
        public FunctionalityAppRoleAddVM ToFunctionalityAppRoleAddVM(AppRole role)
        {
            FunctionalityAppRoleAddVM result = new FunctionalityAppRoleAddVM()
            {
                RoleId = role.Id,
                RoleName = role.Name
            };
            return result;
        }
    }
}
