using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Demo.Domain.Interfaces
{
    public interface IRepository<TEntity,TKey> where TEntity : class
    {


        Task<TEntity?> GetByIdAsync(TKey id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
