using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWaiter.Domain
{
    [Table("Orders")]
    public class Order : AuditEntity
    {
        public string Comment { get; set; }
        public bool IsTakeaway { get; set; }
    }
}
