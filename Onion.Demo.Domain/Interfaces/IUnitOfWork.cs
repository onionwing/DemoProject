using Onion.Demo.Domain.Models;


namespace Onion.Demo.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Customer,string> Customer { get; }
        IRepository<Order, string> Order { get; }
        IUserRepository User { get; }

        Task<int> SaveAsync();
    }
}
