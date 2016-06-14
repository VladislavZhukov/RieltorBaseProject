namespace DatabaseMigration.XmlCollections
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;

    using DatabaseMigration.XMLCompatibleClasses;
    using RieltorBase.Domain;

    /// <summary>
    /// Коллекция всех объектов недвижимости.
    /// </summary>
    public class XmlRealtyObjectsCollection
    {
        /// <summary>
        /// Папка с исходными данными.
        /// </summary>
        private readonly InitialFolder initialFolder;

        /// <summary>
        /// Объекты недвижимости.
        /// </summary>
        private readonly List<XmlBaseRealtyObject> realtyObjects
            = new List<XmlBaseRealtyObject>();

        /// <summary>
        /// Фирмы (вместе с агентами, объектами недвижимости и фотографиями,
        /// т.е. все данные, необходимые для сохранения).
        /// </summary>
        private readonly List<Firm> firms 
            = new List<Firm>();

        /// <summary>
        /// Создать экземпляр колекции всех xml-объектов недвижимости
        /// и загрузить все объекты из файлов xml.
        /// </summary>
        /// <param name="directory">Папка с исходными данными.</param>
        internal XmlRealtyObjectsCollection(DirectoryInfo directory)
        {
            this.initialFolder = new InitialFolder(directory);
            this.LoadRealtyObjects();
            this.LoadDBCompatibleData();
        }

        /// <summary>
        /// Получить все фирмы (вместе с агентами, объектами недвижимости и 
        /// фотографиями, т.е. все данные, необходимые для сохранения).
        /// </summary>
        /// <returns>Фирмы и все остальные данные вместе с ними.</returns>
        internal IEnumerable<Firm> GetFirms()
        {
            return this.firms;
        }

        /// <summary>
        /// Получить все объекты недвижимости, представленные в виде xml.
        /// </summary>
        /// <returns></returns>
        internal IEnumerable<XmlBaseRealtyObject> GetRealtyObjects()
        {
            return this.realtyObjects;
        }

        /// <summary>
        /// Загрузить все объекты недвижимости.
        /// </summary>
        private void LoadRealtyObjects()
        {
            this.realtyObjects.AddRange(this.Deserialize<XmlArenda>(DatabaseMigration.RealtyObjectType.ArendaZhilyih).RealtyObjects);
            this.realtyObjects.AddRange(this.Deserialize<XmlDacha>(DatabaseMigration.RealtyObjectType.Dachi).RealtyObjects);
            this.realtyObjects.AddRange(this.Deserialize<XmlDolevoye>(DatabaseMigration.RealtyObjectType.Dolevoe).RealtyObjects);
            this.realtyObjects.AddRange(this.Deserialize<XmlDomKottedj>(DatabaseMigration.RealtyObjectType.DomaKottedzhi).RealtyObjects);
            this.realtyObjects.AddRange(this.Deserialize<XmlCommercheskaya>(DatabaseMigration.RealtyObjectType.KommercheskayaNedvizhimost).RealtyObjects);
            this.realtyObjects.AddRange(this.Deserialize<XmlAppartment>(DatabaseMigration.RealtyObjectType.Kvartiryi).RealtyObjects);
            this.realtyObjects.AddRange(this.Deserialize<XmlMalosemeyka>(DatabaseMigration.RealtyObjectType.Malosemeyki).RealtyObjects);
            this.realtyObjects.AddRange(this.Deserialize<XmlRaznoye>(DatabaseMigration.RealtyObjectType.Raznoe).RealtyObjects);
            this.realtyObjects.AddRange(this.Deserialize<XmlUchastok>(DatabaseMigration.RealtyObjectType.Uchastki).RealtyObjects);

            this.RemoveEmptyObjects();
        }

        /// <summary>
        /// Получить коллекцию объектов недвижимости определенного типа
        /// из соответствующего xml-документа.
        /// </summary>
        /// <typeparam name="TElement">Тип (класс) объектов недвижимости.</typeparam>
        /// <param name="type">Тип объектов недвижимости.</param>
        /// <returns>Десериализованная коллекция объектов недвижимости
        /// определенного типа.</returns>
        private SingleSerializableCollection<TElement> Deserialize<TElement>(
            DatabaseMigration.RealtyObjectType type)
            where TElement : XmlBaseRealtyObject
        {
            XmlSerializer serializer =
                new XmlSerializer(typeof(SingleSerializableCollection<TElement>));

            using (FileStream fileStream =
                new FileStream(this.initialFolder.GetXmlDoc(type), FileMode.Open))
            {
                return (SingleSerializableCollection<TElement>)serializer
                    .Deserialize(fileStream);
            }
        }

        /// <summary>
        /// Удалить пустые квартиры из списка всех квартир.
        /// </summary>
        private void RemoveEmptyObjects()
        {
            List<XmlBaseRealtyObject> emptyObjects = this.realtyObjects
                .Where(ro => ro.IsEmpty)
                .ToList();

            foreach (XmlBaseRealtyObject emptyObject in emptyObjects)
            {
                this.realtyObjects.Remove(emptyObject);
            };
        }

        /// <summary>
        /// Получить все фирмы (вместе с агентами), используя
        /// информацию из загруженных объектов недвижимости.
        /// </summary>
        private void LoadDBCompatibleData()
        {
            foreach (XmlBaseRealtyObject realtyObject in this.realtyObjects)
            {
                Firm firm = this.FindOrCreateFirm(
                    realtyObject.Company, 
                    realtyObject.PhoneContact);

                string agentNameAndPhone = !string.IsNullOrWhiteSpace(realtyObject.Agent)
                    ? realtyObject.Agent
                    : realtyObject.Company + " " + realtyObject.PhoneContact;

                if (agentNameAndPhone.StartsWith("ФЛ"))
                {
                    agentNameAndPhone = realtyObject.Company;
                }

                Agent agent = XmlRealtyObjectsCollection.FindOrCreateAgent(
                    firm,
                    agentNameAndPhone.GetAgentName(),
                    agentNameAndPhone.GetAgentPhone());

                agent.RealtyObjects.Add(realtyObject.GetDbCompatibleFullObject());
            }
        }

        /// <summary>
        /// Найти существующую или создать новую фирму.
        /// </summary>
        /// <param name="firmName">Название фирмы.</param>
        /// <param name="firmPhone">Телефон фирмы.</param>
        /// <returns>Существующая или новая (созданная) фирма.</returns>
        private Firm FindOrCreateFirm(string firmName, string firmPhone)
        {
            if (string.IsNullOrWhiteSpace(firmPhone))
            {
                firmPhone = "-";
            }

            if (firmName.Contains("Волга-Инфо (сайт)"))
            {
                firmName = "ВИ Сайт";
            }

            Firm firm = this.firms.FirstOrDefault(f =>
                f.Name == firmName && f.Phone == firmPhone);

            if (firm == null)
            {
                firm = new Firm()
                {
                    Name = firmName,
                    Phone = firmPhone,
                    Address = "-"
                };

                firms.Add(firm);
            }

            return firm;
        }

        /// <summary>
        /// Добавить агента к фирме, если фирма еще не содержит такого агента.
        /// </summary>
        /// <param name="firm">Фирма.</param>
        /// <param name="agentName">Имя агента.</param>
        /// <param name="agentPhone">Телефон агента.</param>
        private static Agent FindOrCreateAgent(
            Firm firm,
            string agentName,
            string agentPhone)
        {
            Agent existingAgent = firm.Agents.FirstOrDefault(a =>
                a.Name == agentName && a.PhoneNumber == agentPhone);

            if (existingAgent != null)
            {
                return existingAgent;
            }

            Agent newAgent = new Agent()
            {
                Name = agentName,
                PhoneNumber = agentPhone,
                LastName = "-",
                Addres = "-"
            };

            firm.Agents.Add(newAgent);

            return newAgent;
        }
    }
}