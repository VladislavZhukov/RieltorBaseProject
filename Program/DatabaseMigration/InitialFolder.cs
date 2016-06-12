namespace DatabaseMigration
{
    using System;
    using System.IO;

    /// <summary>
    /// Класс работы с папкой-источником.
    /// </summary>
    internal class InitialFolder
    {
        /// <summary>
        /// Исходная папка с данными.
        /// </summary>
        private readonly DirectoryInfo initialDir;

        /// <summary>
        /// Создать экземпляр класса информации об исходной папке.
        /// </summary>
        /// <param name="directory"></param>
        internal InitialFolder(DirectoryInfo directory)
        {
            this.initialDir = directory;
        }

        /// <summary>
        /// Получить полный путь к XML-документу, содержащему 
        /// объекты недвижимости конкретного типа.
        /// </summary>
        /// <param name="type">Тип объектов недвижимости.</param>
        /// <returns>XML-документ.</returns>
        internal string GetXmlDoc(RealtyObjectType type)
        {
            string path = this.initialDir.FullName;

            switch (type)
            {
                case RealtyObjectType.ArendaZhilyih:
                    path = Path.Combine(path, "arenda_zhilyih", "info_arenda_zhilyih.xml");
                    break;
                case RealtyObjectType.Dachi:
                    path = Path.Combine(path, "dachi", "info_dachi.xml");
                    break;
                case RealtyObjectType.Dolevoe:
                    path = Path.Combine(path, "dolevoe", "info_dolevoe.xml");
                    break;
                case RealtyObjectType.DomaKottedzhi:
                    path = Path.Combine(path, "doma_kottedzhi", "info_doma_kottedzhi.xml");
                    break;
                case RealtyObjectType.KommercheskayaNedvizhimost:
                    path = Path.Combine(path, "kommercheskaya_nedvizhimost", "info_kommercheskaya_nedvizhimost.xml");
                    break;
                case RealtyObjectType.Kvartiryi:
                    path = Path.Combine(path, "kvartiryi", "info_kvartiryi.xml");
                    break;
                case RealtyObjectType.Malosemeyki:
                    path = Path.Combine(path, "malosemeyki", "info_malosemeyki.xml");
                    break;
                case RealtyObjectType.Raznoe:
                    path = Path.Combine(path, "raznoe", "info_raznoe.xml");
                    break;
                case RealtyObjectType.Uchastki:
                    path = Path.Combine(path, "uchastki", "info_uchastki.xml");
                    break;
                default:
                    throw new ArgumentOutOfRangeException("type", type, null);
            }

            return path;
        }
    }
}
