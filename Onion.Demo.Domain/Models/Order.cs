using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Demo.Domain.Models
{
    public class Order
    {
        [Key]
        public Guid Id { get; private set; } = Guid.NewGuid();

        public string CustomerId { get; private set; }

        public List<OrderItem> Items { get; private set; } = [];

        public decimal TotalAmount { get; private set; }

        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;


    }
}
