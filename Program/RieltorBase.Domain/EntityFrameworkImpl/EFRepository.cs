namespace RieltorBase.Domain.EntityFrameworkImpl
{
    using System.Collections.Generic;

    using RieltorBase.Domain.Interfaces;

    public abstract class EFRepository<TEntity> : IRepository<TEntity>
    {
        private readonly VolgaInfoDBEntities context 
            = new VolgaInfoDBEntities();

        public VolgaInfoDBEntities Context
        {
            get
            {
                return this.context;
            }
        }

        public abstract IEnumerable<TEntity> GetAll();

        public abstract TEntity Find(int id);

        public abstract TEntity Add(TEntity newEntity);

        public abstract TEntity Update(TEntity changedEntity);

        public abstract void Delete(int id);

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }
    }
}
