using VirtualWaiter.Application.Abstraction;
using VirtualWaiter.Domain;
using VirtualWaiter.Utils;
using DevExtreme.AspNet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using VirtualWaiter.Data;

namespace VirtualWaiter.Application
{
    public interface IDrinkService : IService
    {
        void Add(DrinkAddVM model);
        List<DrinkListDTO> GetDrinks();
    }
}