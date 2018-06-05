using InventoryManagement.Models.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using InventoryManagement.Repositories.Interface.Infastructure;

namespace InventoryManagement.Repositories.Infastructure
{
    public abstract class BaseRepository<TEntity, TKey> where TEntity : class, IModel<TKey>, new()
    {
        protected IDatabaseContext Context { get; }

        protected BaseRepository(IDatabaseContext context)
        {
            Context = context;
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();
            return includes.Aggregate(query, (current, include) => current.Include(include));
        }

        public virtual IList<TEntity> GetByIds(IEnumerable<TKey> ids, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();
            query = includes.Aggregate(query, (current, include) => current.Include(include));
            return query.Where(i => ids.Contains(i.Id)).ToList();
        }

        public virtual TEntity GetById(TKey id, params Expression<Func<TEntity, object>>[] includes)
        {
            return GetByIds(new[] { id }, includes).FirstOrDefault();
        }

        public virtual IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();
            query = includes.Aggregate(query, (current, include) => current.Include(include));
            return query.Where(predicate).AsEnumerable();
        }

        public virtual void Insert(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public virtual void Insert(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities.ToList())
            {
                Context.Set<TEntity>().Add(entity);
            }
        }

        public virtual void Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Update(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities.ToList())
            {
                Context.Entry(entity).State = EntityState.Modified;
            }
        }

        public virtual void Update(TEntity entity, params Expression<Func<TEntity, object>>[] properties)
        {
            Context.Set<TEntity>().Attach(entity);
            var entry = Context.Entry(entity);
            foreach (var selector in properties)
            {
                entry.Property(selector).IsModified = true;
            }
        }

        public virtual void Delete(TKey id)
        {
            var fake = new TEntity { Id = id };
            Context.Set<TEntity>().Attach(fake);
            Context.Set<TEntity>().Remove(fake);
        }

        public virtual void Delete(IEnumerable<TKey> ids)
        {
            foreach (var id in ids)
            {
                Delete(id);
            }
        }

        public virtual void Delete(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public virtual void Delete(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities.ToList())
            {
                Delete(entity);
            }
        }
    }
}
