using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualWaiter.Dictionaries;

namespace VirtualWaiter.Domain
{
    [Table("Orders")]
    public class Order : AuditEntity
    {
        public string Description { get; set; }
        public OrderStatusEnum OrderStatus { get; set; }
    }
}
