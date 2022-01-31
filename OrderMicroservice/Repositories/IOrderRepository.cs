using OrderMicroservice.DAL.Entities;
using OrderMicroservice.Models.RequestModels;
using OrderMicroservice.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderMicroservice.Repositories
{
    interface IOrderRepository
    {
        OrderResponseModel InsertOrder(OrderRequestModel order);
        string UpdateOrder(Payment order);
    }
}
