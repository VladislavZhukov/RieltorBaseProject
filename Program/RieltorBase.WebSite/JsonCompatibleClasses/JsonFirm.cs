namespace RieltorBase.WebSite.JsonCompatibleClasses
{
    using RieltorBase.Domain.Interfaces;

    /// <summary>
    /// Реализация <see cref="IFirm"/>, поддерживающая 
    /// простую сериализацию JSON.
    /// </summary>
    public class JsonFirm : IFirm
    {
        /// <summary>
        /// Id фирмы.
        /// </summary>
        public int FirmId { get; set; }

        /// <summary>
        /// Имя фирмы.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Адрес фирмы.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Телефон фирмы.
        /// </summary>
        public string Phone { get; set; }
    }
}