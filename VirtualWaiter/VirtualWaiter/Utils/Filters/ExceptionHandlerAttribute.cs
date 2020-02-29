using VirtualWaiter.Application;
using Microsoft.AspNet.Identity;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace VirtualWaiter.Utils
{
    public class ExceptionHandlerAttribute : HandleErrorAttribute
    {
        public string IdParamName { get; set; }

        private string ActionName { get; set; }
        private string ControllerName { get; set; }
        private string AreaName { get; set; }

        public ExceptionHandlerAttribute()
        {

        }
        public ExceptionHandlerAttribute(string actionName)
        {
            ActionName = actionName;
        }
        public ExceptionHandlerAttribute(string actionName, string controllerName)
        {
            ActionName = actionName;
            ControllerName = controllerName;
        }
        public ExceptionHandlerAttribute(string actionName, string controllerName, string areaName)
        {
            ActionName = actionName;
            ControllerName = controllerName;
            AreaName = areaName;
        }
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
            {
                base.OnException(filterContext);
                return;
            }
            Logger logger = LogManager.GetCurrentClassLogger();
            var user = filterContext.HttpContext.User as IPrincipal;
            var accountId = user == null ? null : user.Identity.GetUserId();

            var message = "An unexpected error has occured";

            var controller = filterContext.Controller as Controller;
            if (controller != null)
            {
                message = FormatLogHelper.FormatErrorMessage(controller, accountId, message);
            }


            if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new JsonResult
                {
                    Data = new { success = false, error = filterContext.Exception.ToString() },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
                if (filterContext.Exception is BussinesException)
                {
                    logger.Warn(filterContext.Exception, message);
                    return;
                }
            }

            if (filterContext.Exception is BussinesException)
            {
                filterContext.Controller.TempData["bussinesExceptionMessage"] = filterContext.Exception.Message;
                if ((filterContext.Exception as BussinesExceptionMvc) != null && (filterContext.Exception as BussinesExceptionMvc).RedirectToRouteResult != null)
                {
                    filterContext.Result = (filterContext.Exception as BussinesExceptionMvc).RedirectToRouteResult;
                }
                else if (!ActionName.IsNullOrEmpty())
                {
                    RouteValueDictionary routeValueDictionary = new RouteValueDictionary();
                    routeValueDictionary.Add("action", ActionName);
                    if (!ControllerName.IsNullOrEmpty())
                    {
                        routeValueDictionary.Add("controller", ControllerName);
                        if (!AreaName.IsNullOrEmpty())
                        {
                            routeValueDictionary.Add("area", AreaName);
                        }
                    }
                    filterContext.Result = new RedirectToRouteResult(routeValueDictionary);
                }
                else
                {
                    filterContext.Controller.TempData["bussinesExceptionMessage"] = "";
                    filterContext.Result = new ViewResult
                    {
                        ViewName = "~/Views/shared/Error.cshtml",
                        ViewData = new ViewDataDictionary<string>(filterContext.Exception.Message)
                    };
                }
                filterContext.ExceptionHandled = true;
            }

            if (filterContext.Exception is FunctionalityAuthorizationException)
            {
                HttpRequestBase request = filterContext.RequestContext.HttpContext.Request;
                if (request.UrlReferrer != null && request.UrlReferrer.AbsolutePath != request.Url.AbsolutePath)
                {

                    filterContext.Controller.TempData["authorizationExceptionMessage"] = filterContext.Exception.Message;
                    UrlHelper urlHelper = new UrlHelper(request.RequestContext);
                    if (request.IsAuthenticated && request.UrlReferrer.AbsolutePath == urlHelper.Action("Login", "Account", new { area = "" }))
                    {
                        filterContext.HttpContext.Response.Redirect(urlHelper.Action("Index", "Home", new { area = "" }));
                    }
                    else if (!filterContext.HttpContext.Response.IsRequestBeingRedirected)
                    {
                        filterContext.HttpContext.Response.Redirect(request.UrlReferrer.AbsoluteUri);
                    }
                }
                else
                {
                    filterContext.Result = new ViewResult
                    {
                        ViewName = "~/Views/Error/FunctionalityAuthorizationError.cshtml",
                        ViewData = new ViewDataDictionary<string>(filterContext.Exception.Message)
                    };
                    filterContext.ExceptionHandled = true;
                }
            }
            if (filterContext.Exception is AuthorizationException)
            {
                var adAuthorizationException = filterContext.Exception as AuthorizationException;

                if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new PartialViewResult
                    {
                        ViewName = "~/Views/Shared/AdAuthorization.cshtml",
                        ViewData = new ViewDataDictionary<string>(adAuthorizationException.ExceptionMessage),
                    };
                }
                else
                {
                    filterContext.Result = new ViewResult
                    {
                        ViewName = "~/Views/Shared/AdAuthorization.cshtml",
                        ViewData = new ViewDataDictionary<string>(adAuthorizationException.ExceptionMessage),
                    };
                }
                filterContext.ExceptionHandled = true;
            }
            else if (filterContext.Exception is NotActiveException)
            {
                var exception = filterContext.Exception as NotActiveException;

                if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new PartialViewResult
                    {
                        ViewName = "~/Views/Shared/AdAuthorizationNotActive.cshtml",
                        ViewData = new ViewDataDictionary<string>(exception.ExceptionMessage),
                    };
                }
                else
                {
                    filterContext.Result = new ViewResult
                    {
                        ViewName = "~/Views/Shared/AdAuthorizationNotActive.cshtml",
                        ViewData = new ViewDataDictionary<string>(exception.ExceptionMessage),
                    };
                }
                filterContext.ExceptionHandled = true;
            }
            if (filterContext.ExceptionHandled)
            {
                logger.Warn(filterContext.Exception, message);
                return;
            }

            logger.Error(filterContext.Exception, message);

            base.OnException(filterContext);
        }
    }
}