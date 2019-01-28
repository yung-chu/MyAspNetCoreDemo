using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using Microsoft.AspNetCore.Mvc;
using MyAspNetCoreDemo.Services;

namespace MyAspNetCoreDemo.Web.Controllers
{
    public class EasyNetQController : BaseController
    {
        private IBus _bus;

        public EasyNetQController(IBus bus)
        {
            _bus = bus;
        }

        /// <summary>
        /// 发布者
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task Publish([FromBody]Order message)
        {
            await _bus.PublishAsync(message);
        }


        /// <summary>
        /// 订阅者1
        /// EasyNetQ提供了一个AutoSubscriber的方式，可以通过接口和标签快速地让一个类成为Consumer。
        /// 详细内容参考：https://github.com/EasyNetQ/EasyNetQ/wiki/Auto-Subscriber
        /// /// </summary>
        private class OrderConsumerAsync : IConsumeAsync<Order>
        {
            [AutoSubscriberConsumer(SubscriptionId = "OrderService.Consumer1")]
            public async Task ConsumeAsync(Order message)
            {
                await Task.Run(() =>
               {
                   for (int i = 0; i < 10000; i++)
                   {
                       Console.WriteLine(i);
                   }
               });
            }
        }


        /// <summary>
        /// 订阅者2
        /// </summary>
        private class OrderConsumer : IConsume<Order>
        {
            [AutoSubscriberConsumer(SubscriptionId = "OrderService.Consumer2")]
            public void Consume(Order message)
            {

                for (int i = 0; i < 10000; i++)
                {
                    Console.WriteLine(i);
                }
            }
        }
    }
}