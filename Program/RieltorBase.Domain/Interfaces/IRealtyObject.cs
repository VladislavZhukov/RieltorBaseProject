namespace RieltorBase.Domain.Interfaces
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Интерфейс, общий для всех объектов недвижимости.
    /// </summary>
    public interface IRealtyObject
    {
        /// <summary>
        /// Id объекта недвижимости.
        /// </summary>
        int RealtyObjectId { get; set; }

        /// <summary>
        /// Тип объекта недвижимости.
        /// </summary>
        string TypeName { get; set; }

        /// <summary>
        /// Дата.
        /// </summary>
        DateTime Date { get; set; }

        /// <summary>
        /// Дополнительная информация.
        /// </summary>
        string AdditionalInfo { get; set; }

        /// <summary>
        /// Примечание.
        /// </summary>
        string Note { get; set; }

        /// <summary>
        /// Фирма.
        /// </summary>
        string FirmName { get; set; }

        /// <summary>
        /// Агент.
        /// </summary>
        string AgentName { get; set; }

        /// <summary>
        /// Телефон контакта.
        /// </summary>
        string Phone { get; set; }

        /// <summary>
        /// Цена.
        /// </summary>
        string Cost { get; set; }

        /// <summary>
        /// Все остальные свойства.
        /// </summary>
        Dictionary<string, string> AdditionalAttributes { get; }
    }
}
