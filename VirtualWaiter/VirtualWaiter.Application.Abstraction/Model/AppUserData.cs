using VirtualWaiter.Dictionaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWaiter.Application
{
    public class AppUserData
    {
        public AppUserData()
        {
            Functionalities = new List<FunctionalityType>();
            Roles = new List<AppRoleType>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public LanguageDictionary Language { get; set; }
        public List<FunctionalityType> Functionalities { get; set; }
        public string Login { get; set; }
        public bool IsActive { get; set; }
        public List<AppRoleType> Roles { get; set; }
    }
}
