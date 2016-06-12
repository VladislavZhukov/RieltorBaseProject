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
            if (options == null)
            {
                return this.GetAll();
            }

            RealtyObjectsFinder finder = new RealtyObjectsFinder(
                options,
                this.Context.RealtyObjects);

            ICollection<RealtyObject> rawResult = finder.Find();

            return rawResult.Select(robj => 
                new RealtyObjectWrap(robj, this.Context));
        }

        /// <summary>
        /// Найти объекты недвижимости определенного агента.
        /// </summary>
        /// <param name="agentId">Id агента.</param>
        /// <returns>Объекты недвижимости определенного агента.</returns>
        public IEnumerable<IRealtyObject> FindByAgent(int agentId)
        {
            return this.Context.RealtyObjects.Where(ro =>
                ro.AgentId == agentId)
                .ToList()
                .Select(obj => new RealtyObjectWrap(obj, this.Context));
        }

        /// <summary>
        /// Найти объекты недвижимости всех агентов определенной фирмы.
        /// </summary>
        /// <param name="firmId">Id фирмы.</param>
        /// <returns>Объекты недвижимости всех агентов фирмы.</returns>
        public IEnumerable<IRealtyObject> FindByFirm(int firmId)
        {
            return this.Context.RealtyObjects.Where(ro =>
                ro.Agent.Id_firm == firmId)
                .ToList()
                .Select(obj => new RealtyObjectWrap(obj, this.Context));
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
                entry.State = oldProps.Contains(propValue) 
                    ? EntityState.Modified 
                    : EntityState.Added;
            }
        }
    }
}
