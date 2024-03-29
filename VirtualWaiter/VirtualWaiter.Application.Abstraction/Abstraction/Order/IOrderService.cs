using VirtualWaiter.Application.Abstraction;
using VirtualWaiter.Domain;
using VirtualWaiter.Utils;
using DevExtreme.AspNet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace VirtualWaiter.Application
{
    public interface IOrderService : IService
    {
        void Add(OrderAddVM model);
    }
}