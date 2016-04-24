namespace RieltorBase.Domain.InfoClasses
{
    using System.Collections.Generic;
    using System.Linq;

    

    /// <summary>
    /// Информация об объекте недвижимости.
    /// </summary>
    public class RealtyObjectInfo
    {
        /// <summary>
        /// Конструктор без параметров нужен для 
        /// использования в Queryable-выражениях.
        /// </summary>
        internal RealtyObjectInfo()
        {
            
        }

        /// <summary>
        /// Стандартный конструктор на основе объекта недвижимости.
        /// </summary>
        /// <param name="realtyObject">Объект недвижимости.</param>
        public RealtyObjectInfo(RealtyObject realtyObject)
        {
            this.Id = realtyObject.RealtyObjectId;

            this.Type = realtyObject.RealtyObjectType.TypeName;

            this.Properties = realtyObject.PropertyValues.ToDictionary(
                value => value.PropertyType.PropertyName, value => value.StringValue);
        }

        public RealtyObject CreateRealtyObject()
        {
            return new RealtyObject()
            {
                RealtyObjectId = this.Id
            };

            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Id объекта недвижимости.
        /// </summary>
        public int Id { get;  set; }

        /// <summary>
        /// Имя типа объекта недвижимости.
        /// </summary>
        public string Type { get;  set; }

        /// <summary>
        /// Свойства объекта недвижимости (имя -> значение).
        /// </summary>
        public Dictionary<string, string> Properties { get;  set; }
    }
}
