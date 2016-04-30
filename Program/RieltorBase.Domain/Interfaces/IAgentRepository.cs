namespace RieltorBase.Domain.Interfaces
{
    using System.Collections.Generic;

    public interface IAgentRepository : IRepository<IAgent>
    {
        IEnumerable<IAgent> FindByName(string partOfName);
    }
}
