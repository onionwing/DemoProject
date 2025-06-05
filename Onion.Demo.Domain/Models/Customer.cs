
using System.ComponentModel.DataAnnotations;

namespace Onion.Demo.Domain.Models
{

    public class Customer
    {
        [Key]
        public string Id { get; private set; } = Guid.NewGuid().ToString();

        public string Name { get; private set; }

        public string Phone { get; private set; }

    }
}
