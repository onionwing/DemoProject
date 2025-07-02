using Onion.Demo.Domain.Models;


namespace Onion.Demo.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Customer> Customer { get; }
        IRepository<Order> Order { get; }
        IRepository<Permission> Permission { get; }

        IUserRepository User { get; }

        Task<int> SaveAsync();
    }
}
