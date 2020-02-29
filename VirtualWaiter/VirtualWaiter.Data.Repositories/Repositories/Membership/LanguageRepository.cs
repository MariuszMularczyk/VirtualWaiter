using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using VirtualWaiter.Dictionaries;
using VirtualWaiter.Domain;
using VirtualWaiter.EntityFramework;
using VirtualWaiter.Infrastructure;
using VirtualWaiter.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWaiter.Data
{
    public class LanguageRepository : Repository<Language, MainDatabaseContext>, ILanguageRepository
    {
        public LanguageRepository(MainDatabaseContext context)
         : base(context)
        {
        }

        public Language GetLanguage(LanguageDictionary languageDictionary)
        {
            return _dbset.FirstOrDefault(x => x.LanguageDictionary == languageDictionary);
        }

        public List<SelectModelBinder<int>> GetLanguagesToSelect()
        {
            List<SelectModelBinder<int>> result = _dbset
            .AsEnumerable()
            .Select(x => new SelectModelBinder<int>()
            {
                Value = x.Id,
                Text = x.LanguageDictionary.GetDisplayName()
            }).OrderBy(x => x.Text).ToList();

            return result;
        }
    }
}
