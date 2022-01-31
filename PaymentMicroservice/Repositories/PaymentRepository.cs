using AutoMapper;
using PaymentMicroservice.Contracts.Logger;
using PaymentMicroservice.DAL;
using PaymentMicroservice.DAL.Entities;
using PaymentMicroservice.Models.RequestModels;
using PaymentMicroservice.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentMicroservice.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly PaymentMicroserviceContext _context;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        public PaymentRepository(PaymentMicroserviceContext context, IMapper mapper, ILoggerManager logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }
        public PaymentResponseModel InsertPayment(Order model)
        {
            try
            {
                PaymentResponseModel responseMesponseModel = new();
                Payment payment = new();
                _mapper.Map(model, payment);
                _context.Add(payment);
                _context.Save();
                return _mapper.Map(payment, responseMesponseModel);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message + " " + ex.InnerException);
                return null;
            }
        }

    }
}
