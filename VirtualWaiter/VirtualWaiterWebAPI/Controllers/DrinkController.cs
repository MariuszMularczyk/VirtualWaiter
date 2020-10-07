using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Ninject;
using VirtualWaiter.Application;
using VirtualWaiter.Data;

namespace VirtualWaiterWebAPI.Controllers
{
    [RoutePrefix("api/[controller]")]
    public class DrinksController : ApiController
    {
        #region Dependencies
        [Inject]
        public IDrinkService DrinkService { get; set; }
        [Inject]
        public IOrderService OrderService { get; set; }
        #endregion

        [HttpPost]
        [ActionName("add")]
        public void Add(DrinkAddVM model)
        {
            if (ModelState.IsValid)
            {
                DrinkService.Add(model);
            }
        }

        [HttpGet]
        [ActionName("getDrinks")]
        public List<DrinkListDTO> GetDrinks()
        {
            return DrinkService.GetDrinks();
        }

        [HttpGet]
        [ActionName("getValues")]
        public IEnumerable<string> GetValues()
        {
            return new string[] { "value1", "value2" };
        }

    }
}
