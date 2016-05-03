namespace RieltorBase.Domain.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// Интерфейс репозитория объектов недвижимости.
    /// </summary>
    public interface IRealtyObjectsRepository 
        : IRepository<IRealtyObject>
    {
        /// <summary>
        /// Найти объекты недвижимости по параметрам поиска.
        /// </summary>
        /// <param name="options">Параметры поиска.</param>
        /// <returns>Найденные объекты недвижимости.</returns>
        IEnumerable<IRealtyObject> FindByParams(
            RealtyObjectSearchOptions options);
    }
}
