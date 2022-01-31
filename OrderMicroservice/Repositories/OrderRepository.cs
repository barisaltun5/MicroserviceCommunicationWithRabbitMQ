using AutoMapper;
using NLog;
using OrderMicroservice.Contracts.Logger;
using OrderMicroservice.DAL;
using OrderMicroservice.DAL.Entities;
using OrderMicroservice.Models.Enums;
using OrderMicroservice.Models.RequestModels;
using OrderMicroservice.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderMicroservice.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderMicroserviceContext _context;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        public OrderRepository(OrderMicroserviceContext context, IMapper mapper, ILoggerManager logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }
        public OrderResponseModel InsertOrder(OrderRequestModel model)
        {
            try
            {
                OrderResponseModel responseModel = new();
                Order order = new();
                _mapper.Map(model, order);
                _context.Add(order);
                _context.Save();
                return _mapper.Map(order, responseModel);
            }
            catch(Exception ex)
            {
                _logger.Error(ex, ex.Message + " " + ex.InnerException);
                return null;
            }
        }

        public string UpdateOrder(Payment orderRequestModel)
        {
            try
            {
                if(orderRequestModel.Status == OrderStatus.Success)
                {
                   var order = _context.Orders.FirstOrDefault(x => x.Id == orderRequestModel.OrderId);
                    order.Status = OrderStatus.Success;
                    _context.Update(order);
                    _context.Save();
                    return "Success";
                }
                return "Error";
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message + " " + ex.InnerException);
                return "Error";
            }
        }
    }
}
