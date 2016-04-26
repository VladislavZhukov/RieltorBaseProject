namespace RieltorBase.Domain.Interfaces
{
    using System.Collections.Generic;

    public interface IFirmsRepository : IRepository<IFirm>
    {
        IEnumerable<IFirm> FindByName(string partOfName);
    }
}
