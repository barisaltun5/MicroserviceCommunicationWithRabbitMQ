using PaymentMicroservice.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentMicroservice.DAL.Entities
{
    public class Payment : Entity
    {
        public int OrderId { get; set; }
        public decimal Price { get; set; }
        public PaymentStatus Status { get; set; } = PaymentStatus.Success;
    }
}
