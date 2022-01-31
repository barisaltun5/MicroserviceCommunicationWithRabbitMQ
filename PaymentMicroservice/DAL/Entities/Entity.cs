using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentMicroservice.DAL.Entities
{
    public class Entity : IEntity
    {
        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }
        [Column(Order = 1)]
        public DateTime CreatedAt { get; set; }
        [Column(Order = 2)]
        public DateTime? UpdatedAt { get; set; }
    }
}
