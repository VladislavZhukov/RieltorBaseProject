namespace DatabaseMigration.XMLCompatibleClasses
{
    using System.Collections.Generic;
    using System.Linq;

    using RieltorBase.Domain;

    /// <summary>
    /// Коммерческая недвижимость, представленная в xml.
    /// </summary>
    public class XmlCommercheskaya : XmlBaseRealtyObject
    {
        /// <summary>
        /// Тип сделки.
        /// </summary>
        public string TypeTransaction { get; set; }

        /// <summary>
        /// Назначение.
        /// </summary>
        public string Appointment { get; set; }

        /// <summary>
        /// Район.
        /// </summary>
        public string District { get; set; }

        /// <summary>
        /// Адрес.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Этаж.
        /// </summary>
        public string Flor { get; set; }

        /// <summary>
        /// Площадь всего.
        /// </summary>
        public string EntireArea { get; set; }

        /// <summary>
        /// Цена за метр.
        /// </summary>
        public string PriceMeter { get; set; }

        /// <summary>
        /// Получить тип объекта недвижимости EF.
        /// </summary>
        protected override RealtyObjectType GetRealtyObjectType()
        {
            return MigrationContext.ExistingDbRealtyObjectTypes
                .First(t => t.TypeName == "Коммерческая Недвижимость");
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
                    PropertyType = MigrationContext.ExistingDbPropTypes.First(pt => pt.PropertyName == "Тип Сделки"),
                    StringValue = this.TypeTransaction
                },

                new PropertyValue()
                {
                    PropertyType = MigrationContext.ExistingDbPropTypes.First(pt => pt.PropertyName == "Назначение"),
                    StringValue = this.Appointment
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
                    PropertyType = MigrationContext.ExistingDbPropTypes.First(pt => pt.PropertyName == "Этаж"),
                    StringValue = this.Flor
                },

                new PropertyValue()
                {
                    PropertyType = MigrationContext.ExistingDbPropTypes.First(pt => pt.PropertyName == "Площадь всего"),
                    StringValue = this.EntireArea
                },

                new PropertyValue()
                {
                    PropertyType = MigrationContext.ExistingDbPropTypes.First(pt => pt.PropertyName == "Цена метра"),
                    StringValue = this.PriceMeter
                }
            };
        }
    }
}
