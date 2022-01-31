using Newtonsoft.Json;
using PaymentMicroservice.Contracts.RabbitMQ;
using PaymentMicroservice.Models.RequestModels;
using PaymentMicroservice.Models.ResponseModels;
using PaymentMicroservice.Repositories;
using PaymentMicroservice.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentMicroservice.Businesses
{
    public class PaymentBusiness : BaseBusiness
    {
        private readonly PaymentRepository _paymentRepository;
        //private readonly IEventBusRabbitMQ _eventBusRabbitMQ;
        public PaymentBusiness(PaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
            //this._eventBusRabbitMQ = eventBusRabbitMQ;
        }
        public BaseResponseModel CreatePayment(Order order)
        {
            try
            {
                var result = _paymentRepository.InsertPayment(order);
                if (result == null)
                {
                    return ResponseIsFailed("Error", null, null);
                }
                var response = eventBus.CreatePaymentMessage(result);
                if (!response)
                    return ResponseIsFailed("Error", null, null);

                return ResponseIsSuccess(result);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message + " " + ex.InnerException);
                return ResponseCatch();
            }
        }
    }
}
