using VirtualWaiter.Dictionaries;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace VirtualWaiter.Application
{
    public class ImageVM
    {
        public byte[] Content { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string CreatedByName { get; set; }
    }
}