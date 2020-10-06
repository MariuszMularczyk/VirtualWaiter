using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Ninject;
using VirtualWaiter.Application;


namespace VirtualWaiterWebAPI.Controllers
{
    [RoutePrefix("api/[controller]")]
    public class OrdersController : ApiController
    {
        #region Dependencies
        [Inject]
        public IOrderService OrderService { get; set; }
        #endregion

        [HttpPost]
        [Route("add")]
        public void Add(OrderAddVM model)
        {
            if (ModelState.IsValid)
            {
                OrderService.Add(model);
            }
        }
        [HttpGet]
        [ActionName("getTest")]
        public IEnumerable<string> GetTest()
        {
            return new string[] { "test1", "test2" };
        }

        [HttpGet]
        [ActionName("getValues")]
        public IEnumerable<string> GetValues()
        {
            return new string[] { "value1", "value2" };
        }

    }
}
