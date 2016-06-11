namespace DatabaseMigration.XMLCompatibleClasses
{
    using System.Linq;

    using RieltorBase.Domain;

    /// <summary>
    /// Малосемейка, представленная в xml.
    /// </summary>
    public class XmlMalosemeyka : XmlAppartmentBase
    {
        /// <summary>
        /// Получить тип объекта недвижимости EF.
        /// </summary>
        protected override RealtyObjectType GetRealtyObjectType()
        {
            return MigrationContext.ExistingDbRealtyObjectTypes
                .First(t => t.TypeName == "Малосемейки");
        }
    }
}
