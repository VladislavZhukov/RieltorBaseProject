namespace RieltorBase.Domain.Interfaces
{
    /// <summary>
    /// Контекст базы данных недвижимости.
    /// </summary>
    public interface IRealtyBaseContext
    {
        void SaveChanges();

        void ClearDatabase();

        void CreateStandardMetadata();

        void CreateFewObjects();
    }
}
