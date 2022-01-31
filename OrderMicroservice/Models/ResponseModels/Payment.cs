using OrderMicroservice.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderMicroservice.Models.ResponseModels
{
    public class Payment
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int OrderId { get; set; }
        public decimal Price { get; set; }
        public OrderStatus Status { get; set; } 
    }
}
