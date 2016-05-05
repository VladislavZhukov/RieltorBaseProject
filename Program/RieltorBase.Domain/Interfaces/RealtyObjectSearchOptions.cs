namespace RieltorBase.Domain.Interfaces
{
    using System;

    /// <summary>
    /// Опции поиска объектов недвижимости.
    /// </summary>
    public class RealtyObjectSearchOptions
    {
        /// <summary>
        /// Тип объекта недвижимости.
        /// </summary>
        public string RealtyObjectType { get; set; }

        /// <summary>
        /// Часть адреса.
        /// </summary>
        public string PartOfAddress { get; set; }

        /// <summary>
        /// Минимальная стоимость.
        /// </summary>
        public int MinCost { get; set; }

        /// <summary>
        /// Максимальная стоимость.
        /// </summary>
        public int MaxCost { get; set; }

        /// <summary>
        /// Минимальная дата.
        /// </summary>
        public DateTime? MinDate { get; set; }

        /// <summary>
        /// Максимальная дата.
        /// </summary>
        public DateTime? MaxDate { get; set; }
    }
}
