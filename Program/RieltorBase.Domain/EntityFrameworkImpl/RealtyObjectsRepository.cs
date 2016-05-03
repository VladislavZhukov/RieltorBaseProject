namespace RieltorBase.Domain.EntityFrameworkImpl
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
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
            if (!this.Context.RealtyObjects.Any(ro =>
                ro.RealtyObjectId == changedEntity.RealtyObjectId))
            {
                throw new InvalidOperationException(
                    "Невозможно обновить данные объекта недвижимости. Объекта с id "
                    + changedEntity.RealtyObjectId + " не существует.");
            }

            RealtyObjectWrap wrap = new RealtyObjectWrap(
                changedEntity,
                this.Context);

            RealtyObject rObj = wrap.GetRealObject();

            this.Context.RealtyObjects.Attach(rObj);
            this.Context.Entry(rObj).State = EntityState.Modified;

            int[] newPropTypeIds = 
                rObj.PropertyValues.Select(pv => pv.PropertyTypeId)
                .ToArray();

            List<PropertyValue> oldProps = this.Context.PropertyValues
                .Where(pv => pv.RealtyObjectId == changedEntity.RealtyObjectId)
                .ToList();

            List<PropertyValue> removedProps = oldProps
                .Where(pv => !newPropTypeIds.Contains(pv.PropertyTypeId))
                .ToList();

            foreach (PropertyValue oldProp in removedProps)
            {
                this.Context.PropertyValues.Remove(oldProp);
            }

            foreach (PropertyValue prop in rObj.PropertyValues)
            {
                DbEntityEntry<PropertyValue> entry = this.Context.Entry(prop);

                if (oldProps.Contains(prop))
                {
                    entry.State = EntityState.Modified;
                }
                else
                {
                    entry.State = EntityState.Added;
                }
            }

            //foreach (PropertyValue newProp in rObj.PropertyValues)
            //{
            //    if (this.Context.Entry(newProp) == null)
            //    {
            //        this.Context.PropertyValues.Attach(newProp);
            //    }

            //    this.Context.Entry(newProp).State = EntityState.Modified;
            //}
            
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
