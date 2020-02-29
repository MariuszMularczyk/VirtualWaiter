using VirtualWaiter.Resources.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWaiter.Application
{
    public class AppRoleEditVM
    {
        public int Id { get; set; }
        public AppRoleEditVM()
        {

        }
        [RequiredShort]
        [Display(ResourceType = typeof(SharedResource), Name = "Name")]
        public string Name { get; set; }
        [Display(ResourceType = typeof(SharedResource), Name = "Description")]
        public string Description { get; set; }

    }
}
