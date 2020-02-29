using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWaiter.Domain
{
    [Table("AppUsers")]
    public class AppUser : Person
    {
        public AppUser()
        {

        }
        public string Login { get; set; }
        public virtual List<AppUserRole> AppUserRoles { get; set; }
    }
}
