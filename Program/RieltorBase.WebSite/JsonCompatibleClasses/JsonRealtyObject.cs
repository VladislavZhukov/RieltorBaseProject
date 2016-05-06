namespace RieltorBase.WebSite.JsonCompatibleClasses
{
    using System;
    using System.Collections.Generic;

    using RieltorBase.Domain.Interfaces;

    /// <summary>
    /// Реализация интерфейса <see cref="IRealtyObject"/>,
    /// поддерживающая простую сериализацию JSON.
    /// </summary>
    public class JsonRealtyObject : IRealtyObject
    {
        /// <summary>
        /// Остальные свойства объекта недвижимости.
        /// </summary>
        private readonly Dictionary<string, string> additionalProps
            = new Dictionary<string, string>();

        /// <summary>
        /// Id объекта недвижимости.
        /// </summary>
        public int RealtyObjectId { get; set; }

        /// <summary>
        /// Имя типа объекта недвижимости.
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// Дата.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Дополнительная информация.
        /// </summary>
        public string AdditionalInfo { get; set; }

        /// <summary>
        /// Примечания.
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Название фирмы.
        /// </summary>
        public string FirmName { get; set; }

        /// <summary>
        /// Имя агента.
        /// </summary>
        public string AgentName { get; set; }

        /// <summary>
        /// Телефон агента.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Стоимость.
        /// </summary>
        public int Cost { get; set; }

        /// <summary>
        /// Остальные свойства.
        /// </summary>
        public Dictionary<string, string> AdditionalAttributes
        {
            get { return this.additionalProps; }
        }
    }
}