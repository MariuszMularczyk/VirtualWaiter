using VirtualWaiter.Dictionaries;
using VirtualWaiter.Resources.Shared;
using VirtualWaiter.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWaiter.Application
{
    public class DrinkAddVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}

