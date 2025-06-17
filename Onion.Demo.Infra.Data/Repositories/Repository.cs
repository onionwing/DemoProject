using Microsoft.EntityFrameworkCore;
using Onion.Demo.Domain.Interfaces;
using Onion.Demo.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Demo.Infra.Data.Repositories
{
    public class Repository<TEntity> :  IRepository<TEntity> where TEntity : class
    {

        protected readonly AppDbContext _context;
        public readonly DbSet<TEntity> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<TEntity?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<TEntity>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task AddAsync(TEntity entity) => await _dbSet.AddAsync(entity);

        public void Update(TEntity entity) => _dbSet.Update(entity);

        public void Delete(TEntity entity) => _dbSet.Remove(entity);

        public DbSet<TEntity> GetDbSet()
        {
            return _dbSet;
        }

        public void Dispose() => _context.Dispose();

    }
}
