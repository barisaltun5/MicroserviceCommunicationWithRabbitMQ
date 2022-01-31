using PaymentMicroservice.Models.ResponseModels;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentMicroservice.Contracts.RabbitMQ
{
    interface IEventBusRabbitMQ
    {
        IModel CreateConsumerChannel();
        bool CreatePaymentMessage(PaymentResponseModel model);
    }
}
