using VirtualWaiter.Dictionaries;
using VirtualWaiter.Resources.Shared;
using VirtualWaiter.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWaiter.Application
{
    public class AppUserAddVM
    {
        public string Login { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public bool IsActive { get; set; }
        public AppRoleType Role { get; set; }
    }
}
