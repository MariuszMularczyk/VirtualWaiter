using Hangfire;
using VirtualWaiter.Domain;
using Ninject.Modules;
using Ninject.Syntax;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using VirtualWaiter.EntityFramework;
using VirtualWaiter.Application;

namespace VirtualWaiter.Infrastructure
{
    public class NinjectCoreApp : NinjectModule
    {
        public override void Load()
        {
            Bind<MainContext>().ToSelf().InNamedOrBackgroundJobScope(context =>
            context.Kernel.Components.GetAll<INinjectHttpApplicationPlugin>().Select(c => c.GetRequestScope(context)).FirstOrDefault(s => s != null) /*?? InCustomParentScope(context)*/);

            Bind<TransactionInterceptor>().ToSelf().InTransientScope();          
            Bind<MainDatabaseContext>().ToSelf().InMainContextScope();       
            Bind<DbSession>().ToSelf().InMainContextScope();

            CoreBindings.Load(Kernel);
            MembershipBindings.Load(Kernel);
            DictionariesBindings.Load(Kernel);
            OrderBindings.Load(Kernel);
        }
    }
    public static class MainContextScope
    {
        public static IBindingNamedWithOrOnSyntax<T> InMainContextScope<T>(this IBindingInSyntax<T> syntax)
        {
            return syntax.InScope(x => ((MainContext)x.Kernel.GetService(typeof(MainContext))));
        }
    }
}
