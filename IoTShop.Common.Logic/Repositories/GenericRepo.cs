using IoTShop.Common.Logic.Context;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace IoTShop.Common.Logic.Repositories
{
    public class GenericRepo<TEntity> : IGenericRepo<TEntity> where TEntity : class
    { 
        internal ApplicationDbContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepo()
        {
            context = new ApplicationDbContext();
            dbSet = context.Set<TEntity>();
        }

        public GenericRepo(ApplicationDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> All()
        {
            return dbSet;
        }

        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }

    public virtual TEntity Insert(TEntity entity)
    {
        return dbSet.Add(entity);
    }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            context.Entry(entityToUpdate).State = EntityState.Modified;
            dbSet.Attach(entityToUpdate);
        }

    public virtual void SaveChanges()
    {
        context.SaveChanges();
    }
        
        public virtual void SetEntityState(object entity, EntityState state)
        {
            context.Entry(entity).State = state;
        }
    }
}