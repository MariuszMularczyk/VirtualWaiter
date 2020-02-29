using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWaiter.Domain
{
    [Table("SystemUsers")]
    public class SystemUser : Person
    {
        public string Name { get; set; }
    }
}
