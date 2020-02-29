using VirtualWaiter.Dictionaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWaiter.Domain
{
    public class Language : Entity
    {
        public LanguageDictionary LanguageDictionary { get; set; }
        public string CultureSymbol { get; set; }
    }
}
