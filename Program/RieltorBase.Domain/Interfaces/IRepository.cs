namespace RieltorBase.Domain.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// Общий интерфейс репозитория (хранилища).
    /// </summary>
    /// <typeparam name="TEntity">Тип хранимых объектов.</typeparam>
    public interface IRepository<TEntity>
    {
        /// <summary>
        /// Получить все объекты, которые есть в репозитории.
        /// </summary>
        /// <returns>Все имеющиеся объекты.</returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Найти определенный объект по уникальному идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор объекта.</param>
        /// <returns>Найденный конкретный объект.</returns>
        TEntity Find(int id);
 
        /// <summary>
        /// Добавить новый объект в хранилище.
        /// </summary>
        /// <param name="newEntity">Новый объект.</param>
        /// <returns>Добавленный объект.</returns>
        TEntity Add(TEntity newEntity);

        /// <summary>
        /// Обновить данные существующего объекта.
        /// </summary>
        /// <param name="changedEntity">Объект с обновленными данными.</param>
        /// <returns>Обновленный объект.</returns>
        TEntity Update(TEntity changedEntity);

        /// <summary>
        /// Удалить конкретный объект.
        /// </summary>
        /// <param name="id">Уникальный идентификатор 
        /// удаляемого объекта.</param>
        void Delete(int id);

        /// <summary>
        /// Сохранить изменения.
        /// </summary>
        void SaveChanges();
    }
}
