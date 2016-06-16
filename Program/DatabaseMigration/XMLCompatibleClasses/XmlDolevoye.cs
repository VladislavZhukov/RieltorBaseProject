namespace DatabaseMigration.XMLCompatibleClasses
{
    using System.Collections.Generic;
    using System.Linq;

    using RieltorBase.Domain;

    /// <summary>
    /// Новостройка, представленная в xml.
    /// </summary>
    public class XmlDolevoye : XmlBaseRealtyObject
    {
        /// <summary>
        /// Район.
        /// </summary>
        public string District { get; set; }

        /// <summary>
        /// Квартал.
        /// </summary>
        public string Quarter { get; set; }

        /// <summary>
        /// Жилой комплекс.
        /// </summary>
        public string HousingEstate { get; set; }

        /// <summary>
        /// Количество комнат.
        /// </summary>
        public string QuantityRoom { get; set; }

        /// <summary>
        /// Улица.
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// Дом.
        /// </summary>
        public string Home { get; set; }

        /// <summary>
        /// Этаж/этажность.
        /// </summary>
        public string Flor { get; set; }

        /// <summary>
        /// Планировка.
        /// </summary>
        public string Disposition { get; set; }

        /// <summary>
        /// Материал стен.
        /// </summary>
        public string WallMaterial { get; set; }

        /// <summary>
        /// Площадь.
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// Вариант (тип сделки).
        /// </summary>
        public string Variant { get; set; }

        /// <summary>
        /// Балкон.
        /// </summary>
        public string Balcony { get; set; }

        /// <summary>
        /// Получить тип объекта недвижимости EF.
        /// </summary>
        protected override RealtyObjectType GetRealtyObjectDBType()
        {
            return MigrationContext.ExistingDbRealtyObjectTypes
                .First(t => t.TypeName == "Новостройки");
        }

        /// <summary>
        /// Получить тип объекта недвижимости.
        /// </summary>
        /// <returns>Тип объекта недвижимости.</returns>
        protected override DatabaseMigration.RealtyObjectType GetRealtyObjectType()
        {
            return DatabaseMigration.RealtyObjectType.Dolevoe;
        }

        /// <summary>
        /// Создать специфичные для объекта недвижимости свойства.
        /// </summary>
        protected override IEnumerable<PropertyValue> CreateSpecificProperties()
        {
            return new PropertyValue[]
            {
                new PropertyValue()
                {
                    PropertyType = MigrationContext.ExistingDbPropTypes.First(pt => pt.PropertyName == "Район"),
                    StringValue = this.District
                },

                new PropertyValue()
                {
                    PropertyType = MigrationContext.ExistingDbPropTypes.First(pt => pt.PropertyName == "Квартал"),
                    StringValue = this.Quarter
                },

                new PropertyValue()
                {
                    PropertyType = MigrationContext.ExistingDbPropTypes.First(pt => pt.PropertyName == "Жилой Комплекс"),
                    StringValue = this.HousingEstate
                },

                new PropertyValue()
                {
                    PropertyType = MigrationContext.ExistingDbPropTypes.First(pt => pt.PropertyName == "Комнат"),
                    StringValue = this.QuantityRoom
                },

                new PropertyValue()
                {
                    PropertyType = MigrationContext.ExistingDbPropTypes.First(pt => pt.PropertyName == "Улица"),
                    StringValue = this.Street
                },

                new PropertyValue()
                {
                    PropertyType = MigrationContext.ExistingDbPropTypes.First(pt => pt.PropertyName == "Дом"),
                    StringValue = this.Home
                },

                new PropertyValue()
                {
                    PropertyType = MigrationContext.ExistingDbPropTypes.First(pt => pt.PropertyName == "Этажи"),
                    StringValue = this.Flor
                },

                new PropertyValue()
                {
                    PropertyType = MigrationContext.ExistingDbPropTypes.First(pt => pt.PropertyName == "Планировка"),
                    StringValue = this.Disposition
                },

                new PropertyValue()
                {
                    PropertyType = MigrationContext.ExistingDbPropTypes.First(pt => pt.PropertyName == "Материал Стен"),
                    StringValue = this.WallMaterial
                },

                new PropertyValue()
                {
                    PropertyType = MigrationContext.ExistingDbPropTypes.First(pt => pt.PropertyName == "Площадь [общ/жил/кух]"),
                    StringValue = this.Area
                },

                new PropertyValue()
                {
                    PropertyType = MigrationContext.ExistingDbPropTypes.First(pt => pt.PropertyName == "Вариант"),
                    StringValue = this.Variant
                },

                new PropertyValue()
                {
                    PropertyType = MigrationContext.ExistingDbPropTypes.First(pt => pt.PropertyName == "Балкон"),
                    StringValue = this.Balcony
                },
            };
        }
    }
}
