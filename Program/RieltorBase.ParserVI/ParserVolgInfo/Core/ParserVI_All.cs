using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using xNet;

namespace ParserVolgInfo.Core
{
    class ParserVI_All
    {
        #region Данные для парсинга

        private static readonly List<string> ForParsQuantityPage = new List<string>() { " объект,", " объекта,", " объектов," };

        #endregion

        static public void StartParser()
        {
            #region Настройки консоли

            Console.SetWindowSize(120, 10);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Title = "Parser VI active";
            Console.WriteLine("Парсер запущен, ожидайте окончания выполнения работы...");

            #endregion

            try
            {
                var serializer = new XmlSerializer(typeof(DataApartment));

                var dataApart = new DataApartment();

                var pathDataApartmentXML = "Core\\DataApartment.xml";

                if (File.Exists(pathDataApartmentXML))
                {
                    FileStream stream = new FileStream(pathDataApartmentXML, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                    dataApart = serializer.Deserialize(stream) as DataApartment;

                    stream.Close();
                }

                Directory.CreateDirectory("apartment");

                for (int i = 0; i < dataApart.UrlTypeapArtement.Count; i++)
                {
                    var typeApartament = dataApart.UrlTypeapArtement[i].Substring("/search/", "/", 0).ToString();//;
                    var pathCurrentApartment = "apartment/" + typeApartament;
                    var pathCurrentApartmentPhoto = pathCurrentApartment + "/photo";

                    Console.WriteLine("идет получение " + typeApartament);

                    Directory.CreateDirectory(pathCurrentApartment);
                    Directory.CreateDirectory(pathCurrentApartmentPhoto);

                    using (var xmlWriter = new XmlTextWriter(pathCurrentApartment + "/info_" + typeApartament + ".xml", Encoding.UTF8))
                    {
                        //получаем общее кол-во квартир        
                        var quantityApartment = GetCountPages(dataApart.UrlTypeapArtement[i]);//GetCountPages(dataApart.UrlTypeapArtement[i]);                       

                        //получаем ID квартир.
                        var idApartment = GetAllIdApartment(quantityApartment, dataApart.UrlTypeapArtement[i]);

                        //счетчик записанных в файл жилищь
                        var counterFinishApartment = 0;

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
                                Console.WriteLine("Готовые " + typeApartament + " = " + counterFinishApartment);

                                var urlApartment = dataApart.UrlTypeapArtement[i].Replace("/x", "/").Replace("search", "object") + idApart;

                                var startTime = DateTime.Now;
                                Console.WriteLine("Получение данных со страницы: " + urlApartment);

                                //получаем страницу
                                string sourcePage = null;

                                try
                                {
                                    sourcePage = request.Get(urlApartment).ToString();
                                }
                                catch (Exception)
                                {
                                    Logger logger = LogManager.GetCurrentClassLogger();
                                    logger.Info("не удалось загрузить квартиру" + " " + urlApartment);
                                    continue;
                                }

                                Console.WriteLine("Данные полученны за: " + (DateTime.Now - startTime).TotalMilliseconds + "\nНачат процесс парсинга данных и загрузки их на диск...");

                                //парсим и получаем массив с ссылками на фотографии
                                var imageUrl = sourcePage.Substrings("<img alt=\"\" src=\"", "\" style", 0);

                                switch (typeApartament)
                                {
                                    case "kvartiryi":
                                        //парсим и одновременно пишем в файл
                                        WriteToXmlFile(dataApart.ParsKvartiryiList, dataApart.XmlKvartiryiList, xmlWriter, idApart, sourcePage, imageUrl);
                                        break;
                                    case "malosemeyki":
                                        WriteToXmlFile(dataApart.ParsMalosimeykiList, dataApart.XmlMalosimeykiList, xmlWriter, idApart, sourcePage, imageUrl);
                                        break;
                                    case "dolevoe":
                                        WriteToXmlFile(dataApart.ParsDolevoeList, dataApart.XmlDolevoeList, xmlWriter, idApart, sourcePage, imageUrl);
                                        break;
                                    case "doma_kottedzhi":
                                        WriteToXmlFile(dataApart.ParsDomaKottedzhiList, dataApart.XmlDomaKottedzhiList, xmlWriter, idApart, sourcePage, imageUrl);
                                        break;
                                    case "arenda_zhilyih":
                                        WriteToXmlFile(dataApart.ParsArendaZhilyihList, dataApart.XmlArendaZhilyihList, xmlWriter, idApart, sourcePage, imageUrl);
                                        break;
                                    case "kommercheskaya_nedvizhimost":
                                        WriteToXmlFile(dataApart.ParsKommerNedvizList, dataApart.XmlKommerNedvizList, xmlWriter, idApart, sourcePage, imageUrl);
                                        break;
                                    case "uchastki":
                                        WriteToXmlFile(dataApart.ParsUchastkiList, dataApart.XmlUchastkiList, xmlWriter, idApart, sourcePage, imageUrl);
                                        break;
                                    case "dachi":
                                        WriteToXmlFile(dataApart.ParsDachiList, dataApart.XmlDachiList, xmlWriter, idApart, sourcePage, imageUrl);
                                        break;
                                    case "raznoe":
                                        WriteToXmlFile(dataApart.ParsRaznoeList, dataApart.XmlRaznoeList, xmlWriter, idApart, sourcePage, imageUrl);
                                        break;
                                    default:
                                        break;
                                }

                                if (imageUrl.Length > 0)
                                {
                                    //парсим, создаем каталоги и сохраняем в них фото
                                    CreateDirAndPfoto(pathCurrentApartmentPhoto, idApart, imageUrl);
                                }

                                counterFinishApartment++;
                                Console.WriteLine("Данные успешно загурженны...\n");
                                Console.Clear();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger logger = LogManager.GetCurrentClassLogger();
                logger.Error(ex);
            }
            finally
            {
                Console.WriteLine("Работа завершена.");
                Console.Title = "Parser VI done";
            }
        }

        /// <summary>
        /// Возвращает общее кол-во квартир.
        /// </summary>
        /// <param name="urlTypeShelter">url для полученмя кол-ва объектов</param>
        /// <returns>Кол-во объектов заданого типа</returns>
        private static int GetCountPages(string urlTypeShelter)
        {
            var countPages = 0;

            using (var request = new HttpRequest())
            {
                //получаем страницу
                var sourcePage = request.Get(urlTypeShelter).ToString();

                foreach (var item in ForParsQuantityPage)
                {
                    var numOfPages = sourcePage.Substrings("Всего найдено ", item, 0);

                    if (numOfPages.Length != 0)
                    {
                        //парсим и получаем общее кол-во страниц
                        countPages = Convert.ToInt32(numOfPages[0]);
                    }
                }

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
        /// <param name="numberApartments">Кол-во объектов</param>
        /// <param name="urlTypeShelter">Url для получения страницы</param>
        /// <returns></returns>
        private static List<string> GetAllIdApartment(int numberApartments, string urlTypeShelter)
        {
            var listIdApartment = new List<string>();

            var reqParams = new RequestParams();

            //парсим и вытаскиваем все ID квартир (одна страница 20 квартир)
            for (var i = 0; i <= numberApartments; i += 20)
            {
                using (var request = new HttpRequest())
                {
                    reqParams["SEARCH_BEGINPOS"] = i.ToString();

                    //получпем страницу с квартирами
                    var contentPage = request.Post(urlTypeShelter, reqParams).ToString();

                    //парсим страницу и записываем данные в массив
                    var apartmentId = contentPage.Substrings("<tr id=\"", "\"", 0).Distinct().ToArray();

                    foreach (var item in apartmentId)
                    {
                        listIdApartment.Add(item);
                    }
                }
            }

            return listIdApartment.Distinct().ToList();
        }

        /// <summary>
        /// Метод парсит и записывает в XML файл данные со страницы.
        /// </summary>
        /// <param name="arrPropPars">Массив с параметрами для парсинга</param>
        /// <param name="arrPropXml">Массив с свойствами для записи в XML</param>
        /// <param name="xmlWriter">Yеобходим для записи в xml файл</param>
        /// <param name="idApart">Id квартир</param>
        /// <param name="sourcePage">Cтраница для парсинга</param>
        /// <param name="imagesUrl">Массив с ссылками на фото</param>
        private static void WriteToXmlFile(List<string> arrPropPars, List<string> arrPropXml, XmlTextWriter xmlWriter, string idApart, string sourcePage, string[] imagesUrl)
        {
            //создаем дерево(структуру) xml файла и пишем в него данные
            xmlWriter.WriteStartElement("Apartment");
            xmlWriter.WriteStartElement("IdApartment");
            xmlWriter.WriteString(idApart);
            xmlWriter.WriteEndElement();

            if (arrPropPars.Count == arrPropXml.Count)
            {
                for (int i = 0; i < arrPropXml.Count; i++)
                {
                    xmlWriter.WriteStartElement(arrPropXml[i]);

                    var parsData = sourcePage.Substrings("<b>" + arrPropPars[i] + "</b></td><td>", "</td></tr>", 0);

                    if (parsData.Length != 0)
                    {
                        xmlWriter.WriteString(parsData[0].Replace("<br>", "").Replace("->", "").Trim());
                    }

                    xmlWriter.WriteEndElement();
                }

                if (imagesUrl.Length > 0)
                {
                    foreach (var iUrl in imagesUrl)
                    {
                        xmlWriter.WriteStartElement("UrlFoto");
                        xmlWriter.WriteString(iUrl);
                        xmlWriter.WriteEndElement();
                    }
                }
            }

            xmlWriter.WriteEndElement();
        }

        /// <summary>
        /// Метод создает каталоги по ID квартир, парсит страницу и сохраняет фото.
        /// </summary>
        /// <param name="pathForPhotoApartment"></param>
        /// <param name="idApart">ID квартир</param>
        /// <param name="imagesUrl">Массив с ссылками на фото</param>
        private static void CreateDirAndPfoto(string pathForPhotoApartment, string idApart, string[] imagesUrl)
        {
            var nameImage = 0;

            var pathImageFile = pathForPhotoApartment + "/" + idApart;

            Directory.CreateDirectory(pathImageFile);

            foreach (var image in imagesUrl)
            {
                var url = new Uri(image);

                //сохраняем фото
                using (var client = new WebClient())
                {
                    nameImage++;
                    var path = pathImageFile + "\\" + nameImage.ToString() + ".jpg";
                    client.DownloadFile(url, path);
                }
            }
        }
    }
}
