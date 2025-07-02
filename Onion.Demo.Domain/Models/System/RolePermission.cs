using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Demo.Domain.Models
{
    public class RolePermission
    {
        public Guid? RoleId { get; set; }
        public IdentityRole Role { get; set; }
        public Guid? PermissionId { get; set; }
        public Permission Permission { get; set; }
        public string? UserId { get; set; }
        public User User { get; set; }
    }
}
