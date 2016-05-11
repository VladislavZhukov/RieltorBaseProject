using System.Collections.Generic;
using System.Xml.Serialization;

namespace ParserVolgInfo.Core
{
    public class DataApartment
    {
        #region URL type apartement

        private List<string> urlTypeApartement = new List<string>();

        [XmlArray("UrlTypeapArtement")]
        [XmlArrayItem("References")]
        public List<string> UrlTypeapArtement
        {
            get
            {
                return urlTypeApartement;
            }

            set
            {
                urlTypeApartement = value;
            }
        }

        #endregion

        #region Kvartiryi

        private List<string> parsKvartiryiList = new List<string>();
        private List<string> xmlKvartiryiList = new List<string>();        

        [XmlArray("ParsKvartiryiList")]
        [XmlArrayItem("Properties")]
        public List<string> ParsKvartiryiList
        {
            get
            {
                return parsKvartiryiList;
            }

            set
            {
                parsKvartiryiList = value;
            }
        }

        [XmlArray("XmlKvartiryiList")]
        [XmlArrayItem("Properties")]
        public List<string> XmlKvartiryiList
        {
            get
            {
                return xmlKvartiryiList;
            }

            set
            {
                xmlKvartiryiList = value;
            }
        }

        #endregion

        #region Dolevoe

        private List<string> parsDolevoeList = new List<string>();
        private List<string> xmlDolevoeList = new List<string>();

        [XmlArray("ParsDolevoeList")]
        [XmlArrayItem("Properties")]
        public List<string> ParsDolevoeList
        {
            get
            {
                return parsDolevoeList;
            }

            set
            {
                parsDolevoeList = value;
            }
        }

        [XmlArray("XmlDolevoeList")]
        [XmlArrayItem("Properties")]
        public List<string> XmlDolevoeList
        {
            get
            {
                return xmlDolevoeList;
            }

            set
            {
                xmlDolevoeList = value;
            }
        }

        #endregion

        #region Doma kottedzhi

        private List<string> parsDomaKottedzhiList = new List<string>();
        private List<string> xmlDomaKottedzhiList = new List<string>();

        [XmlArray("ParsDomaKottedzhiList")]
        [XmlArrayItem("Properties")]
        public List<string> ParsDomaKottedzhiList
        {
            get
            {
                return parsDomaKottedzhiList;
            }

            set
            {
                parsDomaKottedzhiList = value;
            }
        }

        [XmlArray("XmlDomaKottedzhiList")]
        [XmlArrayItem("Properties")]
        public List<string> XmlDomaKottedzhiList
        {
            get
            {
                return xmlDomaKottedzhiList;
            }

            set
            {
                xmlDomaKottedzhiList = value;
            }
        }

        #endregion

        #region Arenda zhilyih

        private List<string> parsArendaZhilyihList = new List<string>();
        private List<string> xmlArendaZhilyihList = new List<string>();

        [XmlArray("ParsArendaZhilyihList")]
        [XmlArrayItem("Properties")]
        public List<string> ParsArendaZhilyihList
        {
            get
            {
                return parsArendaZhilyihList;
            }

            set
            {
                parsArendaZhilyihList = value;
            }
        }

        [XmlArray("XmlArendaZhilyihList")]
        [XmlArrayItem("Properties")]
        public List<string> XmlArendaZhilyihList
        {
            get
            {
                return xmlArendaZhilyihList;
            }

            set
            {
                xmlArendaZhilyihList = value;
            }
        }

        #endregion

        #region Kommercheskaya nedvizhimost

        private List<string> parsKommerNedvizList = new List<string>();
        private List<string> xmlKommerNedvizList = new List<string>();

        [XmlArray("ParsKommerNedvizList")]
        [XmlArrayItem("Properties")]
        public List<string> ParsKommerNedvizList
        {
            get
            {
                return parsKommerNedvizList;
            }

            set
            {
                parsKommerNedvizList = value;
            }
        }

        [XmlArray("XmlKommerNedvizList")]
        [XmlArrayItem("Properties")]
        public List<string> XmlKommerNedvizList
        {
            get
            {
                return xmlKommerNedvizList;
            }

            set
            {
                xmlKommerNedvizList = value;
            }
        }

        #endregion

        #region Uchastki

        private List<string> parsUchastkiList = new List<string>();
        private List<string> xmlUchastkiList = new List<string>();

        [XmlArray("ParsUchastkiList")]
        [XmlArrayItem("Properties")]
        public List<string> ParsUchastkiList
        {
            get
            {
                return parsUchastkiList;
            }

            set
            {
                parsUchastkiList = value;
            }
        }

        [XmlArray("XmlUchastkiList")]
        [XmlArrayItem("Properties")]
        public List<string> XmlUchastkiList
        {
            get
            {
                return xmlUchastkiList;
            }

            set
            {
                xmlUchastkiList = value;
            }
        }

        #endregion

        #region Dachi

        private List<string> parsDachiList = new List<string>();
        private List<string> xmlDachiList = new List<string>();

        [XmlArray("ParsDachiList")]
        [XmlArrayItem("Properties")]
        public List<string> ParsDachiList
        {
            get
            {
                return parsDachiList;
            }

            set
            {
                parsDachiList = value;
            }
        }

        [XmlArray("XmlDachiList")]
        [XmlArrayItem("Properties")]
        public List<string> XmlDachiList
        {
            get
            {
                return xmlDachiList;
            }

            set
            {
                xmlDachiList = value;
            }
        }

        #endregion

        #region Raznoe

        private List<string> parsRaznoeList = new List<string>();
        private List<string> xmlRaznoeList = new List<string>();

        [XmlArray("ParsRaznoeList")]
        [XmlArrayItem("Properties")]
        public List<string> ParsRaznoeList
        {
            get
            {
                return parsRaznoeList;
            }

            set
            {
                parsRaznoeList = value;
            }
        }

        [XmlArray("XmlRaznoeList")]
        [XmlArrayItem("Properties")]
        public List<string> XmlRaznoeList
        {
            get
            {
                return xmlRaznoeList;
            }

            set
            {
                xmlRaznoeList = value;
            }
        }

        #endregion
    }
}
