using VirtualWaiter.Dictionaries;
using System;
using System.Collections.Generic;

namespace VirtualWaiter.Infrastructure
{
    public class MainContext
    {
        public int PersonId { get; set; }

        public bool IsAuthenticated { get; set; }

        private string _id;
        public IList<FunctionalityType> Functionalities { get; set; }
        public MainContext()
        {
            Functionalities = new List<FunctionalityType>();
            _id = Guid.NewGuid().ToString();
            //_kernel = kernel;
        }
    }
}
