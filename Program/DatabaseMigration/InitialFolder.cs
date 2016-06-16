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

        /// <summary>
        /// Получить предполагаемый путь к папке с фотографиями
        /// объекта недвижимости.
        /// </summary>
        /// <param name="realtyObjectType">Тип объекта недвижимости.</param>
        /// <param name="realtyObjectId">Id объекта недвижимости (из xml).</param>
        /// <returns>Предполагаемый путь к фотографиям 
        /// объекта недвижимости.</returns>
        /// <remarks>Существование папки не гарантируется, т.к. 
        /// у объекта может не быть фотографий.</remarks>
        internal string GetPhotoPath(
            RealtyObjectType realtyObjectType,
            string realtyObjectId)
        {
            string xmlPath = this.GetXmlDoc(realtyObjectType);
            string dirPath = Path.GetDirectoryName(xmlPath);

            if (string.IsNullOrWhiteSpace(dirPath))
            {
                throw new DirectoryNotFoundException(
                    "Не удалось найти папку хранения фотографий объекта " 
                    + realtyObjectId);
            }
            
            return Path.Combine(dirPath, "photo", realtyObjectId);
        }

        /// <summary>
        /// Получить полный путь к родительской папке (папка, в которую
        /// входит папка с исходными данными).
        /// </summary>
        /// <returns>Полный путь к родительской папке.</returns>
        internal string GetParentFolderPath()
        {
            DirectoryInfo parentDir = this.initialDir.Parent;

            if (parentDir == null)
            {
                throw new InvalidOperationException(
                    "Не удалось получить родительскую папку для папки " 
                    + this.initialDir.FullName);
            }

            return parentDir.FullName;
        }
    }
}
