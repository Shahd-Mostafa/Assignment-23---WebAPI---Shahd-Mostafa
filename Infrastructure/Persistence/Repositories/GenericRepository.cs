using Services.Abstraction;
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
            return trackChanges ? await _context.Set<TEntity>().ToListAsync()
                : await _context.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<TEntity?> GetAsync(TKey id)
            => await _context.Set<TEntity>().FindAsync(id);

        public void Update(TEntity entity)

            => _context.Set<TEntity>().Update(entity);

        public async Task<IEnumerable<TEntity>> GetAllAsync(Specifications<TEntity> spec)
        {
            return await _context.Set<TEntity>().GetQuery(spec).ToListAsync();
        }

        public async Task<TEntity?> GetAsync(Specifications<TEntity> spec)
        {
            return await _context.Set<TEntity>().GetQuery(spec).FirstOrDefaultAsync();
        }

        public async Task<int> CountAsync(Specifications<TEntity> spec)
        {
            return await _context.Set<TEntity>().GetQuery(spec).CountAsync();
        }
    }
}
