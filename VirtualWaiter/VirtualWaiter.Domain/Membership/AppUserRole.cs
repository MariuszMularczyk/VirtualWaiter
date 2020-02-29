using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWaiter.Domain
{
    public class AppUserRole : AuditEntity
    {
        public AppUserRole()
        {

        }
        public int AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
        public int AppRoleId { get; set; }
        public virtual AppRole AppRole { get; set; }
    }
}
