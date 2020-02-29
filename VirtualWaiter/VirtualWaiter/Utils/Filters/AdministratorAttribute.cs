using VirtualWaiter.Resources.Shared;
using VirtualWaiter.Application;
using VirtualWaiter.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.Identity;

namespace VirtualWaiter.Utils
{
    [System.Web.Mvc.Authorize]
    public class AdministratorAuthorizationAttribute : FilterAttribute, IAuthorizationFilter
    {


        public AdministratorAuthorizationAttribute()
        {

        }
        void IAuthorizationFilter.OnAuthorization(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new HttpUnauthorizedResult();
                return;
            }
            AppUserData appUserData = UserHelper.GetUserData();
            if (!appUserData.Roles.Any(x => x == Dictionaries.AppRoleType.Administrator)) {
                throw new AuthorizationException(ErrorResource.AccessDenied);
            }
        }
    }
}