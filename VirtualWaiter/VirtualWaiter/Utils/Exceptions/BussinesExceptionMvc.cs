using VirtualWaiter.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VirtualWaiter.Utils
{
    public class BussinesExceptionMvc : VirtualWaiter.Application.BussinesException
    {
        public RedirectToRouteResult RedirectToRouteResult { get; set; }
        public BussinesExceptionMvc(int number, string message, RedirectToRouteResult redirectToRouteResult)
            : base(string.Format("({0}) {1}", number.ToString(), message))
        {
            Number = number;
            ExceptionMessage = message;
            RedirectToRouteResult = redirectToRouteResult;
        }
    }
}