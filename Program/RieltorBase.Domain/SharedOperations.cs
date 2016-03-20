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
        private static readonly VolgInfoDBEntities dbContext
            = new VolgInfoDBEntities();
        
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

            IQueryable<Apartment_PropertiesApartment> matchProps
                = SharedOperations.dbContext.Apartment_PropertiesApartment
                    .Where(prop => (prop.PropertiesApartment.NameProperties == "Address" && prop.ValueProperties.Contains(searchOptions.PartOfAddress))
                        /* по цене не работает. Надо или столбец в БД, или хранимую процедуру или еще что-нибудь.|| prop.PropertiesApartment.NameProperties == "Cost" && (prop.IntValue >= searchOptions.MinCost && prop.IntValue <= searchOptions.MaxCost)*/);

            IQueryable<int> appartmentIds = matchProps.Select(prop => prop.Id_apartment);

            List<AppartmentInfo> result = SharedOperations.dbContext.Apartments
                .Where(app => appartmentIds.Contains(app.Id_apartment))
                .Select(SharedOperations.GetAppartmentInfoFromAppartment).ToList();

            return result;
        }

        private static AppartmentInfo GetAppartmentInfoFromAppartment(
            Apartment appartment)
        {
            AppartmentInfo resultInfo = new AppartmentInfo();
            resultInfo.Id = appartment.Id_apartment;

            Apartment_PropertiesApartment costProperty
                = appartment.Apartment_PropertiesApartment.FirstOrDefault(prop => 
                    prop.PropertiesApartment.NameProperties == "Cost");

            resultInfo.Cost = costProperty != null 
                ? costProperty.ValueProperties
                : "Цена не указана.";

            Apartment_PropertiesApartment addressProperty
                = appartment.Apartment_PropertiesApartment.FirstOrDefault(prop =>
                    prop.PropertiesApartment.NameProperties == "Address");

            resultInfo.Address = addressProperty != null
                ? addressProperty.ValueProperties
                : "Без адреса";

            return resultInfo;
        }
    }
}
