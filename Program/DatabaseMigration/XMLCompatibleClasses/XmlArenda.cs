namespace DatabaseMigration.XMLCompatibleClasses
{
    using System.Linq;
    
    using RieltorBase.Domain;

    public class XmlArenda : XmlAppartmentBase
    {
        /// <summary>
        /// Получить тип объекта недвижимости EF.
        /// </summary>
        protected override RealtyObjectType GetRealtyObjectDBType()
        {
            return MigrationContext.ExistingDbRealtyObjectTypes
                .First(t => t.TypeName == "Аренда жилья");
        }

        /// <summary>
        /// Получить тип объекта недвижимости.
        /// </summary>
        /// <returns>Тип объекта недвижимости.</returns>
        protected override DatabaseMigration.RealtyObjectType GetRealtyObjectType()
        {
            return DatabaseMigration.RealtyObjectType.ArendaZhilyih;
        }
    }
}
