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
        #region Массиы     
        
        #region malosemeyki

        //нужен для парсинга страниц малосимеек
        private static string[] ArrPropMalosemeykiPars = new string[] { "Вариант:",
            "Планировка:",
            "Материал стен:",
            "Балкон:",
            "Остекление:",
            "Примечания:",
            "Фирма:",
            "Агент:" };

        //нужен для создания структуры xml файла
        private static string[] ArrPropMalosemeykiXml = new string[] { "Variant",
            "Disposition",
            "WallMaterial",
            "Balcony",
            "Glazing",
            "Comments",
            "Company",
            "Agent" };

        #endregion



        #endregion

        #region Данные для парсинга

        private static List<string> forParsQuantityPage = new List<string>() { " объект,", " объекта,", " объектов," };

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
                XmlSerializer serializer = new XmlSerializer(typeof(DataApartment));

                DataApartment dataApart = new DataApartment();

                string pathDataApartmentXML = "DataApartment.xml";

                if (File.Exists(pathDataApartmentXML))
                {
                    FileStream stream = new FileStream(pathDataApartmentXML, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                    dataApart = serializer.Deserialize(stream) as DataApartment;

                    stream.Close();
                }

                var reqParams = new RequestParams();

                Directory.CreateDirectory("apartment");

                for (int i = 0; i < dataApart.UrlTypeapArtement.Count; i++)
                {
                    string typeApartament = dataApart.UrlTypeapArtement[i].Substring("/search/", "/", 0).ToString();
                    string pathCurrentApartment = "apartment/" + typeApartament;
                    string pathCurrentApartmentPhoto = pathCurrentApartment + "/photo";

                    Console.WriteLine("идет получение " + typeApartament);

                    Directory.CreateDirectory(pathCurrentApartment);
                    Directory.CreateDirectory(pathCurrentApartmentPhoto);

                    using (var xmlWriter = new XmlTextWriter(pathCurrentApartment + "/info_" + typeApartament + ".xml", Encoding.UTF8))
                    {
                        //получаем общее кол-во квартир        
                        var quantityApartment = 20;//GetCountPages();                       

                        //получаем ID квартир.
                        List<string> idApartment = GetAllIdApartment(quantityApartment, dataApart.UrlTypeapArtement[i]);

                        //счетчик записанных в файл жилищь
                        int counterFinishApartment = 0;

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

                                string urlApartment = dataApart.UrlTypeapArtement[i].Replace("/x", "/").Replace("search", "object") + idApart;

                                DateTime startTime = DateTime.Now;
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

                                switch (typeApartament)
                                {
                                    case "kvartiryi":
                                        //парсим и одновременно пишем в файл
                                        WriteToXmlFile(dataApart.ParsKvartiryiList, dataApart.XmlKvartiryiList, xmlWriter, idApart, sourcePage);
                                        break;
                                    case "dolevoe":                                        
                                        WriteToXmlFile(dataApart.ParsDolevoeList, dataApart.XmlDolevoeList, xmlWriter, idApart, sourcePage);
                                        break;
                                    case "doma_kottedzhi":                                        
                                        WriteToXmlFile(dataApart.ParsDomaKottedzhiList, dataApart.XmlDomaKottedzhiList, xmlWriter, idApart, sourcePage);
                                        break;
                                    case "arenda_zhilyih":
                                        WriteToXmlFile(dataApart.ParsArendaZhilyihList, dataApart.XmlArendaZhilyihList, xmlWriter, idApart, sourcePage);
                                        break;
                                    case "kommercheskaya_nedvizhimost":
                                        WriteToXmlFile(dataApart.ParsKommerNedvizList, dataApart.XmlKommerNedvizList, xmlWriter, idApart, sourcePage);
                                        break;
                                    case "uchastki":
                                        WriteToXmlFile(dataApart.ParsUchastkiList, dataApart.XmlUchastkiList, xmlWriter, idApart, sourcePage);
                                        break;
                                    case "dachi":
                                        WriteToXmlFile(dataApart.ParsDachiList, dataApart.XmlDachiList, xmlWriter, idApart, sourcePage);
                                        break;
                                    case "raznoe":
                                        WriteToXmlFile(dataApart.ParsRaznoeList, dataApart.XmlRaznoeList, xmlWriter, idApart, sourcePage);
                                        break;
                                    default:
                                        break;
                                }

                                //парсим и получаем массив с ссылками на фотографии
                                var imageUrl = sourcePage.Substrings("<img alt=\"\" src=\"", "\" style", 0);

                                if (imageUrl.Length > 0)
                                {
                                    //парсим, создаем каталоги и сохраняем в них фото
                                    CreateDirAndPfoto(pathCurrentApartmentPhoto, idApart, sourcePage, imageUrl);
                                }

                                counterFinishApartment++;
                                Console.WriteLine("Данные успешно загурженны...\n");
                                Console.Clear();
                            }
                        }
                        #region for malosemeiki

                        //xmlWriter.WriteStartDocument();
                        //xmlWriter.WriteStartElement("ListOfApartment");

                        ////парсим и вытаскиваем все ID квартир (одна страница 20 квартир)
                        //for (int z = 0; z <= quantityApartment; z += 20)
                        //{
                        //    using (var request = new HttpRequest())
                        //    {
                        //        reqParams["SEARCH_BEGINPOS"] = z.ToString();

                        //        //получпем страницу с квартирами
                        //        string contentPage = request.Post(urlTypeShelters[i], reqParams).ToString();

                        //        string[] apartmentId;
                        //        List<string[]> variant = new List<string[]>();



                        //        //парсим страницу и записываем данные в массив
                        //        apartmentId = contentPage.Substrings("<tr id=\"", "\"", 0).ToArray();
                        //        for (int counterIdApartment = 0; counterIdApartment < apartmentId.Length; counterIdApartment++)
                        //        {
                        //            xmlWriter.WriteStartElement("Apartment");
                        //            xmlWriter.WriteStartElement("IdApartment");
                        //            xmlWriter.WriteString(apartmentId[counterIdApartment]);
                        //            xmlWriter.WriteEndElement();

                        //            switch (typeApartament)
                        //            {
                        //                case "malosemeyki":

                        //                    FillingXmlFileDataApartment(ArrPropMalosemeykiPars, ArrPropMalosemeykiXml, xmlWriter, contentPage, counterIdApartment);

                        //                    break;
                        //                case "dolevoe":

                        //                    FillingXmlFileDataApartment(ArrPropDolevoekiPars, ArrPropDolevoeXml, xmlWriter, contentPage, counterIdApartment);

                        //                    break;
                        //                default:
                        //                    break;
                        //            }

                        //            xmlWriter.WriteEndElement();
                        //        }
                        //    }
                        //}

                        //xmlWriter.WriteEndElement();

                        #endregion
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
        /// <returns></returns>
        private static int GetCountPages(string urlTypeShelter)
        {
            int countPages = 0;

            try
            {
                using (var request = new HttpRequest())
                {
                    string sourcePage;

                    //получаем страницу
                    sourcePage = request.Get(urlTypeShelter).ToString();

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
        private static List<string> GetAllIdApartment(int numberApartments, string urlTypeShelter)
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
                        string contentPage = request.Post(urlTypeShelter, reqParams).ToString();

                        string[] apartmentId;

                        //парсим страницу и записываем данные в массив
                        apartmentId = contentPage.Substrings("<tr id=\"", "\"", 0).Distinct().ToArray();

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
        private static void WriteToXmlFile(List<string> ArrPropPars, List<string> ArrPropXml, XmlTextWriter xmlWriter, string idApart, string sourcePage)
        {
            try
            {
                //создаем дерево(структуру) xml файла и пишем в него данные
                xmlWriter.WriteStartElement("Apartment");
                xmlWriter.WriteStartElement("IdApartment");
                xmlWriter.WriteString(idApart);
                xmlWriter.WriteEndElement();

                if (ArrPropPars.Count == ArrPropXml.Count)
                {
                    for (int i = 0; i < ArrPropXml.Count; i++)
                    {
                        xmlWriter.WriteStartElement(ArrPropXml[i]);

                        var parsData = sourcePage.Substrings("<b>" + ArrPropPars[i] + "</b></td><td>", "</td></tr>", 0);

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
        private static void CreateDirAndPfoto(string pathForPhotoApartment, string idApart, string sourcePage, string[] imagesUrl)
        {
            try
            {
                int nameImage = 0;

                string pathImageFile = pathForPhotoApartment + "/" + idApart;

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

        #region допилить
        ///переделать
        //private static void NewFillingXmlFileDataApartment(string[] ArrPropPars, string[] ArrPropXml, XmlTextWriter xmlWriter, string contentPage, int counterIdApartment)
        //{
        //    if (ArrPropPars.Length == ArrPropXml.Length)
        //    {
        //        for (int i = 0; i < ArrPropPars.Length; i++)
        //        {
        //            xmlWriter.WriteStartElement(ArrPropXml[i]);

        //            var parsData = contentPage.Substrings("<b>" + ArrPropPars[i] + "</b></td><td>", "</td></tr>", 0);

        //            if (parsData.Length != 0)
        //            {
        //                xmlWriter.WriteString(parsData[counterIdApartment].Replace("<br>", " ").Trim());
        //            }

        //            xmlWriter.WriteEndElement();
        //        }
        //    }
        //}

        ////для малосемеек
        //private static void FillingXmlFileDataApartment(string[] ArrPropPars, string[] ArrPropXml, XmlTextWriter xmlWriter, string contentPage, int counterIdApartment)
        //{
        //    if (ArrPropPars.Length == ArrPropXml.Length)
        //    {
        //        for (int i = 0; i < ArrPropPars.Length; i++)
        //        {
        //            xmlWriter.WriteStartElement(ArrPropXml[i]);

        //            var parsData = contentPage.Substrings("<li><strong>" + ArrPropPars[i] + "</strong>", "<", 0);

        //            if (parsData.Length != 0)
        //            {
        //                xmlWriter.WriteString(parsData[counterIdApartment].Replace("<br>", " ").Trim());
        //            }

        //            xmlWriter.WriteEndElement();
        //        }
        //    }
        //}
        #endregion
    }
}
