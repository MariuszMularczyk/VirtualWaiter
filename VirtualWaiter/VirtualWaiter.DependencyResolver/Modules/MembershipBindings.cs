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
    internal class MembershipBindings
    {
        internal static void Load(IKernel kernel)
        {



            kernel.Bind<IAppUserService>().To<AppUserService>().InMainContextScope().Intercept().With<TransactionInterceptor>();
            kernel.Bind<AppUserService>().To<AppUserService>().InMainContextScope();
            kernel.Bind<IAppUserRepository>().To<AppUserRepository>().InMainContextScope();
            kernel.Bind<AppUserConverter>().ToSelf().InMainContextScope();


            kernel.Bind<IAppUserRoleService>().To<AppUserRoleService>().InMainContextScope().Intercept().With<TransactionInterceptor>();
            kernel.Bind<AppUserRoleService>().To<AppUserRoleService>().InMainContextScope();
            kernel.Bind<IAppUserRoleRepository>().To<AppUserRoleRepository>().InMainContextScope();
            kernel.Bind<AppUserRoleConverter>().ToSelf().InMainContextScope();


            kernel.Bind<IAppRoleService>().To<AppRoleService>().InMainContextScope().Intercept().With<TransactionInterceptor>();
            kernel.Bind<AppRoleService>().To<AppRoleService>().InMainContextScope();
            kernel.Bind<IAppRoleRepository>().To<AppRoleRepository>().InMainContextScope();
            kernel.Bind<AppRoleConverter>().ToSelf().InMainContextScope();

            kernel.Bind<ILanguageRepository>().To<LanguageRepository>().InMainContextScope();

            kernel.Bind<IFunctionalityAppRoleService>().To<FunctionalityAppRoleService>().InMainContextScope().Intercept().With<TransactionInterceptor>();
            kernel.Bind<FunctionalityAppRoleService>().To<FunctionalityAppRoleService>().InMainContextScope();
            kernel.Bind<IFunctionalityAppRoleRepository>().To<FunctionalityAppRoleRepository>().InMainContextScope();
            kernel.Bind<FunctionalityAppRoleConverter>().ToSelf().InMainContextScope();


            kernel.Bind<IFunctionalityService>().To<FunctionalityService>().InMainContextScope().Intercept().With<TransactionInterceptor>();
            kernel.Bind<FunctionalityService>().To<FunctionalityService>().InMainContextScope();
            kernel.Bind<IFunctionalityRepository>().To<FunctionalityRepository>().InMainContextScope();
        }
    }
}
