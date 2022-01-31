using Microsoft.AspNetCore.Mvc;
using OrderMicroservice.Businesses;
using OrderMicroservice.DAL.Entities;
using OrderMicroservice.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderMicroservice.Controllers
{
    [Route("order")]
    public class OrderController : Controller
    {
        private readonly OrderBusiness _orderBusiness;
        public OrderController(OrderBusiness orderBusiness)
        {
            _orderBusiness = orderBusiness;
        }

        [HttpPost("create")]
        public IActionResult CreateOrder([FromBody] OrderRequestModel order)
        {
            var response = _orderBusiness.CreateOrder(order);
            return Ok(response);
        }
    }
}
