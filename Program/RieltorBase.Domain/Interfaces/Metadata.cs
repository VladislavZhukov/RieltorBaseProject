namespace RieltorBase.Domain.Interfaces
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
        #region TypeNames

        internal const string AppartmentTypeName = "Квартиры";

        internal const string MalosemeykiTypeName = "Малосемейки";

        internal const string NovostroykiTypeName = "Новостройки";

        internal const string DomaKottedgiTypeName = "Дома/Коттеджи";

        internal const string ArendaTypeName = "Аренда жилья";

        internal const string KommercheskayaTypeName = "Коммерческая Недвижимость";

        internal const string UchastkiTypeName = "Участки";

        internal const string DachiTypeName = "Дачи";

        internal const string RaznoeTypeName = "Разное";

        #endregion

        #region Имена общих свойств для всех типов объектов

        internal const string DatePropName = "Дата";

        internal const string AdditionalInfoPropName = "Дополнительная информация";

        internal const string NotePropName = "Примечание";

        /// <summary>
        /// Имя свойства цены для всех типов объектов, кроме аренды.
        /// </summary>
        internal const string CostPropName = "Цена тыс.руб.";

        /// <summary>
        /// Имя свойства цены для аренды жилья.
        /// </summary>
        internal const string CostPropNameForRent = "Цена";

        #endregion

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
            var resourceName = "RieltorBase.Domain.Interfaces.DataApartment.xml";

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

        /// <summary>
        /// Получить имена всех дополнительных свойств объекта недвижимости.
        /// </summary>
        /// <param name="realtyObjTypeName">Имя типа объекта недвижимости.</param>
        /// <returns>Имена дополнительных свойств объекта недвижимости.</returns>
        internal static string[] GetAdditionalAttrNames(
            string realtyObjTypeName)
        {
            switch (realtyObjTypeName)
            {
                case Metadata.AppartmentTypeName:
                    return new[]
                    {
                        "Район",
                        "Квартал",
                        "Комнат",
                        "Улица",
                        "Дом",
                        "Этажи",
                        "Планировка",
                        "Материал Стен",
                        "Площадь [общ/жил/кух]",
                        "Вариант",
                        "Балкон"
                    };
                case Metadata.MalosemeykiTypeName:
                    return new[]
                    {
                        "Район",
                        "Квартал",
                        "Комнат",
                        "Улица",
                        "Дом",
                        "Этажи",
                        "Планировка",
                        "Материал Стен",
                        "Площадь [общ/жил/кух]",
                        "Вариант",
                        "Балкон"
                    };
                case Metadata.NovostroykiTypeName:
                    return new[]
                    {
                        "Район",
                        "Квартал",
                        "Жилой Комплекс",
                        "Комнат",
                        "Улица",
                        "Дом",
                        "Этажи",
                        "Планировка",
                        "Материал Стен",
                        "Площадь [общ/жил/кух]",
                        "Вариант",
                        "Балкон"
                    };
                case Metadata.DomaKottedgiTypeName:
                    return new[]
                    {
                        "Готовность",
                        "Район",
                        "Адрес",
                        "Этажей",
                        "S Дома",
                        "S участка",
                        "Материал Стен"
                    };
                case Metadata.ArendaTypeName:
                    return new[]
                    {
                        "Район",
                        "Квартал",
                        "Комнат",
                        "Улица",
                        "Дом",
                        "Этажи",
                        "Планировка",
                        "Материал Стен",
                        "Площадь [общ/жил/кух]",
                        "Вариант",
                        "Балкон"
                    };
                case Metadata.KommercheskayaTypeName:
                    return new[]
                    {
                        "Тип Сделки",
                        "Назначение",
                        "Район",
                        "Адрес",
                        "Этаж",
                        "Площадь всего",
                        "Цена метра"
                    };
                case Metadata.UchastkiTypeName:
                    return new[]
                    {
                        "Назначение",
                        "Район",
                        "Адрес",
                        "Площадь всего"
                    };
                case Metadata.DachiTypeName:
                    return new[]
                    {
                        "Готовность",
                        "Район",
                        "Адрес",
                        "Этажей",
                        "S Дома",
                        "S участка",
                        "Материал Стен"
                    };
                case Metadata.RaznoeTypeName:
                    return new[]
                    {
                        "Тип помещения",
                        "Адрес",
                        "S Дома",
                        "Материал Стен"
                    };
                default:
                    throw new ArgumentOutOfRangeException("realtyObjTypeName");
            }
        }

        internal static string GetCostPropertyName(string typeName)
        {
            if (typeName == Metadata.ArendaTypeName)
            {
                return Metadata.CostPropNameForRent;
            }

            return Metadata.CostPropName;
        }
    }
}
