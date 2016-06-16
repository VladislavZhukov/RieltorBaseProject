namespace DatabaseMigration.XMLCompatibleClasses
{
    using System.Collections.Generic;
    using System.Linq;

    using RieltorBase.Domain;

    /// <summary>
    /// Дом/коттедж, представленный в xml.
    /// </summary>
    public class XmlDomKottedj : XmlBaseRealtyObject
    {
        /// <summary>
        /// Готовность.
        /// </summary>
        public string Readiness { get; set; }

        /// <summary>
        /// Район.
        /// </summary>
        public string District { get; set; }

        /// <summary>
        /// Адрес.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Этажей.
        /// </summary>
        public string Floors { get; set; }

        // ReSharper disable once InconsistentNaming - для соответствия XML
        /// <summary>
        /// Площадь дома.
        /// </summary>
        public string S_Houses { get; set; }

        // ReSharper disable once InconsistentNaming - для соответствия XML
        /// <summary>
        /// Площадь участка.
        /// </summary>
        public string S_Site { get; set; }

        /// <summary>
        /// Материал стен.
        /// </summary>
        public string WallMaterial { get; set; }

        /// <summary>
        /// Получить тип объекта недвижимости EF.
        /// </summary>
        protected override RealtyObjectType GetRealtyObjectDBType()
        {
            return MigrationContext.ExistingDbRealtyObjectTypes
                .First(t => t.TypeName == "Дома/Коттеджи");
        }

        /// <summary>
        /// Получить тип объекта недвижимости.
        /// </summary>
        /// <returns>Тип объекта недвижимости.</returns>
        protected override DatabaseMigration.RealtyObjectType GetRealtyObjectType()
        {
            return DatabaseMigration.RealtyObjectType.DomaKottedzhi;
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
                    PropertyType = MigrationContext.ExistingDbPropTypes.First(pt => pt.PropertyName == "Готовность"),
                    StringValue = this.Readiness
                },

                new PropertyValue()
                {
                    PropertyType = MigrationContext.ExistingDbPropTypes.First(pt => pt.PropertyName == "Район"),
                    StringValue = this.District
                },

                new PropertyValue()
                {
                    PropertyType = MigrationContext.ExistingDbPropTypes.First(pt => pt.PropertyName == "Адрес"),
                    StringValue = this.Address
                },

                new PropertyValue()
                {
                    PropertyType = MigrationContext.ExistingDbPropTypes.First(pt => pt.PropertyName == "Этажей"),
                    StringValue = this.Floors
                },

                new PropertyValue()
                {
                    PropertyType = MigrationContext.ExistingDbPropTypes.First(pt => pt.PropertyName == "S Дома"),
                    StringValue = this.S_Houses
                },

                new PropertyValue()
                {
                    PropertyType = MigrationContext.ExistingDbPropTypes.First(pt => pt.PropertyName == "S участка"),
                    StringValue = this.S_Site
                },

                new PropertyValue()
                {
                    PropertyType = MigrationContext.ExistingDbPropTypes.First(pt => pt.PropertyName == "Материал Стен"),
                    StringValue = this.WallMaterial
                }
            };
        }
    }
}
