namespace RieltorBase.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс фирмы - агентства недвижимости.
    /// </summary>
    public interface IFirm
    {
        /// <summary>
        /// Уникальный идентификатор фирмы.
        /// </summary>
        int FirmId { get; set; }

        /// <summary>
        /// Название фирмы.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Адрес фирмы.
        /// </summary>
        string Address { get; set; }

        /// <summary>
        /// Телефон фирмы.
        /// </summary>
        string Phone { get; set; }
    }
}
