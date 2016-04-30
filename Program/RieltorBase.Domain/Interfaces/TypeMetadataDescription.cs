namespace RieltorBase.Domain.Interfaces
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    /// <summary>
    /// Описание конкретного типа недвижимости.
    /// </summary>
    internal class TypeMetadataDescription
    {
        /// <summary>
        /// XML-элемент, на основе которого читаются имена свойств.
        /// </summary>
        private readonly XElement xmlElement;

        /// <summary>
        /// Свойства, которые не нужно читать из xml, так как это не свойства, а ссылки
        /// на связанные объекты.
        /// </summary>
        private static string[] notLoadedProperties = new[] {"Фирма", "Агент", "Телефон контакта"};

        /// <summary>
        /// Создать описание типа объекта недвижимости.
        /// </summary>
        /// <param name="typeElement">XML-элемент, описывающий тип недвижимости.</param>
        internal TypeMetadataDescription(XElement typeElement)
        {
            this.xmlElement = typeElement;
        }

        /// <summary>
        /// Имя типа объекта недвижимости.
        /// </summary>
        internal string Name
        {
            get
            {
                return this.xmlElement.Attribute("realtyType").Value;
            }
        }

        /// <summary>
        /// Имена свойств для данного типа.
        /// </summary>
        internal IEnumerable<string> PropertyNames
        {
            get
            {
                return this.xmlElement
                    .Elements("Properties")
                    .Select(el => el.Value)
                    .Where(propName => !notLoadedProperties.Contains(propName))
                    .ToList();
            }
        }
    }
}
