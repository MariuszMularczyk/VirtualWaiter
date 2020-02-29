using VirtualWaiter.Data;
using VirtualWaiter.Dictionaries;
using VirtualWaiter.Domain;
using VirtualWaiter.Infrastructure;
using VirtualWaiter.Resources.Shared;
using VirtualWaiter.Utils;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWaiter.Application
{
    public class FunctionalityService : ServiceBase, IFunctionalityService
    {
        #region Dependencies

        [Inject]
        public IFunctionalityRepository FunctionalityRepository { get; set; }
        #endregion

    }
}
