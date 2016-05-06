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
        /// <remarks>Сюда могут добавляться условия до 
        /// загрузки объектов из БД.</remarks>
        private IQueryable<RealtyObject> currentQuery;

        /// <summary>
        /// Текущий загруженный результат.
        /// </summary>
        /// <remarks>Нужен для фильтрации условий поиска, 
        /// которые не удалось проверить до загрузки.</remarks>
        private IEnumerable<RealtyObject> currentLoadedResult; 

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

            if (string.IsNullOrWhiteSpace(options.RealtyObjectType))
            {
                throw new InvalidOperationException(
                    "В параметрах поиска не указан тип объектов.");
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
            
            // формирование запроса
            this.AddTypeCondition();
            this.AddCostCondition();
            this.AddDateCondition();
            this.AddAddressCondition();

            // выполнение запроса и загрузка объектов
            this.currentLoadedResult = this.currentQuery.ToList();

            ICollection<RealtyObject> result = this.currentLoadedResult.ToList();

            this.currentQuery = null;
            this.currentLoadedResult = null;

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
                    ro.RealtyObjectType.TypeName == this.options.RealtyObjectType);
            }
        }

        /// <summary>
        /// Добавить условие поиска по цене.
        /// </summary>
        private void AddCostCondition()
        {
            if (this.options.MinCost != 0 || this.options.MaxCost != 0)
            {
                this.currentQuery = this.currentQuery.Where(ro =>
                    ro.Cost >= this.options.MinCost
                    && ro.Cost <= this.options.MaxCost);
            }
        }

        /// <summary>
        /// Добавить условие поиска по дате.
        /// </summary>
        private void AddDateCondition()
        {
            if (this.options.MinDate != null
                || this.options.MaxDate != null)
            {
                DateTime minDate = this.options.MinDate ?? DateTime.MinValue;
                DateTime maxDate = this.options.MaxDate ?? DateTime.MaxValue;

                this.currentQuery = this.currentQuery.Where(ro =>
                    ro.Date <= minDate && ro.Date >= maxDate);
            }
        }

        /// <summary>
        /// Добавить условие поиска по части адреса.
        /// </summary>
        private void AddAddressCondition()
        {
            if (!string.IsNullOrWhiteSpace(this.options.PartOfAddress))
            {
                string addressPropName = Metadata.GetAddressPropName(
                    this.options.RealtyObjectType);

                this.currentQuery = this.currentQuery.Where(ro =>
                    ro.PropertyValues.Any(pv =>
                        pv.PropertyType.PropertyName == addressPropName
                        && 
                        (pv.StringValue.Contains(this.options.PartOfAddress))));
            }
        }
    }
}
