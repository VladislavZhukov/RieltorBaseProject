namespace RieltorBase.WebSite.JsonCompatibleClasses
{
    using RieltorBase.Domain.Interfaces;

    /// <summary>
    /// Реализация <see cref="IPhoto"/>, поддерживающая 
    /// простую сериализацию JSON.
    /// </summary>
    public class JsonPhoto : IPhoto
    {
        /// <summary>
        /// Id фотографии.
        /// </summary>
        public int PhotoId { get; set; }

        /// <summary>
        /// Id объекта недвижимости.
        /// </summary>
        public int RealtyObjectId { get; set; }

        /// <summary>
        /// Id фирмы.
        /// </summary>
        public int FirmId { get; set; }

        /// <summary>
        /// Ссылка на фотографию.
        /// </summary>
        public string RelativeSource { get; set; }
    }
}