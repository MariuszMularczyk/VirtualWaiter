using VirtualWaiter.Resources.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWaiter.Dictionaries
{
    public enum LanguageDictionary : int
    {
        [Display(ResourceType = typeof(EnumResource), Name = "LanguageDictionary_Polish")]
        Polish = 0,
        [Display(ResourceType = typeof(EnumResource), Name = "LanguageDictionary_EnglishGB")]
        EnglishGB = 1,
        [Display(ResourceType = typeof(EnumResource), Name = "LanguageDictionary_GermanDE")]
        GermanDE = 2
    }
}
