using VirtualWaiter.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;

namespace VirtualWaiter.Data
{
    public interface IAppUserRepository : IRepository<AppUser>
    {
        object GetUsersLookup(DataSourceLoadOptionsBase loadOptions);
        object GetUsersToList(DataSourceLoadOptionsBase loadOptions);
        string GetActiveUserIdByEmail(string email);
        int GetUnknownUserId();
    }
}
