namespace DatabaseMigration.XMLCompatibleClasses
{
    using System.Collections.Generic;
    using System.Linq;

    using RieltorBase.Domain;

    /// <summary>
    /// Объекты недвижимости "Разное", представленные в xml.
    /// </summary>
    public class XmlRaznoye : XmlBaseRealtyObject
    {
        /// <summary>
        /// Тип помещения.
        /// </summary>
        public string TypeRoom { get; set; }

        /// <summary>
        /// Адрес.
        /// </summary>
        public string Address { get; set; }

        // ReSharper disable once InconsistentNaming - для соответствия XML
        /// <summary>
        /// Площадь дома.
        /// </summary>
        public string S_Houses { get; set; }

        /// <summary>
        /// Материал стен.
        /// </summary>
        public string WallMaterial { get; set; }

        /// <summary>
        /// Получить тип объекта недвижимости EF.
        /// </summary>
        protected override RealtyObjectType GetRealtyObjectType()
        {
            return MigrationContext.ExistingDbRealtyObjectTypes
                .First(t => t.TypeName == "Разное");
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
                    PropertyType = MigrationContext.ExistingDbPropTypes.First(pt => pt.PropertyName == "Тип помещения"),
                    StringValue = this.TypeRoom
                },

                new PropertyValue()
                {
                    PropertyType = MigrationContext.ExistingDbPropTypes.First(pt => pt.PropertyName == "Адрес"),
                    StringValue = this.Address
                },

                new PropertyValue()
                {
                    PropertyType = MigrationContext.ExistingDbPropTypes.First(pt => pt.PropertyName == "S Дома"),
                    StringValue = this.S_Houses
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
