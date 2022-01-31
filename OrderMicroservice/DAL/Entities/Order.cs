using OrderMicroservice.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderMicroservice.DAL.Entities
{
    public class Order : Entity
    {
        public decimal Price { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Waiting;
    }
}
