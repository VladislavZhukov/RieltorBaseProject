namespace RieltorBase.Domain.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// Интерфейс репозитория фотографий.
    /// </summary>
    public interface IPhotosRepository : IRepository<IPhoto>
    {
        /// <summary>
        /// Получить все фотографии определенной фирмы.
        /// </summary>
        /// <param name="firmId">Id фирмы.</param>
        /// <returns>Фотографии фирмы.</returns>
        IEnumerable<IPhoto> GetFirmPhotos(int firmId);

        /// <summary>
        /// Получить все фотографии объекта недвижимости.
        /// </summary>
        /// <param name="realtyObjectId">Id объекта недвижимости.</param>
        /// <returns>Фотографии объекта недвижимости.</returns>
        IEnumerable<IPhoto> GetRealtyObjectPhotos(int realtyObjectId);
    }
}
