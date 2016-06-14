namespace DatabaseMigration.XMLCompatibleClasses
{
    using System;
    using System.Collections.Generic;

    using RieltorBase.Domain;

    /// <summary>
    /// Объект недвижимости, представленный в виде xml.
    /// </summary>
    public abstract class XmlBaseRealtyObject
    {
        /// <summary>
        /// ID объекта недвижимости.
        /// </summary>
        public string IdApartment { get; set; }

        /// <summary>
        /// Дата последнего изменения.
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// Цена.
        /// </summary>
        public string Price { get; set; }

        /// <summary>
        /// Дополнительная информация.
        /// </summary>
        public string AdditionalInfo { get; set; }

        /// <summary>
        /// Примечания.
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// Фирма.
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// Агент.
        /// </summary>
        public string Agent { get; set; }

        /// <summary>
        /// Телефон.
        /// </summary>
        public string PhoneContact { get; set; }

        /// <summary>
        /// Объект "пустой" - т.е. не содержит основных свойств.
        /// </summary>
        internal bool IsEmpty
        {
            get
            {
                return string.IsNullOrWhiteSpace(this.Date)
                    && string.IsNullOrWhiteSpace(this.Price)
                    && string.IsNullOrWhiteSpace(this.AdditionalInfo)
                    && string.IsNullOrWhiteSpace(this.Comments)
                    && string.IsNullOrWhiteSpace(this.Company)
                    && string.IsNullOrWhiteSpace(this.Agent)
                    && string.IsNullOrWhiteSpace(this.PhoneContact);
            }
        }

        /// <summary>
        /// Получить объект недвижимости, включая все свойства
        /// и фотографии, для дальнейшего сохранения в БД.
        /// </summary>
        /// <returns>Объект недвижимости.</returns>
        public RealtyObject GetDbCompatibleFullObject()
        {
            RealtyObject ro = new RealtyObject
            {
                Date = this.Date.GetDateTime(),
                Cost = this.Price.GetIntegerPrice(),
                Note = this.Comments,
                AdditionalInfo = this.AdditionalInfo,
                RealtyObjectType = this.GetRealtyObjectType()
            };
            foreach (PropertyValue value in this.CreateSpecificProperties())
            {
                ro.PropertyValues.Add(value);
            }

            this.AddPhotos(ro);

            return ro;
        }

        /// <summary>
        /// Получить тип объекта недвижимости.
        /// </summary>
        protected abstract RealtyObjectType GetRealtyObjectType();

        /// <summary>
        /// Создать свойства, специфичные для конкретного типа 
        /// объекта недвижимости.
        /// </summary>
        protected abstract IEnumerable<PropertyValue> CreateSpecificProperties();

        /// <summary>
        /// Добавить относительные ссылки на фотографии объекта недвижимости.
        /// </summary>
        /// <param name="realtyObject">Объект недвижимости.</param>
        private void AddPhotos(
            RealtyObject realtyObject)
        {
            // TODO
            // получить папку: this.photoDirectory

            // вычислить все относительные пути к фотографиям

            // добавить фотографии к объекту недвижимости
            
            // throw new NotImplementedException();
        }
    }
}
