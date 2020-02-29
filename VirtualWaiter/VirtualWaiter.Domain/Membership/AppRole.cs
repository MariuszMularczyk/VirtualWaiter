using VirtualWaiter.Dictionaries;
using EntityFramework.Audit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWaiter.Domain
{
    public class AppRole : AuditEntity
    {
        public AppRole()
        {
            FunctionalityAppRoles = new List<FunctionalityAppRole>();
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public AppRoleType AppRoleType { get; set; }
        public virtual List<FunctionalityAppRole> FunctionalityAppRoles { get; set; }
    }
}
