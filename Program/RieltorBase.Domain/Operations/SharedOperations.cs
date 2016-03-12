namespace RieltorBase.Domain.Operations
{
    /// <summary>
    /// Операции, которые может выполнить любой пользователь.
    /// </summary>
    /// <remarks>
    /// Эти операции не записываются в журнал.
    /// </remarks>
    public class SharedOperations
    {
        /// <summary>
        /// Получить квартиры по параметрам поиска.
        /// </summary>
        /// <param name="searchOptions">Параметры поиска.</param>
        /// <returns>Найденные квартиры.</returns>
        public object[] FindAppartments(
            AppartmentSearchOptions searchOptions)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Получить квартиры, используя быстрый поиск по строке.
        /// </summary>
        /// <param name="searchString">Параметр поиска - обычная строка.</param>
        /// <returns>Найденные квартиры.</returns>
        public object[] FindAppartmentsFast(string searchString)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Получить информацию о всех фирмах.
        /// </summary>
        public FirmInfo[] GetFirms()
        {
            throw new System.NotImplementedException();
        }
    }
}
