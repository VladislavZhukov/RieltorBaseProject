namespace DatabaseMigration.XmlCollections
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    using DatabaseMigration.XMLCompatibleClasses;

    /// <summary>
    /// Класс для десериализации xml-документов разных типов 
    /// объектов недвижимости.
    /// </summary>
    /// <typeparam name="TElement">Тип объекта недвижимости.</typeparam>
    [XmlRoot("ListOfApartment")]
    public class SingleSerializableCollection<TElement>
        where TElement : XmlBaseRealtyObject
    {
        /// <summary>
        /// Объекты недвижимости.
        /// </summary>
        [XmlElement("Apartment")]
        public List<TElement> RealtyObjects { get; set; }
    }
}
