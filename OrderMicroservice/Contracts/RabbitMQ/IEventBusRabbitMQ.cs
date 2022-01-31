using OrderMicroservice.Models.ResponseModels;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderMicroservice.Contracts.RabbitMQ
{
    interface IEventBusRabbitMQ
    {
        IModel CreateConsumerChannel();
        bool CreateOrderMessage(OrderResponseModel model);
    }
}
