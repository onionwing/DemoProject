using Onion.Demo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Demo.Domain.Interfaces
{
    public interface IUserRepository : IDisposable
    {
        Task<User?> FindByUserNameAsync(string userName);
        Task AddAsync(User user);
    }
}
