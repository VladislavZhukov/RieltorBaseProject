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
        protected override RealtyObjectType GetRealtyObjectDBType()
        {
            return MigrationContext.ExistingDbRealtyObjectTypes
                .First(t => t.TypeName == "Малосемейки");
        }

        /// <summary>
        /// Получить тип объекта недвижимости.
        /// </summary>
        /// <returns>Тип объекта недвижимости.</returns>
        protected override DatabaseMigration.RealtyObjectType GetRealtyObjectType()
        {
            return DatabaseMigration.RealtyObjectType.Malosemeyki;
        }
    }
}
