using VirtualWaiter.Resources.Shared;
using VirtualWaiter.Application;
using VirtualWaiter.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR;

namespace VirtualWaiter.Utils
{
    public class AllowServerAttribute : Attribute
    {
    }
}