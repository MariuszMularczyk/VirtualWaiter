﻿using VirtualWaiter.Data;
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
    public class OrderService : ServiceBase, IOrderService
    {
        #region Dependencies
        [Inject]
        public IOrderRepository OrderRepository { get; set; }
        #endregion

    }
}