using VirtualWaiter.Data;
using VirtualWaiter.Dictionaries;
using VirtualWaiter.Domain;
using VirtualWaiter.Utils;
using DevExtreme.AspNet.Data;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWaiter.Application
{
    public class DrinkService : ServiceBase, IDrinkService
    {
        #region Dependencies
        [Inject]
        public IDrinkRepository DrinkRepository { get; set; }
        #endregion

        public void Add(DrinkAddVM model)
        {
            Drink drink = new Drink()
            {
                Description = model.Description,
                Name = model.Name,
                Price = model.Price

            };
            DrinkRepository.Add(drink);
            DrinkRepository.Save();
        }
        public List<DrinkListDTO>GetDrinks()
        {
            return DrinkRepository.GetAll();
        }
    }
}
