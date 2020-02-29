using VirtualWaiter.Application;
using VirtualWaiter.Dictionaries;
using VirtualWaiter.Resources.Shared;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VirtualWaiter.Utils
{
    public class UserHelper
    {
        public static AppUserData GetUserData(bool force = false, string userName = "")
        {
            if (HttpContext.Current == null)
                return null;
            if (userName.IsNullOrEmpty())
            {
                userName = HttpContext.Current.User.Identity.Name;
            }
            if (userName.IsNullOrEmpty())
                throw new AuthorizationException(ErrorResource.NoLoginAdded);
            AppUserData userData = null;

            userData = HttpContext.Current.Session?[SessionVariableNames.AppUserData] as AppUserData;

#if DEBUG
            bool isTheSameUserName = true;
#else
            bool isTheSameUserName = false;
            if (userData != null)
                isTheSameUserName = userData.UserName == userName;
#endif
            if (userData == null || !isTheSameUserName || force)
            {
                IAppUserService userService = DependencyResolver.Current.GetService<IAppUserService>();
#if DEBUG
                userData = userService.GetFirstUser();

#else
                //var principalContext = new PrincipalContext(ContextType.Domain);
                userData = userService.GetUserDataByAdLogin(userName);
#endif
                if (userData == null)
                    throw new AuthorizationException(ErrorResource.NoLoginAdded);
                if (HttpContext.Current.Session != null)
                {
                    HttpContext.Current.Session[SessionVariableNames.AppUserData] = userData;
                }
            }

            return userData;
        }

        public static void RefreshUserData()
        {
            string userName = HttpContext.Current.User.Identity.Name;
            if (userName.IsNullOrEmpty())
                return;
            AppUserData userData = HttpContext.Current.Session[SessionVariableNames.AppUserData] as AppUserData;
            if (userData != null && userData.UserName == userName)
            {
                IAppUserService userService = DependencyResolver.Current.GetService<IAppUserService>();
#if DEBUG
                userData = userService.GetFirstUser();
#else
                userData = userService.GetUserDataByAdLogin(userName);
#endif
                if (userData == null)
                    throw new AuthorizationException(ErrorResource.NoLoginAdded);
                HttpContext.Current.Session[SessionVariableNames.AppUserData] = userData;
            }
        }

        public static bool UserHaveFunctionality(params FunctionalityType[] functionalities)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return false;
            }


            IList<FunctionalityType> userFunctionalities = GetUserFunctionalities();
            foreach (FunctionalityType functionality in functionalities)
            {
                if (userFunctionalities.Contains(functionality))
                {
                    return true;
                }
            }

            return false;
        }



        public static IList<FunctionalityType> GetUserFunctionalities()
        {
            AppUserData userData = GetUserData();

            IList<FunctionalityType> userFunctionalities = userData.Functionalities;
            return userFunctionalities;
        }

        public static int GetUnknownUserId()
        {
            IAppUserService userService = DependencyResolver.Current.GetService<IAppUserService>();
            return userService.GetUnknownUserId();
        }

        public static bool IsAdministrator
        {
            get
            {
                AppUserData userData = GetUserData();
                return userData.Roles.Any(x => x == AppRoleType.Administrator);
            }
        }
    }
}