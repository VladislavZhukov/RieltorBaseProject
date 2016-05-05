namespace RieltorBase.Domain.EntityFrameworkImpl
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;

    using RieltorBase.Domain.Interfaces;

    /// <summary>
    /// EF-реализация хранилища объектов недвижимости.
    /// </summary>
    public class RealtyObjectsRepository : EFRepository<IRealtyObject>, IRealtyObjectsRepository
    {
        /// <summary>
        /// Получить все объекты недвижимости.
        /// </summary>
        /// <returns>Все объекты недвижимости.</returns>
        public override IEnumerable<IRealtyObject> GetAll()
        {
            return this.Context.RealtyObjects.ToList().Select(
                obj => new RealtyObjectWrap(obj, this.Context));
        }

        /// <summary>
        /// Найти конкретный объект недвижимости.
        /// </summary>
        /// <param name="id">Id объекта недвижимости.</param>
        /// <returns>Найденный объект недвижимости.</returns>
        public override IRealtyObject Find(int id)
        {
            RealtyObject obj = this.Context.RealtyObjects.FirstOrDefault(
                ro => ro.RealtyObjectId == id);

            return obj != null
                ? new RealtyObjectWrap(obj, this.Context) 
                : null;
        }

        /// <summary>
        /// Добавить новый объект недвижимости.
        /// </summary>
        /// <param name="newEntity">Новый объект недвижимости.</param>
        /// <returns>Добавленный объект недвижимости.</returns>
        public override IRealtyObject Add(IRealtyObject newEntity)
        {
            RealtyObjectWrap wrap =
                new RealtyObjectWrap(newEntity, this.Context);

            this.Context.RealtyObjects.Add(wrap.GetRealObject());
            return wrap;
        }

        /// <summary>
        /// Обновить данные существующего объекта недвижимости.
        /// </summary>
        /// <param name="changedEntity">Объект недвижимости с 
        /// обновленными данными.</param>
        /// <returns>Обновленный объект недвижимости.</returns>
        public override IRealtyObject Update(IRealtyObject changedEntity)
        {
            if (!this.Context.RealtyObjects.Any(ro =>
                ro.RealtyObjectId == changedEntity.RealtyObjectId))
            {
                throw new InvalidOperationException(
                    "Невозможно обновить данные объекта недвижимости. Объекта с id "
                    + changedEntity.RealtyObjectId + " не существует.");
            }

            RealtyObject rObj = this.AttachRealtyObject(changedEntity);

            this.UpdateProperties(rObj);

            return new RealtyObjectWrap(rObj, this.Context);
        }

        /// <summary>
        /// Удалить объект недвижимости.
        /// </summary>
        /// <param name="id">Id удаляемого объекта.</param>
        public override void Delete(int id)
        {
            this.Context.RealtyObjects.Remove(
                this.Context.RealtyObjects.First(ro => 
                    ro.RealtyObjectId == id));
        }

        /// <summary>
        /// Найти объекты недвижимости.
        /// </summary>
        /// <param name="options">Параметры поиска.</param>
        /// <returns>Найденные объекты недвижимости.</returns>
        public IEnumerable<IRealtyObject> FindByParams(
            RealtyObjectSearchOptions options)
        {
            IQueryable<RealtyObject> queryableResult =
                this.Context.RealtyObjects;

            if (!string.IsNullOrWhiteSpace(options.RealtyObjectType))
            {
                queryableResult = queryableResult.Where(ro =>
                    options.RealtyObjectType.Contains(
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

        /// <summary>
        /// Прикрепить к контексту обновленный объект недвижимости.
        /// </summary>
        /// <param name="changedEntity">Интерфейс обновленного объекта 
        /// недвижимости.</param>
        /// <returns>EF-объект недвижимости, прикрепленный к контексту.</returns>
        private RealtyObject AttachRealtyObject(IRealtyObject changedEntity)
        {
            RealtyObjectWrap wrap = new RealtyObjectWrap(
                changedEntity,
                this.Context);

            RealtyObject rObj = wrap.GetRealObject();

            this.Context.RealtyObjects.Attach(rObj);
            this.Context.Entry(rObj).State = EntityState.Modified;
            return rObj;
        }

        /// <summary>
        /// Обновить все свойства в контексте EF.
        /// </summary>
        /// <param name="rObj">Обновленный EF-объект недвижимости.</param>
        private void UpdateProperties(RealtyObject rObj)
        {
            int[] newPropTypeIds =
                rObj.PropertyValues.Select(pv => pv.PropertyTypeId)
                    .ToArray();

            int id = rObj.RealtyObjectId;

            List<PropertyValue> oldProps = this.Context.PropertyValues
                .Where(pv => pv.RealtyObjectId == id)
                .ToList();

            List<PropertyValue> removedProps = oldProps
                .Where(pv => !newPropTypeIds.Contains(pv.PropertyTypeId))
                .ToList();

            foreach (PropertyValue oldProp in removedProps)
            {
                this.Context.PropertyValues.Remove(oldProp);
            }

            this.UpdatePropertyStates(rObj.PropertyValues, oldProps);
        }

        /// <summary>
        /// Обновить состояние каждого свойства в контексте БД.
        /// </summary>
        /// <param name="propValues">Набор свойств.</param>
        /// <param name="oldProps">Свойства, которые уже были в БД.</param>
        private void UpdatePropertyStates(
            IEnumerable<PropertyValue> propValues,
            ICollection<PropertyValue> oldProps)
        {
            foreach (PropertyValue propValue in propValues)
            {
                DbEntityEntry<PropertyValue> entry = 
                    this.Context.Entry(propValue);

                // если свойство было до этого - оно обновлено, 
                // если нет - добавлено.
                if (oldProps.Contains(propValue))
                {
                    entry.State = EntityState.Modified;
                }
                else
                {
                    entry.State = EntityState.Added;
                }
            }
        }
    }
}
