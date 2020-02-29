using VirtualWaiter.Resources.Shared;
using VirtualWaiter.Application;
using VirtualWaiter.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.Identity;

namespace VirtualWaiter.Utils
{
    [System.Web.Mvc.Authorize]
    public class AdAuthorizationAttribute : FilterAttribute, IAuthorizationFilter
    {


        public AdAuthorizationAttribute()
        {

        }
        void IAuthorizationFilter.OnAuthorization(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                throw new AuthorizationException(ErrorResource.NoLoginAdded);
            }
            
            AppUserData userData = UserHelper.GetUserData();
            if (!userData.IsActive)
                throw new NotActiveException(ErrorResource.AdAuthorizationNotActiveErrorText);
            MainContext context = DependencyResolver.Current.GetService<MainContext>();
            context.PersonId = userData.Id;
            context.Functionalities = userData.Functionalities;

            IAppUserService userService = DependencyResolver.Current.GetService<IAppUserService>();

            if (userData.Login == null || userData.Login.Length == 0 || userService.GetUserDataByAdLogin(userData.Login) == null)
                throw new AuthorizationException(userData.Login);
        }
    }

    public class AdAuthorizationHubConnectionAttribute : Microsoft.AspNet.SignalR.AuthorizeAttribute
    {
        public AdAuthorizationHubConnectionAttribute()
        {

        }

        public override bool AuthorizeHubConnection(HubDescriptor hubDescriptor, IRequest request)
        {
            //var result = base.AuthorizeHubConnection(hubDescriptor, request);
            if (request.User?.Identity?.IsAuthenticated == true)
            {
                return true;
            }
            return false;
        }
        public override bool AuthorizeHubMethodInvocation(IHubIncomingInvokerContext hubIncomingInvokerContext, bool appliesToMethod)
        {
            if (hubIncomingInvokerContext.MethodDescriptor.Attributes.Any(x => x is AllowServerAttribute))
            {
                return true;
            }
            //var result = base.AuthorizeHubMethodInvocation(hubIncomingInvokerContext, appliesToMethod);
            //CrmUserData userData = UserHelper.GetUserData();
            //MainContext context = DependencyResolver.Current.GetService<MainContext>();
            //context.PersonId = userData.Id;
            //context.Functionalities = userData.Functionalities;
            //context.IsAdmin = userData.IsAdmin;
            //context.IsSok = userData.IsSok;
            //context.BranchId = userData.BranchId;
            //context.IsSmokSupervisor = userData.IsSmokSupervisor;

            //if (!userData.IsAdmin && !userData.IsSok && !userData.BranchId.HasValue)
            //{
            //    return false;
            //}
            return true;
        }
        //protected override bool UserAuthorized(IPrincipal user)
        //{
        //    //var result = base.UserAuthorized(user);
        //    if (!user.Identity.IsAuthenticated)
        //    {
        //        return false;
        //    }
        //    return true;

        //}
    }
}