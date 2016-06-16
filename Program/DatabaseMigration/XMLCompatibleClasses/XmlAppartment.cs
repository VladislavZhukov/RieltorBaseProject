namespace DatabaseMigration.XMLCompatibleClasses
{
    using RieltorBase.Domain;
    using System.Linq;

    /// <summary>
    /// Квартира, представленная в xml.
    /// </summary>
    public class XmlAppartment : XmlAppartmentBase
    {
        /// <summary>
        /// Получить тип объекта недвижимости EF.
        /// </summary>
        protected override RealtyObjectType GetRealtyObjectDBType()
        {
            return MigrationContext.ExistingDbRealtyObjectTypes
                .First(t => t.TypeName == "Квартиры");
        }

        /// <summary>
        /// Получить тип объекта недвижимости.
        /// </summary>
        /// <returns>Тип объекта недвижимости.</returns>
        protected override DatabaseMigration.RealtyObjectType GetRealtyObjectType()
        {
            return DatabaseMigration.RealtyObjectType.Kvartiryi;
        }
    }
}
