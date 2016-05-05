namespace RieltorBase.Domain.EntityFrameworkImpl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using RieltorBase.Domain.Interfaces;

    /// <summary>
    /// Класс поиска объектов недвижимости.
    /// </summary>
    internal class RealtyObjectsFinder
    {
        /// <summary>
        /// Параметры поиска.
        /// </summary>
        private readonly RealtyObjectSearchOptions options;

        /// <summary>
        /// Исходный запрос.
        /// </summary>
        private readonly IQueryable<RealtyObject> initialQuery;

        /// <summary>
        /// Текущий запрос на поиск.
        /// </summary>
        private IQueryable<RealtyObject> currentQuery;

        /// <summary>
        /// Создать экземпляр класса поиска объектов недвижимости.
        /// </summary>
        /// <param name="options">Параметры поиска.</param>
        /// <param name="initialQuery">Исходный запрос 
        /// (например, все объекты).</param>
        internal RealtyObjectsFinder(
            RealtyObjectSearchOptions options,
            IQueryable<RealtyObject> initialQuery)
        {
            if (options == null)
            {
                throw new ArgumentNullException("options");
            }

            if (initialQuery == null)
            {
                throw new ArgumentNullException("initialQuery");
            }

            this.options = options;
            this.initialQuery = initialQuery;
        }

        /// <summary>
        /// Найти объекты недвижимости.
        /// </summary>
        /// <returns>Найденные и загруженные объекты недвижимости.</returns>
        internal ICollection<RealtyObject> Find()
        {
            this.currentQuery = this.initialQuery;

            this.AddTypeCondition();
            this.AddCostCondition();
            this.AddDateCondition();
            this.AddAddressCondition();

            List<RealtyObject> result = this.currentQuery.ToList();

            this.currentQuery = null;

            return result;
        }

        /// <summary>
        /// Добавить условие поиска по типу.
        /// </summary>
        private void AddTypeCondition()
        {
            if (!string.IsNullOrWhiteSpace(this.options.RealtyObjectType))
            {
                this.currentQuery = this.currentQuery.Where(ro =>
                    this.options.RealtyObjectType == ro.RealtyObjectType.TypeName);
            }
        }

        /// <summary>
        /// Добавить условие поиска по цене.
        /// </summary>
        private void AddCostCondition()
        {
            // todo
            //if (this.options.MinCost != 0 || this.options.MaxCost != 0)
            //{
            //    this.currentQuery = this.currentQuery.Where(ro =>
            //        ro.PropertyValues.Any(pv =>
            //            pv.PropertyType.PropertyName == Metadata.CostPropName
            //            /*&& pv.IntegerValue >= searchOptions.MinCost 
            //             *&& pv.IntegerValue <= searchOptions.MaxCost*/));
            //    // с ценой пока не работает!!! нужно целочисленное поле в БД.
            //}
        }

        /// <summary>
        /// Добавить условие поиска по дате.
        /// </summary>
        private void AddDateCondition()
        {
            // todo
            //if (this.options.MinDate != null
            //    || this.options.MaxDate != null)
            //{
            //    this.currentQuery = this.currentQuery.Where(ro =>
            //        true);
            //        // нужно обязательное поле "CreationDate" ro.CreationDate >= searchOptions.MinDate && ro.CreationDate <= searchOptions.MaxDate);
            //}
        }

        /// <summary>
        /// Добавить условие поиска по части адреса.
        /// </summary>
        private void AddAddressCondition()
        {
            // todo
            //if (!string.IsNullOrWhiteSpace(this.options.PartOfAddress))
            //{
            //    this.currentQuery = this.currentQuery.Where(ro =>
            //        ro.PropertyValues.Any(pv =>
            //            pv.PropertyType.PropertyName == "Адрес"
            //            && pv.StringValue.Contains(this.options.PartOfAddress)));
            //}
        }
    }
}
