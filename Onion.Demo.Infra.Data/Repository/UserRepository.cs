using Microsoft.EntityFrameworkCore;
using Onion.Demo.Domain.Interfaces;
using Onion.Demo.Domain.Models;
using Onion.Demo.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Demo.Infra.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<User> _dbSet;

        public UserRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<User>();
        }

        public async Task AddAsync(User user)
        {
            await _dbSet.AddAsync(user);
        }

        public async Task<User> FindByUserNameAsync(string userName)
        {
            return await _dbSet.Where(o => o.UserName == userName).FirstOrDefaultAsync();
        }
    }
}
