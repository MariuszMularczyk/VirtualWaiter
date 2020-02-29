using VirtualWaiter.Application;
using VirtualWaiter.Domain;
using VirtualWaiter.Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace VirtualWaiter.Utils
{
    public class InternalAuthorizationAttribute : FilterAttribute, IAuthorizationFilter
    {
        private static int? _unknownUserId;
        private static object _lockObject = new object();

        public InternalAuthorizationAttribute()
        {

        }
        void IAuthorizationFilter.OnAuthorization(AuthorizationContext filterContext)
        {
            MainContext context = DependencyResolver.Current.GetService<MainContext>();
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                if (!filterContext.ActionDescriptor.GetCustomAttributes(false).ToList().Any(x => x is AllowAnonymousAttribute))
                {
                    filterContext.Result = new HttpUnauthorizedResult();
                    return;
                }
                if (_unknownUserId == null)
                {
                    lock (_lockObject)
                    {
                        if (_unknownUserId == null)
                        {
                            _unknownUserId = UserHelper.GetUnknownUserId();
                        }
                    }
                }
                context.PersonId = _unknownUserId.Value;
                return;
            }

            //if (!LoginHelper.IsNormalUser())
            //{
            //    filterContext.HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            //    LoginHelper.ClearSession();
            //    filterContext.Result = new HttpUnauthorizedResult();
            //    return;
            //}


            if (LoginHelper.IsUserBanned())
            {
                if (!filterContext.IsChildAction)
                {
                    filterContext.Result = new ViewResult()
                    {
                        ViewName = "Lockout"
                    };
                }

                return;
            }

            AppUserData userData = UserHelper.GetUserData();

            context.PersonId = userData.Id;
            context.Functionalities = userData.Functionalities;

        }
    }
}