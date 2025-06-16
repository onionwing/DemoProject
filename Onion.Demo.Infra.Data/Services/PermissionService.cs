using Microsoft.EntityFrameworkCore;
using Onion.Demo.Domain.Interfaces;
using Onion.Demo.Domain.Models;
using Onion.Demo.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Demo.Infra.Data.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IRepository<Permission, string> _permission;

        public PermissionService(IRepository<Permission, string> permission)
        {
            _permission = permission;
        }



        public async Task<bool> UserHasAccessAsync(string path, string method, IEnumerable<string> userRoles)
        {
            var perms = await _permission
                .Include(p => p.RolePermissions)
                    .ThenInclude(rp => rp.Role)
                .Where(p => p.Url == path && p.Method == method)
                .ToListAsync();

            var permittedRoles = perms.SelectMany(p => p.RolePermissions).Select(rp => rp.Role.Name).Distinct();

            return userRoles.Intersect(permittedRoles).Any();
        }
    }
}
