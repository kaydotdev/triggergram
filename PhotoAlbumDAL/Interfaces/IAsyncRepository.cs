using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhotoAlbumDAL.Interfaces
{
    public interface IAsyncRepository<TEntity, TKey> 
        where TEntity : class
    {
        Task<TEntity> GetByKeyAsync(TKey key);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetByConditionAsync(Func<TEntity, bool> predicate);
        Task CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task DeleteByKeyAsync(TKey key);
    }
}
