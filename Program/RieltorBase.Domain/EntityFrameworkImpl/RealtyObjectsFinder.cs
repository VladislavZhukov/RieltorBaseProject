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

            this.AddTypePreCondition();
            this.AddCostPreCondition();
            this.AddDatePreCondition();
            this.AddAddressPreCondition();

            this.currentLoadedResult = this.currentQuery.ToList();

            this.AddCostPostCondition();
            this.AddDatePostCondition();

            ICollection<RealtyObject> result = this.currentLoadedResult.ToList();

            this.currentQuery = null;
            this.currentLoadedResult = null;

            return result;
        }

        /// <summary>
        /// Добавить условие поиска по типу (до загрузки объектов).
        /// </summary>
        private void AddTypePreCondition()
        {
            if (!string.IsNullOrWhiteSpace(this.options.RealtyObjectType))
            {
                this.currentQuery = this.currentQuery.Where(ro =>
                    this.options.RealtyObjectType == ro.RealtyObjectType.TypeName);
            }
        }

        /// <summary>
        /// Добавить условие поиска по цене (до загрузки объектов).
        /// </summary>
        private void AddCostPreCondition()
        {
            if (this.options.MinCost != 0 || this.options.MaxCost != 0)
            {
                this.currentQuery = this.currentQuery.Where(ro =>
                    ro.PropertyValues.Any(pv =>
                        pv.PropertyType.PropertyName == Metadata.CostPropName));
            }
        }

        /// <summary>
        /// Добавить условие поиска по дате (до загрузки объектов).
        /// </summary>
        private void AddDatePreCondition()
        {
            if (this.options.MinDate != null
                || this.options.MaxDate != null)
            {
                this.currentQuery = this.currentQuery.Where(ro =>
                    ro.PropertyValues.Any(pv =>
                        pv.PropertyType.PropertyName == Metadata.DatePropName));
            }
        }

        /// <summary>
        /// Добавить условие поиска по части адреса (до загрузки объектов).
        /// </summary>
        private void AddAddressPreCondition()
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

        /// <summary>
        /// Добавить условие поиска по цене (после загрузки объектов).
        /// </summary>
        private void AddCostPostCondition()
        {
            if (this.options.MinCost != 0 || this.options.MaxCost != 0)
            {
                this.currentLoadedResult = this.currentLoadedResult.Where(ob =>
                    ob.PropertyValues.Any(pv =>
                    {
                        int cost;
                        if (!this.TryParseStringCost(pv.StringValue, out cost))
                        {
                            return false;
                        }

                        return cost >= this.options.MinCost
                            && cost <= this.options.MaxCost;
                    }));
            }
        }

        /// <summary>
        /// Добавить условие поиска по дате (после загрузки объектов).
        /// </summary>
        private void AddDatePostCondition()
        {
            if (this.options.MinDate != null
                || this.options.MaxDate != null)
            {
                DateTime minDate = this.options.MinDate ?? DateTime.MinValue;
                DateTime maxDate = this.options.MaxDate ?? DateTime.MaxValue;

                this.currentLoadedResult = this.currentLoadedResult.Where(ro =>
                    ro.PropertyValues.Any(pv =>
                        pv.PropertyType.PropertyName == Metadata.DatePropName
                        && DateTime.Parse(pv.StringValue) >= minDate
                        && DateTime.Parse(pv.StringValue) <= maxDate));
            }
        }

        /// <summary>
        /// Получить целочисленное значение цены из строкового представления.
        /// </summary>
        /// <param name="stringCost">Строковое представление цены.</param>
        /// <param name="result">Целочисленное представление цены.</param>
        /// <returns>True - целочисленное значение получено корректно,
        /// false - <paramref name="stringCost"/> не является строковым
        /// представлением цены.</returns>
        private bool TryParseStringCost(string stringCost, out int result)
        {
            result = 0;

            if (string.IsNullOrWhiteSpace(stringCost))
            {
                return false;
            }

            stringCost = stringCost.Replace("р", string.Empty);
            stringCost = stringCost.Replace("тыс", string.Empty);
            stringCost = stringCost.Replace("руб", string.Empty);
            stringCost = stringCost.Replace(".", string.Empty);

            return int.TryParse(stringCost, out result);
        }
    }
}
