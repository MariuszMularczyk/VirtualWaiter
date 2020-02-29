using VirtualWaiter.Dictionaries;
using VirtualWaiter.Domain;
using DevExtreme.AspNet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWaiter.Data
{
    public interface IFunctionalityAppRoleRepository : IRepository<FunctionalityAppRole>
    {
        object GetRoleFunctionality(DataSourceLoadOptionsBase loadOptions, int roleId);
        object GetFunctionalitiesToAdd(DataSourceLoadOptionsBase loadOptions, int roleId);
    }
}
