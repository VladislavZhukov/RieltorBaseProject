namespace RieltorBase.Domain.Interfaces
{
    /// <summary>
    /// Контекст базы данных недвижимости.
    /// </summary>
    public interface IRealtyBaseContext
    {
        /// <summary>
        /// Сохранить все изменения.
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Удалить все данные из базы данных.
        /// </summary>
        void ClearDatabase();

        /// <summary>
        /// Создать стандартные данные: типы объектов недвижимости,
        /// типы свойств и их отношения.
        /// </summary>
        void CreateStandardMetadata();

        /// <summary>
        /// Создать несколько тестовых объектов 
        /// (фирм, агентов, объектов недвижимости).
        /// </summary>
        void CreateFewObjects();
    }
}
