using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VirtualWaiter.Utils
{
    public class FunctionalityAuthorizationException : Exception
    {
        public string ExceptionMessage { get; set; }
        public RedirectToRouteResult RedirectToRouteResult { get; set; }
        public FunctionalityAuthorizationException(string message)
            : base(string.Format("{0}", message))
        {

        }
    }
}