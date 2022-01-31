using PaymentMicroservice.DAL.Entities;
using PaymentMicroservice.Models.RequestModels;
using PaymentMicroservice.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentMicroservice.Repositories
{
    interface IPaymentRepository
    {
        PaymentResponseModel InsertPayment(Order model);
    }
}
