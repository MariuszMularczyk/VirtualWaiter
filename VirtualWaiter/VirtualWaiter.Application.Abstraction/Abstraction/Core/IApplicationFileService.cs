using VirtualWaiter.Application.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWaiter.Application
{
    public interface IApplicationFileService : IService
    {
        ImageVM GetImage(int id);
    }
}
