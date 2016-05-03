namespace RieltorBase.Domain.EntityFrameworkImpl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using RieltorBase.Domain.Interfaces;

    /// <summary>
    /// Обертка класса <see cref="RealtyObject"/>, реализующая 
    /// интерфейс <see cref="IRealtyObject"/>.
    /// </summary>
    public class RealtyObjectWrap : IWrapBase<RealtyObject>, IRealtyObject
    {
        /// <summary>
        /// EF-объект недвижимости.
        /// </summary>
        private readonly RealtyObject realtyObjectEF;

        /// <summary>
        /// Контекст не создается, а передается в конструкторе,
        /// т.к. медленно создавать контекст при каждом создании
        /// обертки объекта недвижимости (например, если 
        /// выполняется поиск 500 объектов недвижимости).
        /// ДОЛЖЕН ИСПОЛЬЗОВАТЬСЯ ТОЛЬКО ДЛЯ ПОИСКА, т.к. нет
        /// гарантий сохранения контекста.
        /// </summary>
        private readonly VolgaInfoDBEntities context;

        /// <summary>
        /// Общая часть конструктора.
        /// </summary>
        /// <param name="context">EF-контекст.</param>
        private RealtyObjectWrap(
            VolgaInfoDBEntities context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            this.context = context;
        }

        /// <summary>
        /// Стандартный конструктор на основе объекта недвижимости.
        /// </summary>
        /// <param name="realtyObject">Объект недвижимости.</param>
        /// <param name="context">Контекст базы данных недвижимости.</param>
        internal RealtyObjectWrap(
            RealtyObject realtyObject,
            VolgaInfoDBEntities context)
            : this(context)
        {
            this.realtyObjectEF = realtyObject;
        }

        /// <summary>
        /// Конструктор на основе любого объекта недвижимости (т.е.
        /// интерфейса <see cref="IRealtyObject"/>).
        /// </summary>
        /// <param name="iRealtyObj">Интерфейс объекта недвижимости.</param>
        /// <param name="context">EF-контекст базы данных.</param>
        internal RealtyObjectWrap(
            IRealtyObject iRealtyObj,
            VolgaInfoDBEntities context)
            : this(context)
        {
            this.realtyObjectEF = new RealtyObject();
            this.realtyObjectEF.RealtyObjectType = 
                this.FindType(iRealtyObj.TypeName);
            this.realtyObjectEF.RealtyObjectTypeId = 
                this.FindType(iRealtyObj.TypeName).RealtyObjectTypeId;
            Agent agent = this.FindAgent(iRealtyObj.FirmName, iRealtyObj.AgentName, iRealtyObj.Phone);
            this.realtyObjectEF.Agent = agent;
            this.realtyObjectEF.AgentId = agent.Id_agent;

            this.realtyObjectEF.RealtyObjectId = iRealtyObj.RealtyObjectId;
            this.SetPropertyValue(Metadata.DatePropName, iRealtyObj.Date.ToShortDateString());
            this.SetPropertyValue(Metadata.GetCostPropertyName(this.TypeName), iRealtyObj.Cost);
            this.SetPropertyValue(Metadata.NotePropName, iRealtyObj.Note);
            this.SetPropertyValue(Metadata.AdditionalInfoPropName, iRealtyObj.AdditionalInfo);

            this.SetPropertyValues(iRealtyObj.AdditionalAttributes);
        }

        /// <summary>
        /// Id объекта недвижимости.
        /// </summary>
        public int RealtyObjectId
        {
            get
            {
                return this.realtyObjectEF.RealtyObjectId;
            }

            set
            {
                this.realtyObjectEF.RealtyObjectId = value;
            }
        }

        /// <summary>
        /// Имя типа объекта недвижимости.
        /// </summary>
        public string TypeName
        {
            get
            {
                return this.realtyObjectEF.RealtyObjectType.TypeName;
            }

            set
            {
                throw new NotSupportedException(
                    "Имя типа необходимо устанавливать через объект типа.");
            }
        }

        /// <summary>
        /// Дата.
        /// </summary>
        public DateTime Date
        {
            get
            {
                return Convert.ToDateTime(
                    this.GetPropertyValue(Metadata.DatePropName));
            }

            set
            {
                this.SetPropertyValue(
                    Metadata.DatePropName, 
                    value.ToShortDateString());
            }
        }

        /// <summary>
        /// Дополнительная информация.
        /// </summary>
        public string AdditionalInfo
        {
            get
            {
                return this.GetPropertyValue(
                    Metadata.AdditionalInfoPropName);
            }

            set
            {
                this.SetPropertyValue(
                    Metadata.AdditionalInfoPropName,
                    value);
            }
        }

        /// <summary>
        /// Примечания.
        /// </summary>
        public string Note
        {
            get
            {
                return this.GetPropertyValue(Metadata.NotePropName);
            }

            set
            {
                this.SetPropertyValue(Metadata.NotePropName, value);
            }
        }

        /// <summary>
        /// Название фирмы.
        /// </summary>
        public string FirmName
        {
            get
            {
                return this.realtyObjectEF.Agent.Firm.Name;
            }
            set
            {
                throw new NotSupportedException(
                    "Имя фирмы необходимо устанавливать через объект фирмы.");
            }
        }

        /// <summary>
        /// Имя агента.
        /// </summary>
        public string AgentName
        {
            get
            {
                return this.realtyObjectEF.Agent.Name;
            }
            set
            {
                throw new NotSupportedException(
                    "Имя агента необходимо устанавливать через объект агента.");
            }
        }

        /// <summary>
        /// Телефон агента.
        /// </summary>
        public string Phone
        {
            get
            {
                return this.realtyObjectEF.Agent.PhoneNumber;
            }

            set
            {
                throw new NotSupportedException(
                    "Телефон агента необходимо устанавливать через объект агента.");
            }
        }

        /// <summary>
        /// Стоимость.
        /// </summary>
        public string Cost
        {
            get
            {
                return this.GetPropertyValue(
                    Metadata.GetCostPropertyName(this.TypeName));
            }
            set
            {
                this.SetPropertyValue(
                    Metadata.GetCostPropertyName(this.TypeName), 
                    value);
            }
        }

        /// <summary>
        /// Свойства объекта недвижимости (имя -> значение).
        /// </summary>
        public Dictionary<string, string> AdditionalAttributes
        {
            get
            {
                return Metadata.GetAdditionalAttrNames(
                    this.realtyObjectEF.RealtyObjectType.TypeName)
                    .Select(name => new KeyValuePair<string, string>(
                        name,
                        this.GetPropertyValue(name)))
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            }
        }

        /// <summary>
        /// Получить объект недвижимости EF.
        /// </summary>
        /// <returns></returns>
        public RealtyObject GetRealObject()
        {
            return this.realtyObjectEF;
        }

        /// <summary>
        /// Установить значения свойств.
        /// </summary>
        /// <param name="properties">Набор значений 
        /// "имя свойства => значение свойства".</param>
        private void SetPropertyValues(Dictionary<string, string> properties)
        {
            string[] propNames = Metadata.GetAdditionalAttrNames(this.TypeName);

            foreach (string propName in propNames)
            {
                string value = properties.ContainsKey(propName)
                    ? properties[propName]
                    : string.Empty;

                this.SetPropertyValue(propName, value);
            }
        }

        /// <summary>
        /// Получить свойство с определенным именем.
        /// </summary>
        /// <param name="propName">Имя свойства.</param>
        /// <returns>Свойство (EF).</returns>
        private PropertyValue GetProperty(string propName)
        {
            PropertyValue currentValue = this.realtyObjectEF.PropertyValues
                .FirstOrDefault(prValue => prValue.PropertyType.PropertyName == propName);
            return currentValue;
        }

        /// <summary>
        /// Получить значение свойства.
        /// </summary>
        /// <param name="propName">Имя свойства.</param>
        /// <returns>Строковое значение свойства.</returns>
        private string GetPropertyValue(string propName)
        {
            PropertyValue value =
                this.realtyObjectEF.PropertyValues.FirstOrDefault(
                    pv => pv.PropertyType.PropertyName == propName);

            return value != null ? value.StringValue : null;
        }

        /// <summary>
        /// Установить значение свойства.
        /// </summary>
        /// <param name="propName">Имя свойства.</param>
        /// <param name="propValue">Значение свойства.</param>
        private void SetPropertyValue(string propName, string propValue)
        {
            if (string.IsNullOrWhiteSpace(propValue))
            {
                this.RemoveProperty(propName);
                return;
            }

            if (this.GetProperty(propName) != null)
            {
                this.GetProperty(propName).StringValue = propValue;
            }
            else
            {
                PropertyValue value = new PropertyValue()
                {
                    PropertyTypeId = this.GetPropertyType(propName).PropertyTypeId,
                    PropertyType = this.GetPropertyType(propName),
                    RealtyObjectId = this.realtyObjectEF.RealtyObjectId,
                    RealtyObject = this.realtyObjectEF,
                    StringValue = propValue
                };

                this.realtyObjectEF.PropertyValues.Add(value);
            }
        }

        /// <summary>
        /// Удалить свойство.
        /// </summary>
        /// <param name="propName">Имя свойства.</param>
        private void RemoveProperty(string propName)
        {
            if (this.GetProperty(propName) != null)
            {
                this.realtyObjectEF.PropertyValues.Remove(
                    this.GetProperty(propName));
            }
        }

        /// <summary>
        /// Найти тип свойства.
        /// </summary>
        /// <param name="propTypeName">Имя типа свойства.</param>
        /// <returns>Найденный тип свойства.</returns>
        private PropertyType GetPropertyType(string propTypeName)
        {
            PropertyType result = 
                this.context.PropertyTypes.FirstOrDefault(pt =>
                    pt.PropertyName == propTypeName);

            if (result == null)
            {
                throw new InvalidOperationException(
                    "Свойства с именем " + propTypeName + " не существует в БД.");
            }

            return result;
        }

        /// <summary>
        /// Найти тип объектов недвижимости.
        /// </summary>
        /// <param name="typeName">Имя типа объектов недвижимости.</param>
        /// <returns>Найденный тип объектов недвижимости.</returns>
        private RealtyObjectType FindType(string typeName)
        {
            RealtyObjectType result =
                this.context.RealtyObjectTypes.FirstOrDefault(rot =>
                    rot.TypeName == typeName);

            if (result == null)
            {
                throw new InvalidOperationException(
                    "Типа с именем " + typeName + " не существует в БД.");
            }

            return result;
        }

        /// <summary>
        /// Найти агента недвижимости.
        /// </summary>
        /// <param name="firmName">Название фирмы.</param>
        /// <param name="agentName">Имя агента.</param>
        /// <param name="phone">Телефон агента.</param>
        /// <returns>Агент недвижимости.</returns>
        /// <exception cref="InvalidOperationException">
        /// Агента с такими параметрами не существует.
        /// </exception>
        private Agent FindAgent(string firmName, string agentName, string phone)
        {
            Firm firm = this.context.Firms.FirstOrDefault(f =>
                f.Name == firmName);

            if (firm == null)
            {
                throw new InvalidOperationException(
                    "Фирмы с именем " + firmName + " не существует в БД.");
            }

            Agent agent = this.context.Agents.FirstOrDefault(a =>
                a.PhoneNumber == phone && a.Name == agentName);

            if (agent == null)
            {
                string message = string.Format(
                    "Агента с именем {0} и телефоном {1} не существует в фирме {2}",
                    agentName,
                    phone,
                    firmName);

                throw new InvalidOperationException(message);
            }

            return agent;
        }
    }
}
