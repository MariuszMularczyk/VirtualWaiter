using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace VirtualWaiter.Utils
{
    public class LoginHelper
    {
        public static void ClearSession()
        {
            HttpContext.Current.Session.Clear();
        }
    }
}