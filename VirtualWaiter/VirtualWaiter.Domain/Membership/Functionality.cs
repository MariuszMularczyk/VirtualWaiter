using VirtualWaiter.Dictionaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWaiter.Domain
{
    public class Functionality : Entity
    {
        public Functionality()
        {
            FunctionalityAppRoles = new List<FunctionalityAppRole>();
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public FunctionalityType FunctionalityType { get; set; }
        public virtual List<FunctionalityAppRole> FunctionalityAppRoles { get; set; }
    }
}
