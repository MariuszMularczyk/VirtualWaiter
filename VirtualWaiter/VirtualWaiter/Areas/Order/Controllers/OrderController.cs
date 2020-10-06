using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VirtualWaiter.Application;
using VirtualWaiter.Dictionaries;
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
            model.OrderStatuses = EnumHelpers.GetEnumBinderListJson<OrderStatusEnum>();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(OrderAddVM model)
        {
            if (ModelState.IsValid)
            {
                OrderService.Add(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}