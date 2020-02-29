using Microsoft.AspNet.Identity;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace VirtualWaiter.Utils
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class LogInternalAttribute : FilterAttribute, IActionFilter
    {
        public string Message { get; set; }
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Logger logger = LogManager.GetLogger("InternalLogger");
            var user = filterContext.HttpContext.User as IPrincipal;
            var accountId = user == null ? null : user.Identity.GetUserId();
            var userName = user == null ? null : user.Identity.GetUserName();
            var message = "";

            var controller = filterContext.Controller as Controller;
            if (controller != null)
            {
                message = FormatLogHelper.FormatErrorMessage(controller, accountId, message, userName);
            }
            logger.Info(message);
        }
    }
}