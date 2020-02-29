using VirtualWaiter.Domain;
using VirtualWaiter.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;

namespace VirtualWaiter.Data
{
    public interface IAppRoleRepository : IRepository<AppRole>
    {
        object GetRolesToList(DataSourceLoadOptionsBase loadOptions);
        List<SelectModelBinder<int>> GetRolesToSelect();
    }
}
