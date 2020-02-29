using VirtualWaiter.Resources.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWaiter.Application
{
    public class AppUserRoleAddToRoleVM
    {

        public AppUserRoleAddToRoleVM()
        {

        }
        public int RoleId { get; set; }
        [Display(ResourceType = typeof(SharedResource), Name = "Role")]
        public string RoleName { get; set; }
        [RequiredShort]
        [RemoteApp("IsNotUserInRole", "AppUserRoles", "RoleId", ErrorMessageResourceType = typeof(ErrorResource), ErrorMessageResourceName = "UserAddedToRole")]
        [Display(ResourceType = typeof(SharedResource), Name = "Person")]
        public int? UserId { get; set; }
    }
}
