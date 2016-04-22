namespace RieltorBase.Domain.MetadataInfo
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Xml.Linq;

    /// <summary>
    /// Метаданные (описание типов и всех свойств каждого типа).
    /// </summary>
    internal class Metadata
    {
        /// <summary>
        /// Экземпляр класса метаданных.
        /// </summary>
        private static Metadata instance;

        /// <summary>
        /// Получить единственный экземпляр класса метаданных
        /// (работа с потоками не предусмотрена).
        /// </summary>
        /// <returns>Экземпляр класса метаданных.</returns>
        internal static Metadata GetInstance()
        {
            return Metadata.instance 
                ?? (Metadata.instance = new Metadata());
        }

        /// <summary>
        /// Имена элементов xml, содержащих информацию 
        /// об отдельных типах недвижимости.
        /// </summary>
        private static string[] elementNames =
        {
            "ParsKvartiryiList",
            "ParsMalosemeykiList",
            "ParsDolevoeList",
            "ParsDomaKottedzhiList",
            "ParsArendaZhilyihList",
            "ParsKommerNedvizList",
            "ParsUchastkiList",
            "ParsDachiList",
            "ParsRaznoeList"
        };

        /// <summary>
        /// Создать экземпляр класса метаданных.
        /// </summary>
        private Metadata()
        {
            XElement rootElement;
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "RieltorBase.Domain.MetadataInfo.DataApartment.xml";

            using (Stream stream = 
                assembly.GetManifestResourceStream(resourceName))
            {
                XDocument doc = XDocument.Load(stream);
                rootElement = doc.Root;
            }

            if (rootElement == null)
            {
                throw new InvalidOperationException(
                    "Не удалось прочитать xml-документ, содержащий информацию "
                    + "о типах недвижимости и их свойствах.");
            }

            this.TypeDescriptions = rootElement.Elements()
                .Where(el => Metadata.elementNames.Contains(el.Name.ToString()))
                .Select(el => new TypeMetadataDescription(el))
                .ToList();
        }

        /// <summary>
        /// Описания типов объектов недвижимости.
        /// </summary>
        internal IEnumerable<TypeMetadataDescription> TypeDescriptions
        {
            get;
            private set;
        }
    }
}
