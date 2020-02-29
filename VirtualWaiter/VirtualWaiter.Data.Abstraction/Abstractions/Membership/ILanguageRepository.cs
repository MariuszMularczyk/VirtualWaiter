using VirtualWaiter.Dictionaries;
using VirtualWaiter.Domain;
using VirtualWaiter.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWaiter.Data
{
    public interface ILanguageRepository : IRepository<Language>
    {
        Language GetLanguage(LanguageDictionary languageDictionary);
        List<SelectModelBinder<int>> GetLanguagesToSelect();
    }
}
