using VirtualWaiter.Application;
using VirtualWaiter.Data;
using Ninject;
using Ninject.Extensions.Interception.Infrastructure.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWaiter.Infrastructure
{
    internal class OrderBindings
    {
        internal static void Load(IKernel kernel)
        {
            kernel.Bind<IOrderService>().To<OrderService>().InMainContextScope().Intercept().With<TransactionInterceptor>();
            kernel.Bind<OrderService>().To<OrderService>().InMainContextScope();

            kernel.Bind<IOrderRepository>().To<OrderRepository>().InMainContextScope();

        }
    }
}
