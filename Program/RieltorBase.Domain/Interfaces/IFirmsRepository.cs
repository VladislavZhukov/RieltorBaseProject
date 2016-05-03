namespace RieltorBase.Domain.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// Интерфейс репозитория (хранилища) агентств недвижимости.
    /// </summary>
    public interface IFirmsRepository : IRepository<IFirm>
    {
        /// <summary>
        /// Поиск по названию.
        /// </summary>
        /// <param name="partOfName">Часть названия.</param>
        /// <returns>Найденные фирмы.</returns>
        IEnumerable<IFirm> FindByName(string partOfName);
    }
}
