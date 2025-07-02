using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Demo.Domain.Models
{
    public class Permission
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Url { get; set; } = "";
        public string Method { get; set; } = "";
        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();

    }
}
