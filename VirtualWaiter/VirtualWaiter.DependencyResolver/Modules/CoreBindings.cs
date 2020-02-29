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
    internal class CoreBindings
    {
        internal static void Load(IKernel kernel)
        {
            kernel.Bind<IAppSettingsService>().To<AppSettingsService>().InMainContextScope().Intercept().With<TransactionInterceptor>();
            kernel.Bind<AppSettingsService>().To<AppSettingsService>().InMainContextScope();
            kernel.Bind<IAppSettingsRepository>().To<AppSettingsRepository>().InMainContextScope();

            kernel.Bind<IApplicationFileService>().To<ApplicationFileService>().InMainContextScope().Intercept().With<TransactionInterceptor>();
            kernel.Bind<ApplicationFileService>().To<ApplicationFileService>().InMainContextScope();
            kernel.Bind<IApplicationFileRepository>().To<ApplicationFileRepository>().InMainContextScope();
        }
    }
}
