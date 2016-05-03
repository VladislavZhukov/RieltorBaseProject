namespace RieltorBase.Domain.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// Интерфейс репозитория агентов (т.е. риэлторов, сотрудников фирм).
    /// </summary>
    public interface IAgentRepository : IRepository<IAgent>
    {
        /// <summary>
        /// Поиск по части имени агента.
        /// </summary>
        /// <param name="partOfName">Часть имени агента.</param>
        /// <returns>Найденные агенты.</returns>
        IEnumerable<IAgent> FindByName(string partOfName);

        /// <summary>
        /// Поиск по id фирмы.
        /// </summary>
        /// <param name="firmId">Id риэлторской фирмы.</param>
        /// <returns>Все агенты, работающие в этой фирме.</returns>
        IEnumerable<IAgent> FindByFirmId(int firmId);
    }
}
