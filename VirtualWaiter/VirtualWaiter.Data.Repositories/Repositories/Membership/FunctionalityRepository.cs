using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using VirtualWaiter.Dictionaries;
using VirtualWaiter.Domain;
using VirtualWaiter.EntityFramework;
using VirtualWaiter.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWaiter.Data
{
    public class FunctionalityRepository : Repository<Functionality, MainDatabaseContext>, IFunctionalityRepository
    {
        public FunctionalityRepository(MainDatabaseContext context)
         : base(context)
        {
        }


    }
}
