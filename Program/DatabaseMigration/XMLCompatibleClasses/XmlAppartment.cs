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
        protected override RealtyObjectType GetRealtyObjectType()
        {
            return MigrationContext.ExistingDbRealtyObjectTypes
                .First(t => t.TypeName == "Квартиры");
        }
    }
}
