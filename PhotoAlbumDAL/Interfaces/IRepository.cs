using System;
using System.Collections.Generic;

namespace PhotoAlbumDAL.Interfaces
{
    public interface IRepository<TEntity, TKey>
        where TEntity : class
    {
        TEntity GetByKey(TKey key);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetByCondition(Func<TEntity, bool> predicate);
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void DeleteByKey(TKey key);
    }
}

