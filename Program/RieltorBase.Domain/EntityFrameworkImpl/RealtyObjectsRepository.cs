namespace RieltorBase.Domain.EntityFrameworkImpl
{
    using System.Collections.Generic;
    using System.Linq;

    using RieltorBase.Domain.Interfaces;

    public class RealtyObjectsRepository : EFRepository<IRealtyObject>, IRealtyObjectsRepository
    {
        public override IEnumerable<IRealtyObject> GetAll()
        {
            return this.Context.RealtyObjects.ToList().Select(
                obj => new RealtyObjectWrap(obj, this.Context));
        }

        public override IRealtyObject Find(int id)
        {
            RealtyObject obj = this.Context.RealtyObjects.FirstOrDefault(
                ro => ro.RealtyObjectId == id);

            return obj != null
                ? new RealtyObjectWrap(obj, this.Context) 
                : null;
        }

        public override IRealtyObject Add(IRealtyObject newEntity)
        {
            RealtyObjectWrap wrap =
                new RealtyObjectWrap(newEntity, this.Context);

            this.Context.RealtyObjects.Add(wrap.GetRealObject());
            return wrap;
        }

        public override IRealtyObject Update(IRealtyObject changedEntity)
        {
            RealtyObject updatedObj = this.Context.RealtyObjects.First(ro =>
                ro.RealtyObjectId == changedEntity.RealtyObjectId);

            RealtyObjectWrap wrap = new RealtyObjectWrap(
                changedEntity,
                this.Context);

            wrap.UpdateRealObject(updatedObj);

            return wrap;
        }

        public override void Delete(int id)
        {
            this.Context.RealtyObjects.Remove(
                this.Context.RealtyObjects.First(ro => 
                    ro.RealtyObjectId == id));
        }

        public IEnumerable<IRealtyObject> FindByParams(
            RealtyObjectSearchOptions options)
        {
            IQueryable<RealtyObject> queryableResult =
                this.Context.RealtyObjects;

            if (!string.IsNullOrWhiteSpace(options.RealtyObjectTypes))
            {
                queryableResult = queryableResult.Where(ro =>
                    options.RealtyObjectTypes.Contains(
                        ro.RealtyObjectType.TypeName));
            }

            if (options.MinCost != 0 || options.MaxCost != 0)
            {
                queryableResult = queryableResult.Where(ro =>
                    ro.PropertyValues.Any(pv =>
                        pv.PropertyType.PropertyName == Metadata.CostPropName
                        /*&& pv.IntegerValue >= searchOptions.MinCost 
                         *&& pv.IntegerValue <= searchOptions.MaxCost*/)); // с ценой пока не работает!!! нужно целочисленное поле в БД.
            }

            if (!string.IsNullOrWhiteSpace(options.PartOfAddress))
            {
                queryableResult = queryableResult.Where(ro =>
                    ro.PropertyValues.Any(pv =>
                        pv.PropertyType.PropertyName == "Адрес"
                        && pv.StringValue.Contains(options.PartOfAddress)));
            }

            if (options.MinDate != null
                || options.MaxDate != null)
            {
                queryableResult = queryableResult.Where(ro =>
                    true); // нужно обязательное поле "CreationDate" ro.CreationDate >= searchOptions.MinDate && ro.CreationDate <= searchOptions.MaxDate);
            }

            IEnumerable<IRealtyObject> result = queryableResult.Select(robj =>
                new RealtyObjectWrap(robj, this.Context));

            return result.AsQueryable();
        }
    }
}
