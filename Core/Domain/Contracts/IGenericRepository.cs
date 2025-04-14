using Services.Abstraction;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain.Contracts
{
    public interface IGenericRepository<TEntity,TKey> where TEntity:BaseEntity<TKey>
    {
        Task<TEntity?> GetAsync(TKey id);
        Task<TEntity?> GetAsync(Specifications<TEntity> spec);
        Task<int> CountAsync(Specifications<TEntity> spec);
        Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges= false);
        Task<IEnumerable<TEntity>> GetAllAsync(Specifications<TEntity> spec);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
