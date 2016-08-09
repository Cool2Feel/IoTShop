using System.Collections.Generic;
using System.Data.Entity;

namespace IoTShop.Common.Logic.Repositories
{
    public interface IGenericRepo<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> All();
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        TEntity GetByID(object id);
        TEntity Insert(TEntity entity);
        void Update(TEntity entityToUpdate);
        void SaveChanges();
        void SetEntityState(object entity, EntityState state);
    }
}
