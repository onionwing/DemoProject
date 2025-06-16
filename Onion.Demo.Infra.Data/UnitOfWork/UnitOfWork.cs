using Onion.Demo.Domain.Interfaces;
using Onion.Demo.Domain.Models;
using Onion.Demo.Infra.Data.Context;
using Onion.Demo.Infra.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Demo.Infra.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IRepository<Customer, string> Customer { get; }
        public IRepository<Order,string> Order { get; }
        public IUserRepository User { get; }


        public UnitOfWork(AppDbContext context) {
            _context = context;
            Customer = new Repository<Customer,string>(_context);
            Order = new Repository<Order, string>(_context);
            User = new UserRepository(_context);

        }

        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}
