namespace RieltorBase.Domain.Interfaces
{
    using System.Collections.Generic;

    public interface IRealtyObjectsRepository 
        : IRepository<IRealtyObject>
    {
        IEnumerable<IRealtyObject> FindByParams(
            RealtyObjectSearchOptions options);
    }
}
