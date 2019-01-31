using System;
using System.Collections.Generic;
using System.Text;
using EasyNetQ;
using EasyNetQ.Topology;

namespace MyAspNetCoreDemo.Services
{
    /// <summary>
    /// .net core使用EasyNetQ做EventBus
    /// https://www.cnblogs.com/focus-lei/p/9121095.html
    /// </summary>
    [Queue("Qka.Order", ExchangeName = "Qka.Order")]
    public class Order
    {
        public int OrderId { get; set; }
    }
}
