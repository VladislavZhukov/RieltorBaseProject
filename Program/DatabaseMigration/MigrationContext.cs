namespace DatabaseMigration
{
    using System.Collections.Generic;
    using System.Linq;

    using RieltorBase.Domain;

    /// <summary>
    /// Контекст миграции данных.
    /// </summary>
    public static class MigrationContext
    {
        /// <summary>
        /// Контекст базы данных недвижимости.
        /// </summary>
        private static readonly VolgaInfoDBEntities context
            = new VolgaInfoDBEntities();

        /// <summary>
        /// Список типов объектов недвижимости из базы данных.
        /// </summary>
        private static readonly List<RieltorBase.Domain.RealtyObjectType> existingDbRealtyObjectTypes
            = MigrationContext.context.RealtyObjectTypes.ToList();

        /// <summary>
        /// Список типов свойств из базы данных.
        /// </summary>
        private static readonly List<PropertyType> existingDbPropTypes
            = MigrationContext.context.PropertyTypes.ToList();

        /// <summary>
        /// Контекст базы данных недвижимости.
        /// </summary>
        public static VolgaInfoDBEntities DbContext
        {
            get
            {
                return MigrationContext.context;
            }
        }

        /// <summary>
        /// Список типов объектов недвижимости из базы данных.
        /// </summary>
        public static IEnumerable<RieltorBase.Domain.RealtyObjectType> ExistingDbRealtyObjectTypes 
        {
            get
            {
                return MigrationContext.existingDbRealtyObjectTypes;
            }
        }

        /// <summary>
        /// Список типов свойств из базы данных.
        /// </summary>
        public static IEnumerable<PropertyType> ExistingDbPropTypes
        {
            get
            {
                return MigrationContext.existingDbPropTypes;
            }
        }
    }
}
