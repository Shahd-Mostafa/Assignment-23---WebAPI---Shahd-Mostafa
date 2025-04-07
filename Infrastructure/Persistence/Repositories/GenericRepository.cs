using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class GenericRepository<TEntity, TKey>(StoreDbContext _context) : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public async Task AddAsync(TEntity entity) =>
            await _context.Set<TEntity>().AddAsync(entity);

        public void Delete(TEntity entity)
            => _context.Set<TEntity>().Remove(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges = false)
        {
            //if (trackChanges)
            //    return Task.FromResult(_context.Set<TEntity>().AsEnumerable());
            //return Task.FromResult(_context.Set<TEntity>().AsNoTracking().AsEnumerable());
            return trackChanges ? await _context.Set<TEntity>().ToListAsync()
                : await _context.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<TEntity?> GetAsync(TKey id)
            => await _context.Set<TEntity>().FindAsync(id);

        public void Update(TEntity entity)

            => _context.Set<TEntity>().Update(entity);
    }
}
