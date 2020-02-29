using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWaiter.Domain
{
    public interface IAuditEntity
    {
        int? CreatedById { get; set; }
        DateTime CreatedDate { get; set; }
        int? ModifiedById { get; set; }
        DateTime? ModifiedDate { get; set; }
    }
}
