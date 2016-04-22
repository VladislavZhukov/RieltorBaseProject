namespace RieltorBase.Domain
{
    using System.Collections.Generic;
    using System.Linq;

    using RieltorBase.Domain.InfoClasses;
    

    /// <summary>
    /// Общедоступные операции, не требующие каких-либо специальных
    /// прав/полномочий.
    /// </summary>
    public static class SharedOperations
    {
        /// <summary>
        /// Контекст базы данных недвижимости.
        /// </summary>
        private static readonly VolgaInfoDBEntities dbContext
            = new VolgaInfoDBEntities();
        
        /// <summary>
        /// Получить информацию об объектах недвижимости, удовлетворяющих 
        /// условиям поиска.
        /// </summary>
        /// <param name="searchOptions">Условия поиска.</param>
        /// <returns>Информация о квартирах.</returns>
        public static IEnumerable<RealtyObjectInfo> GetRealtyObjects(
            RealtyObjectSearchOptions searchOptions)
        {
            IQueryable<RealtyObject> queryableResult = SharedOperations.dbContext.RealtyObjects;

            if (!string.IsNullOrWhiteSpace(searchOptions.RealtyObjectTypes))
            {
                queryableResult = SharedOperations.dbContext.RealtyObjects.Where(ro =>
                    searchOptions.RealtyObjectTypes.Contains(ro.RealtyObjectType.TypeName));
            }

            if (searchOptions.MinCost != 0 || searchOptions.MaxCost != 0)
            {
                queryableResult = queryableResult.Where(ro =>
                    ro.PropertyValues.Any(pv =>
                        pv.PropertyType.PropertyName == "Цена"
                        /*&& pv.IntegerValue >= searchOptions.MinCost 
                         *&& pv.IntegerValue <= searchOptions.MaxCost*/)); // с ценой пока не работает!!! нужно целочисленное поле в БД.
            }

            if (!string.IsNullOrWhiteSpace(searchOptions.PartOfAddress))
            {
                queryableResult = queryableResult.Where(ro =>
                    ro.PropertyValues.Any(pv =>
                        pv.PropertyType.PropertyName == "Адрес"
                        && pv.StringValue.Contains(searchOptions.PartOfAddress)));   
            }

            if (searchOptions.MinDate != null
                || searchOptions.MaxDate != null)
            {
                queryableResult = queryableResult.Where(ro =>
                    true); // нужно обязательное поле "CreationDate" ro.CreationDate >= searchOptions.MinDate && ro.CreationDate <= searchOptions.MaxDate);
            }


            IEnumerable<RealtyObjectInfo> result =
                queryableResult.Select(realtyObject => new
                {
                    realtyObject.RealtyObjectId,
                    RealtyObjectType = realtyObject.RealtyObjectType.TypeName,

                    RealtyObjectProperties = realtyObject.PropertyValues.Select(value =>
                        new
                        {
                            value.PropertyType.PropertyName,
                            PropertyValue = value.StringValue
                        })
                })
                .ToList()
                .Select(anonymousType =>
                new RealtyObjectInfo
                {
                    Id = anonymousType.RealtyObjectId,
                    Type = anonymousType.RealtyObjectType,
                    Properties = anonymousType.RealtyObjectProperties.ToDictionary(
                        value => value.PropertyName, value => value.PropertyValue)
                });
            
            return result;
        }
    }
}
