namespace RieltorBase.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using VI_EF;

    public static class SharedOperations
    {
        /// <summary>
        /// Контекст базы данных недвижимости.
        /// </summary>
        private static readonly VolgaInfoDBEntities dbContext
            = new VolgaInfoDBEntities();
        
        /// <summary>
        /// Получить информацию о квартирах, удовлетворяющих 
        /// условиям поиска.
        /// </summary>
        /// <param name="searchOptions">Условия поиска.</param>
        /// <returns>Информация о квартирах.</returns>
        public static IEnumerable<AppartmentInfo> GetAppartments(
            AppartmentSearchOptions searchOptions)
        {
            string partOfAddress = searchOptions.PartOfAddress;
            int minCost = searchOptions.MinCost;
            int maxCost = searchOptions.MaxCost;

            IQueryable<PropertyValue> matchProps
                = SharedOperations.dbContext.PropertyValues
                    .Where(prop => (prop.PropertyType.PropertyName == "Address" && prop.StringValue.Contains(searchOptions.PartOfAddress))
                        /* по цене не работает. Надо или столбец в БД, или хранимую процедуру или еще что-нибудь.|| prop.PropertiesApartment.NameProperties == "Cost" && (prop.IntValue >= searchOptions.MinCost && prop.IntValue <= searchOptions.MaxCost)*/);

            IQueryable<int> appartmentIds = matchProps.Select(prop => prop.RealtyObjectId);

            List<AppartmentInfo> result = SharedOperations.dbContext.RealtyObjects
                .Where(app => appartmentIds.Contains(app.RealtyObjectId))
                .Select(SharedOperations.GetAppartmentInfoFromAppartment).ToList();

            return result;
        }

        private static AppartmentInfo GetAppartmentInfoFromAppartment(
            RealtyObject realtyObj)
        {
            AppartmentInfo resultInfo = new AppartmentInfo();
            resultInfo.Id = realtyObj.RealtyObjectId;

            PropertyValue costProperty
                = realtyObj.PropertyValues.FirstOrDefault(prop => 
                    prop.PropertyType.PropertyName == "Cost");

            resultInfo.Cost = costProperty != null 
                ? costProperty.StringValue
                : "Цена не указана.";

            PropertyValue addressProperty
                = realtyObj.PropertyValues.FirstOrDefault(prop =>
                    prop.PropertyType.PropertyName == "Address");

            resultInfo.Address = addressProperty != null
                ? addressProperty.StringValue
                : "Без адреса";

            return resultInfo;
        }
    }
}
