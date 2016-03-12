using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using xNet;

namespace ParserVolgInfo.Core
{
    /// <summary>
    /// Класс предназначе для парсинга сайта http://www.volga-info.ru/togliatti/search/kvartiryi/,
    /// вытаскивает всю информацию и фотографии по Тольяттинским квартирам.
    /// </summary>
    public class ParserVI
    {
        #region Массиы
        //оба массива должны быть одного разсера
        //нужен для парсинга страниц квартир
        private static string[] ArrPropAparPars = new string[] { "Дата",
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
            "Цена тыс.руб.",
            "Балкон",
            "Дополнительная информация",
            "Примечания",
            "Фирма",
            "Агент",
            "Телефон контакта" };

        //нужен для создания структуры xml файла
        private static string[] ArrPropAparXml = new string[] { "Date",
            "District",
            "Quarter",
            "QuantityRoom",
            "Street",
            "Home",
            "Flor",
            "Disposition",
            "WallMaterial",
            "Area",
            "Variant",
            "Price",
            "Balcony",
            "AdditionalInfo",
            "Notes",
            "Company",
            "Agent",
            "PhoneContact" };

        #endregion

        #region Данные для парсинга

        private static List<string> forParsQuantityPage = new List<string>() { " объект,", " объекта,", " объектов," };

        #endregion

        #region Public методы

        /// <summary>
        /// Парсит страницы Тольяттинских квартир.
        /// Записывает полученные данные в xml файл.
        /// Создает папку photo, в ней создает папки с id квартир и сохраняет в них фото.
        /// </summary>
        /// <param name="idApartment"></param>
        /// <returns></returns>
        static public void StartParser()
        {
            Directory.CreateDirectory("image");
            Directory.CreateDirectory("log");

            //получаем общее кол-во квартир        
            var quantityApartment = 20;//GetCountPages();

            //получаем ID квартир.
            List<string> idApartment = GetAllIdApartment(quantityApartment);

            int counterFinishApartment = 0;

            //создаем файл apartment.xml
            using (var xmlWriter = new XmlTextWriter("apartment.xml", Encoding.UTF8))
            {
                try
                {
                    #region форматирование xml документа

                    xmlWriter.Formatting = Formatting.Indented;
                    xmlWriter.IndentChar = '\t';
                    xmlWriter.Indentation = 1;
                    xmlWriter.QuoteChar = '\'';

                    #endregion

                    xmlWriter.WriteStartDocument();
                    xmlWriter.WriteStartElement("ListOfApartment");

                    foreach (var idApart in idApartment)
                    {
                        using (var request = new HttpRequest())
                        {
                            //получаем страницу
                            var sourcePage = request.Get("www.volga-info.ru/togliatti/object/kvartiryi/" + idApart).ToString();

                            //парсим и одновременно пишем в файл
                            WriteToXmlFile(xmlWriter, idApart, sourcePage);

                            //парсим и получаем массив с ссылками на фотографии
                            var imageUrl = sourcePage.Substrings("<img alt=\"\" src=\"", "\" style", 0);

                            if (imageUrl.Length > 0)
                            {
                                //парсим, создаем каталоги и сохраняем в них фото
                                CreateDirAndPfoto(idApart, sourcePage, imageUrl);
                            }

                            counterFinishApartment++;
                        }
                    }

                    xmlWriter.WriteEndElement();
                }
                catch (Exception ex)
                {
                    Logger logger = LogManager.GetCurrentClassLogger();
                    logger.Error(ex);
                }
                finally
                {
                    Logger logger = LogManager.GetCurrentClassLogger();
                    logger.Info("квартир спарсено:" + counterFinishApartment);
                }
            }
        }

        #endregion

        #region Private методы

        /// <summary>
        /// Возвращает общее кол-во квартир.
        /// </summary>
        /// <returns></returns>
        private static int GetCountPages()
        {
            int countPages = 0;

            try
            {
                using (var request = new HttpRequest())
                {
                    string sourcePage;

                    //получаем страницу
                    sourcePage = request.Get("http://www.volga-info.ru/togliatti/search/kvartiryi/x").ToString();

                    foreach (var item in forParsQuantityPage)
                    {
                        var numOfPages = sourcePage.Substrings("Всего найдено ", item, 0);

                        if (numOfPages.Length != 0)
                        {
                            //парсим и получаем общее кол-во страниц
                            countPages = Convert.ToInt32(numOfPages[0]);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Error(ex);
            }

            while (countPages % 20 != 0)
            {
                countPages--;
            }

            return countPages;
        }

        /// <summary>
        /// Возвращает List с id квартир. Для перехода по страницам (каждая квартира имеет свою страницу). 
        /// </summary>
        /// <returns>коллекция ID квартир</returns>
        private static List<string> GetAllIdApartment(int numberApartments)
        {
            var listIdApartment = new List<string>();

            try
            {
                var reqParams = new RequestParams();

                //парсим и вытаскиваем все ID квартир (одна страница 20 квартир)
                for (int i = 0; i <= numberApartments; i += 20)
                {
                    using (var request = new HttpRequest())
                    {
                        reqParams["SEARCH_BEGINPOS"] = i.ToString();

                        //получпем страницу с квартирами
                        string contentPage = request.Post("www.volga-info.ru/togliatti/search/kvartiryi/x", reqParams).ToString();

                        string[] apartmentId;

                        //парсим страницу и записываем данные в массив
                        apartmentId = contentPage.Substrings("/togliatti/object/kvartiryi/", "/\">", 0).Distinct().ToArray();

                        foreach (var item in apartmentId)
                        {
                            listIdApartment.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Error(ex);
            }

            return listIdApartment.Distinct().ToList();
        }        

        /// <summary>
        /// Метод парсит и записывает в XML файл данные со страницы.
        /// </summary>
        /// <param name="xmlWriter">инструмент(объект) необходимый для записи в xml файл</param>
        /// <param name="idApart">id квартир</param>
        /// <param name="sourcePage">страница для парсинга</param>
        private static void WriteToXmlFile(XmlTextWriter xmlWriter, string idApart, string sourcePage)
        {
            try
            {
                //создаем дерево(структуру) xml файла и пишем в него данные
                xmlWriter.WriteStartElement("Apartment");
                xmlWriter.WriteStartElement("IdApartment");
                xmlWriter.WriteString(idApart);
                xmlWriter.WriteEndElement();

                if (ArrPropAparXml.Length == ArrPropAparPars.Length)
                {
                    for (int i = 0; i < ArrPropAparXml.Length; i++)
                    {
                        xmlWriter.WriteStartElement(ArrPropAparXml[i]);

                        var parsData = sourcePage.Substrings("<b>" + ArrPropAparPars[i] + "</b></td><td>", "</td></tr>", 0);

                        if (parsData.Length != 0)
                        {
                            xmlWriter.WriteString(parsData[0].Replace("<br>", " ").Trim());
                        }

                        xmlWriter.WriteEndElement();
                    }
                }

                xmlWriter.WriteEndElement();
            }
            catch (Exception ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Error(ex);
            }
        }

        /// <summary>
        /// Метод создает каталоги по ID квартир, парсит страницу и сохраняет фото.
        /// </summary>
        /// <param name="idApart">ID квартир</param>
        /// <param name="sourcePage">страница</param>
        private static void CreateDirAndPfoto(string idApart, string sourcePage, string[] imagesUrl)
        {
            try
            {
                int nameImage = 0;

                string pathImageFile = "image\\" + idApart;

                Directory.CreateDirectory(pathImageFile);

                foreach (var image in imagesUrl)
                {
                    image.Replace("amp;", "");

                    Uri url = new Uri(image);

                    //сохраняем фото
                    using (WebClient client = new WebClient())
                    {
                        nameImage++;
                        string path = pathImageFile + "\\" + nameImage.ToString() + ".jpg";
                        client.DownloadFile(url, path);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Error(ex);
            }
        }

        #endregion
    }
}
