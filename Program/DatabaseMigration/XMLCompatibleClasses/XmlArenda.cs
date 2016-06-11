namespace DatabaseMigration.XMLCompatibleClasses
{
    using System.Linq;
    
    using RieltorBase.Domain;

    public class XmlArenda : XmlAppartmentBase
    {
        /// <summary>
        /// Получить тип объекта недвижимости EF.
        /// </summary>
        protected override RealtyObjectType GetRealtyObjectType()
        {
            return MigrationContext.ExistingDbRealtyObjectTypes
                .First(t => t.TypeName == "Аренда жилья");
        }
    }
}
