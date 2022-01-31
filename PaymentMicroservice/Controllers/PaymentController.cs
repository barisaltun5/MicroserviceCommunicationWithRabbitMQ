using Microsoft.AspNetCore.Mvc;
using PaymentMicroservice.Businesses;
using PaymentMicroservice.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentMicroservice.Controllers
{
    [Route("payment")]
    public class PaymentController : Controller
    {
        private readonly PaymentBusiness _paymentBusiness;
        public PaymentController(PaymentBusiness paymentBusiness)
        {
            _paymentBusiness = paymentBusiness;
        }
        [HttpPost("create")]
        public IActionResult CreatePayment([FromBody] Order model)
        {
            var response = _paymentBusiness.CreatePayment(model);
            return Ok(response);
        }
    }
}
