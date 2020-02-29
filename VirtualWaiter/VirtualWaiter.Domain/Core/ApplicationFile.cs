using VirtualWaiter.Dictionaries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWaiter.Domain
{
    [Table("ApplicationFiles")]
    public class ApplicationFile : AuditEntity
    {
        public string ContentType { get; set; }
        public string Name { get; set; }
        public byte[] Content { get; set; }
    }
}

