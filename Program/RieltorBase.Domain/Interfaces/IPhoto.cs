namespace RieltorBase.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс фотографии.
    /// </summary>
    public interface IPhoto
    {
        /// <summary>
        /// Id фотографии.
        /// </summary>
        int PhotoId { get; set; }

        /// <summary>
        /// Id объекта недвижимости, к которому относится фотография
        /// (если это фотография объекта недвижимости).
        /// </summary>
        /// <remarks></remarks>
        int RealtyObjectId { get; set; }

        /// <summary>
        /// Id фирмы, к которой относится фотография 
        /// (если это фотография фирмы).
        /// </summary>
        int FirmId { get; set; }

        /// <summary>
        /// Относительный путь к самой фотографии (например, часть URL).
        /// </summary>
        string RelativeSource { get; set; }
    }
}
