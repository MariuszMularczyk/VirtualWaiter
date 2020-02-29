using Microsoft.AspNet.Identity;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace VirtualWaiter.Utils
{
    public class FormatLogHelper
    {
        public static string FormatErrorMessage(Controller controller, string accountId = null, string message = null, string userName = null)
        {
            var errorMessage = new StringBuilder();
            errorMessage.AppendLine(string.Format("Date: {0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            errorMessage.AppendLine(string.Format("Action: {0}", controller.RouteData.Action()));
            errorMessage.AppendLine(string.Format("Controller: {0}", controller.RouteData.Controller()));
            errorMessage.AppendLine(string.Format("Area: {0}", controller.RouteData.Area() ?? string.Empty));
            errorMessage.AppendLine(string.Format("HttpMethod: {0}", controller.Request.HttpMethod));
            errorMessage.AppendLine(string.Format("Url: {0}", controller.Request.Url));

            if (!accountId.IsNullOrEmpty())
            {
                errorMessage.AppendLine(string.Format("UserId: {0}", accountId));
            }
            if (!userName.IsNullOrEmpty())
            {
                errorMessage.AppendLine(string.Format("UserName: {0}", userName));
            }
            var routeData = controller.RouteData.RouteValues();
            if (routeData.Any())
            {
                errorMessage.AppendLine(string.Format("Route data:"));
                foreach (var value in routeData)
                {
                    errorMessage.AppendLine(string.Format("{0}: {1}", value.Key, value.Value));
                }
            }
            var model = controller.ViewData.Model();
            if (model.Keys.Any())
            {
                errorMessage.AppendLine(string.Format("Model data:"));
                foreach (var key in model.Keys)
                {
                    var value = model[key] is IEnumerable<string>
                        ? string.Join(", ", model[key] as IEnumerable<string>)
                        : model[key];
                    errorMessage.AppendLine(string.Format("{0}: {1}", key, value));
                }
            }

            if (!string.IsNullOrEmpty(message))
            {
                errorMessage.AppendLine("Message " + message);
            }

            return errorMessage.ToString();
        }
        private static string FormatTraceMessage(Controller controller, string accountId = null, string message = null)
        {
            var traceMessage = new StringBuilder();
            if (!string.IsNullOrEmpty(message))
            {
                traceMessage.AppendLine(message);
            }
            traceMessage.AppendLine(string.Format("Date: {0}", DateTime.Now.ToDateTimeStringSafe()));
            traceMessage.AppendLine(string.Format("Action: {0}", controller.RouteData.Action()));
            traceMessage.AppendLine(string.Format("Controller: {0}", controller.RouteData.Controller()));
            traceMessage.AppendLine(string.Format("Area: {0}", controller.RouteData.Area()));
            traceMessage.AppendLine(string.Format("HttpMethod: {0}", controller.Request.HttpMethod));
            traceMessage.AppendLine(string.Format("Url: {0}", controller.Request.Url));

            if (!accountId.IsNullOrEmpty())
            {
                traceMessage.AppendLine(string.Format("UserId: {0}", accountId));
            }

            return traceMessage.ToString();
        }

        private static string FormatTraceExtendMessage(Controller controller, string accountId = null, string message = null)
        {
            var infoMessage = new StringBuilder();
            if (!string.IsNullOrEmpty(message))
            {
                infoMessage.AppendLine(message);
            }

            infoMessage.AppendLine(string.Format("Date: {0}", DateTime.Now.ToDateTimeStringSafe()));
            infoMessage.AppendLine(string.Format("Action: {0}", controller.RouteData.Action()));
            infoMessage.AppendLine(string.Format("Controller: {0}", controller.RouteData.Controller()));
            infoMessage.AppendLine(string.Format("Area: {0}", controller.RouteData.Area()));
            infoMessage.AppendLine(string.Format("HttpMethod: {0}", controller.Request.HttpMethod));
            infoMessage.AppendLine(string.Format("Url: {0}", controller.Request.Url));

            if (!accountId.IsNullOrEmpty())
            {
                infoMessage.AppendLine(string.Format("UserId: {0}", accountId));
            }
            var routeData = controller.RouteData.RouteValues();
            if (routeData.Any())
            {
                infoMessage.AppendLine(string.Format("Route data:"));
                foreach (var value in routeData)
                {
                    infoMessage.AppendLine(string.Format("{0}: {1}", value.Key, value.Value));
                }
            }
            var model = controller.ViewData.Model();
            if (model.Keys.Any())
            {
                infoMessage.AppendLine(string.Format("Model data:"));
                foreach (var key in model.Keys)
                {
                    var value = model[key] is IEnumerable<string>
                        ? string.Join(", ", model[key] as IEnumerable<string>)
                        : model[key];
                    infoMessage.AppendLine(string.Format("{0}: {1}", key, value));
                }
            }


            return infoMessage.ToString();
        }


        public static void LogTrace(ControllerContext context)
        {
            if (context == null || context.Controller == null)
                return;
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);
            if (sf.GetMethod().ReflectedType.Name != "DefaultControllerAttribute")
                return;

            Logger logger = LogManager.GetLogger("TraceLog");
            if (logger == null)
                return;

            if (logger.IsTraceEnabled || logger.IsInfoEnabled)
            {
                var user = context.HttpContext.User as IPrincipal;
                var accountId = user == null ? null : user.Identity.GetUserId();
                var controller = context.Controller as Controller;
                if (logger.IsTraceEnabled)
                {
                    var message = context.GetType().Name;
                    message = FormatTraceMessage(controller, accountId, message);
                    logger.Trace(message);
                }
                if (logger.IsInfoEnabled)
                {
                    var message = context.GetType().Name;
                    message = FormatTraceExtendMessage(controller, accountId, message);
                    logger.Info(message);
                }
            }
        }

        //public static void LogUserActivity(string userId, LogUserActivityType type, string clientIp, string sessionId)
        //{
        //    Logger logger = LogManager.GetLogger("UserActivityLogger");
        //    LogEventInfo theEvent = new LogEventInfo(LogLevel.Info, "", "");
        //    theEvent.Properties["UserId"] = userId;
        //    theEvent.Properties["LogUserActivityType"] = (int)type;
        //    theEvent.Properties["ClientIp"] = clientIp;
        //    theEvent.Properties["CreatedDateTick"] = DateTime.Now.Ticks;
        //    theEvent.Properties["SessionId"] = sessionId;
        //    theEvent.Properties["Url"] = string.Empty;
        //    logger.Log(theEvent);
        //}

        //public static void LogUserRequest(string userId, string clientIp, string sessionId, string url)
        //{
        //    Logger logger = LogManager.GetLogger("UserRequestLogger");
        //    LogEventInfo theEvent = new LogEventInfo(LogLevel.Info, "", "");
        //    theEvent.Properties["UserId"] = userId;
        //    theEvent.Properties["LogUserActivityType"] = (int)LogUserActivityType.Request;
        //    theEvent.Properties["ClientIp"] = clientIp;
        //    theEvent.Properties["CreatedDateTick"] = DateTime.Now.Ticks;
        //    theEvent.Properties["SessionId"] = sessionId;
        //    theEvent.Properties["Url"] = url;
        //    logger.Log(theEvent);
        //}

    }
}