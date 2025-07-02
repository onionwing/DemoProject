
using System.ComponentModel.DataAnnotations;

namespace Onion.Demo.Domain.Models
{

    public class Customer
    {
        [Key]
        public Guid Id { get; private set; } = Guid.NewGuid();

        public string Name { get;  set; }

        public string Phone { get;  set; }

    }
}
