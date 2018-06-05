using InventoryManagement.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace InventoryManagement.Repositories.Interface.Infastructure
{
    public interface IBaseRepository<TEntity, in TKey> where TEntity : IModel<TKey>
    {
        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes);

        TEntity GetById(TKey id, params Expression<Func<TEntity, object>>[] includes);

        IList<TEntity> GetByIds(IEnumerable<TKey> ids, params Expression<Func<TEntity, object>>[] includes);

        IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);

        void Insert(TEntity entity);

        void Insert(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Update(IEnumerable<TEntity> entities);

        void Update(TEntity entity, Expression<Func<TEntity, object>>[] properties);

        void Delete(TEntity entity);

        void Delete(IEnumerable<TEntity> entities);

        void Delete(TKey id);

        void Delete(IEnumerable<TKey> ids);
    }
}
