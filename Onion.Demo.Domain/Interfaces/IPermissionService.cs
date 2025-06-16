using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Demo.Domain.Interfaces
{
    public interface IPermissionService
    {
        Task<bool> UserHasAccessAsync(string path, string method, IEnumerable<string> userRoles);

    }
}
