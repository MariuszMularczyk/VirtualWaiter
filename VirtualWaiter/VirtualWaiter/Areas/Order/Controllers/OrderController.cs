using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VirtualWaiter.Application;
using VirtualWaiter.Utils;

namespace VirtualWaiter.Areas.Order.Controllers
{
    [Authorize]
    [AdAuthorization]
    public class OrderController : AppController
    {
        #region Dependencies
        [Inject]
        public IOrderService OrderService { get; set; }
        #endregion

        public ActionResult Index()
        {
            //OrderIndexVM model = OrderService.GetOrderIndexVM();
            OrderIndexVM model = new OrderIndexVM();
            return View(model);
        }
    }
}