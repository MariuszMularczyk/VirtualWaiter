﻿using VirtualWaiter.Infrastructure;
using VirtualWaiter.Domain;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using VirtualWaiter.Application;

namespace VirtualWaiter.Utils
{
    [ExceptionHandler]
    [Authorize]
    [NoCache]
    public abstract class AppController : Controller
    {
        [Inject]
        public MainContext MainContext { get; set; }

        //private static string _unknownUserId;
        private static object _lockObject = new object();

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("pl-PL");
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("pl-PL");
            base.OnAuthorization(filterContext);
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            FormatLogHelper.LogTrace(filterContext);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            FormatLogHelper.LogTrace(filterContext);
            base.OnException(filterContext);
        }

        private Logger logger = LogManager.GetCurrentClassLogger();
        [NonAction]
        protected void LogTrace(string msg)
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);
            logger.Trace(string.Format(Loggers.LogFormat, DateTime.Now.ToDateTimeStringSafe(), this.GetType().Name, sf.GetMethod().Name, msg));
        }
        [NonAction]
        protected void LogInfo(string msg)
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);
            logger.Info(string.Format(Loggers.LogFormat, DateTime.Now.ToDateTimeStringSafe(), this.GetType().Name, sf.GetMethod().Name, msg));
        }
        [NonAction]
        protected void LogError(string msg, Exception ex)
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);
            logger.Error(ex, string.Format(Loggers.LogFormat, DateTime.Now.ToDateTimeStringSafe(), this.GetType().Name, sf.GetMethod().Name, msg));
        }
        [NonAction]
        protected void LogDebug(string msg)
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);
            logger.Debug(string.Format(Loggers.LogFormat, DateTime.Now.ToDateTimeStringSafe(), this.GetType().Name, sf.GetMethod().Name, msg));
        }
        [NonAction]
        protected void LogWarn(string msg)
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);
            logger.Warn(string.Format(Loggers.LogFormat, DateTime.Now.ToDateTimeStringSafe(), this.GetType().Name, sf.GetMethod().Name, msg));
        }
        [NonAction]
        protected string RenderViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext,
                                                                         viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View,
                                             ViewData, TempData, sw);
                try
                {
                    viewResult.View.Render(viewContext, sw);
                }
                catch (Exception ex)
                {

                }
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
        [NonAction]
        protected CustomJsonResult CustomJson(object data, JsonRequestBehavior behavior = JsonRequestBehavior.DenyGet, string dateTimeFormat = DateTimeFormats.IsoDateTimeFormat)
        {
            return new CustomJsonResult() { Data = data, JsonRequestBehavior = behavior, DateTimeFormat = dateTimeFormat };
        }
    }
}