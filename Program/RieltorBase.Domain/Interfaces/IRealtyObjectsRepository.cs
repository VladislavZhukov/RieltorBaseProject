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

        /// <summary>
        /// Найти объекты недвижимости определенного агента.
        /// </summary>
        /// <param name="agentId">Id агента.</param>
        /// <returns>Объекты недвижимости определенного агента.</returns>
        IEnumerable<IRealtyObject> FindByAgent(int agentId);

        /// <summary>
        /// Найти объекты недвижимости всех агентов определенной фирмы.
        /// </summary>
        /// <param name="firmId">Id фирмы.</param>
        /// <returns>Объекты недвижимости всех агентов фирмы.</returns>
        IEnumerable<IRealtyObject> FindByFirm(int firmId);
    }
}
