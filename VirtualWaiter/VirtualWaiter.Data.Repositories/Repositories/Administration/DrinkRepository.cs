using DevExtreme.AspNet.Data;
using VirtualWaiter.Domain;
using VirtualWaiter.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data.ResponseModel;

namespace VirtualWaiter.Data
{
    public class DrinkRepository : Repository<Drink, MainDatabaseContext>, IDrinkRepository
    {
        public DrinkRepository(MainDatabaseContext context) : base(context)
        { }

        public List<DrinkListDTO> GetAll()
        {
            return Context.Drinks.Select(x => new DrinkListDTO()
            {
                Name = x.Name,
                Description = x.Description,
                Price = x.Price
            }).ToList();
        }

    }
}
