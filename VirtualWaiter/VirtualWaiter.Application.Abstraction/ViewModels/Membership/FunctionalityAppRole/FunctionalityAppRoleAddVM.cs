using VirtualWaiter.Resources.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWaiter.Application
{
    public class FunctionalityAppRoleAddVM
    {

        public FunctionalityAppRoleAddVM()
        {

        }
        public int RoleId { get; set; }
        [Display(ResourceType = typeof(SharedResource), Name = "Role")]
        public string RoleName { get; set; }
        [RequiredShort]
        [Display(ResourceType = typeof(SharedResource), Name = "Person")]
        public int? FunctionalityId { get; set; }
        [RequiredShort]
        [Display(ResourceType = typeof(SharedResource), Name = "Functionality")]
        public string FunctionalityName { get; set; }
    }
}
