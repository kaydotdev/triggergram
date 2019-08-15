using System;
using System.Linq;

namespace PhotoAlbumDAL.Interfaces
{
    public interface IQueryableRepository<TEntity, TKey> 
        where TEntity : class
    {
        TEntity GetByKey(TKey key);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetByCondition(Func<TEntity, bool> predicate);
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void DeleteByKey(TKey key);
    }
}
