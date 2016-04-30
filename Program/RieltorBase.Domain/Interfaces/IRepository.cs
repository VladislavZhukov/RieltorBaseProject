namespace RieltorBase.Domain.Interfaces
{
    using System.Collections.Generic;

    public interface IRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();

        TEntity Find(int id);
 
        TEntity Add(TEntity newEntity);

        TEntity Update(TEntity changedEntity);

        void Delete(int id);

        /// <summary>
        /// Сохранить изменения.
        /// </summary>
        void SaveChanges();
    }
}
