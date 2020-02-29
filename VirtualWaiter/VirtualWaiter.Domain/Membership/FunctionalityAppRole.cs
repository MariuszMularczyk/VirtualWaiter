using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWaiter.Domain
{
    public class FunctionalityAppRole : AuditEntity
    {
        public int FunctionalityId { get; set; }
        public virtual Functionality Functionality { get; set; }
        public int AppRoleId { get; set; }
        public virtual AppRole AppRole { get; set; }
    }
}
