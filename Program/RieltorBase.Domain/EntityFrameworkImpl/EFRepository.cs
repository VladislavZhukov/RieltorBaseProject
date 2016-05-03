namespace RieltorBase.Domain.EntityFrameworkImpl
{
    using System.Collections.Generic;

    using RieltorBase.Domain.Interfaces;

    /// <summary>
    /// Базовая часть EF-реализации интерфейса репозитория.
    /// </summary>
    /// <typeparam name="TEntity">Тип хранимых объектов.</typeparam>
    public abstract class EFRepository<TEntity> : IRepository<TEntity>
    {
        /// <summary>
        /// Контекст EF.
        /// </summary>
        private readonly VolgaInfoDBEntities context 
            = new VolgaInfoDBEntities();

        /// <summary>
        /// Получить контекст EF.
        /// </summary>
        public VolgaInfoDBEntities Context
        {
            get
            {
                return this.context;
            }
        }

        /// <summary>
        /// Получить все объекты, хранящиеся в репозитории.
        /// </summary>
        /// <returns>Все объекты из репозитория.</returns>
        public abstract IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Найти конкретный объект по id.
        /// </summary>
        /// <param name="id">Уникальный идентификатор объекта.</param>
        /// <returns>Найденный объект.</returns>
        public abstract TEntity Find(int id);

        /// <summary>
        /// Добавить новый объект.
        /// </summary>
        /// <param name="newEntity">Новый объект.</param>
        /// <returns>Добавленный объект.</returns>
        public abstract TEntity Add(TEntity newEntity);

        /// <summary>
        /// Обновить данные существующего объекта.
        /// </summary>
        /// <param name="changedEntity">Объект с обновленными данными.</param>
        /// <returns>Обновленный объект.</returns>
        public abstract TEntity Update(TEntity changedEntity);

        /// <summary>
        /// Удалить конкретный объект.
        /// </summary>
        /// <param name="id">Идентификатор удаляемого объекта.</param>
        public abstract void Delete(int id);

        /// <summary>
        /// Сохранить изменения.
        /// </summary>
        public void SaveChanges()
        {
            this.context.SaveChanges();
        }
    }
}
