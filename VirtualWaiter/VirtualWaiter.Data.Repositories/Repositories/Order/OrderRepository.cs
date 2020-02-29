using DevExtreme.AspNet.Data;
using VirtualWaiter.Domain;
using VirtualWaiter.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data.ResponseModel;

namespace VirtualWaiter.Data
{
    public class OrderRepository : Repository<Order, MainDatabaseContext>, IOrderRepository
    {
        public OrderRepository(MainDatabaseContext context) : base(context)
        { }

    }
}
