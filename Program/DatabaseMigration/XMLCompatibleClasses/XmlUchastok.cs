namespace DatabaseMigration.XMLCompatibleClasses
{
    using System.Collections.Generic;
    using System.Linq;

    using RieltorBase.Domain;

    /// <summary>
    /// Земельный участок, представленный в xml.
    /// </summary>
    public class XmlUchastok : XmlBaseRealtyObject
    {
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
        /// Площадь всего.
        /// </summary>
        public string EntireArea { get; set; }

        /// <summary>
        /// Получить тип объекта недвижимости EF.
        /// </summary>
        protected override RealtyObjectType GetRealtyObjectType()
        {
            return MigrationContext.ExistingDbRealtyObjectTypes
                .First(t => t.TypeName == "Участки");
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
                    PropertyType = MigrationContext.ExistingDbPropTypes.First(pt => pt.PropertyName == "Площадь всего"),
                    StringValue = this.EntireArea
                }
            };
        }
    }
}
