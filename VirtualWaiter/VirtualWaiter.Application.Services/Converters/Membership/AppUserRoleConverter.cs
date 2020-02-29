using VirtualWaiter.Domain;
using VirtualWaiter.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWaiter.Application
{
    public class AppUserRoleConverter : ConverterBase
    {
        public AppUserRole FromAppUserEditVM(AppUserRole appUserRole, AppRole appRole)
        {
            appUserRole.AppRoleId = appRole.Id;
            return appUserRole;
        }
    }
}
