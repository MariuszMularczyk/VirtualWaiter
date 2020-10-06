using VirtualWaiter.Domain;
using DevExtreme.AspNet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWaiter.Data
{
    public interface IDrinkRepository : IRepository<Drink>
    {
        List<DrinkListDTO> GetAll();
    }
}
