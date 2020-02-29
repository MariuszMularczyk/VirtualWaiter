using VirtualWaiter.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWaiter.Data
{
    public interface IAppSettingsRepository : IRepository<AppSetting>
    {
    }
}
