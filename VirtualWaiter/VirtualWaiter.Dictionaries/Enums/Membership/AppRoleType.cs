using VirtualWaiter.Resources.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWaiter.Dictionaries
{
    public enum AppRoleType
    {
        [Display(ResourceType = typeof(AppRoleTypeResource), Name = "AppRoleType_Administrator")]
        Administrator = 0,
    }
}
