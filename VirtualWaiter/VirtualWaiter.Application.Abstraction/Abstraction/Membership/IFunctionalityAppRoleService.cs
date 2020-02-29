using VirtualWaiter.Dictionaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualWaiter.Application.Abstraction;
using DevExtreme.AspNet.Data;

namespace VirtualWaiter.Application
{
    public interface IFunctionalityAppRoleService : IService
    {
        void Delete(int functionalityRoleId);
        object GetRoleFunctionalities(DataSourceLoadOptionsBase loadOptions, int roleId);
        FunctionalityAppRoleAddVM GetFunctionalityAppRoleAddVM(int roleId);
        void Add(int functionalityId, int roleId);
        object GetFunctionalitiesToAdd(DataSourceLoadOptionsBase loadOptions, int roleId);
    }
}
