using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UnitOfWork(StoreDbContext _context) : IUnitOfWork
    {
        private readonly ConcurrentDictionary<string,object> _repositories = new ();
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            //var name= typeof(TEntity).Name;
            //if(_repositories.ContainsKey(name))
            //{
            //    return (IGenericRepository<TEntity, TKey>)_repositories[name];
            //}
            //else
            //{
            //    _repositories[name] = new GenericRepository<TEntity, TKey>(_context);
            //    return (_repositories[name] as IGenericRepository<TEntity,TKey>)!;
            //}

            return (_repositories.GetOrAdd(typeof(TEntity).Name,_=> new GenericRepository<TEntity, TKey>(_context)) as IGenericRepository<TEntity, TKey>)!;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
