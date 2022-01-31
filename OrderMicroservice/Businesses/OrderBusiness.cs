using Newtonsoft.Json;
using OrderMicroservice.Contracts.RabbitMQ;
using OrderMicroservice.DAL.Entities;
using OrderMicroservice.Models.RequestModels;
using OrderMicroservice.Models.ResponseModels;
using OrderMicroservice.Repositories;
using OrderMicroservice.Services.RabbitMQ;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderMicroservice.Businesses
{
    public class OrderBusiness : BaseBusiness
    {
        private readonly OrderRepository _orderRepository;
        //private readonly IEventBusRabbitMQ _eventBusRabbitMQ;
        public OrderBusiness(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
            //this._eventBusRabbitMQ = eventBusRabbitMQ;
        }
        public BaseResponseModel CreateOrder(OrderRequestModel order)
        {
            try
            {
                var result = _orderRepository.InsertOrder(order);
                if (result == null)
                {
                    return ResponseIsFailed("Error", null, null);
                }
                eventBus.CreateOrderMessage(result);

                return ResponseIsSuccess(result);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message + " " + ex.InnerException);
                return ResponseCatch();
            }

        }
        public BaseResponseModel UpdateOrder(Payment orderRequestModel)
        {
            try
            {
               var result = _orderRepository.UpdateOrder(orderRequestModel);
               return ResponseIsSuccess(result);
            }
            catch(Exception ex)
            {
                _logger.Error(ex, ex.Message + " " + ex.InnerException);
                return ResponseIsFailed("Error",null,null);
            }
        }
    }
}
