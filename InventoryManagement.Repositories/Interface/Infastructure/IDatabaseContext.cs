﻿using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace InventoryManagement.Repositories.Interface.Infastructure
{
    public interface IDatabaseContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        int SaveChanges();
    }
}
