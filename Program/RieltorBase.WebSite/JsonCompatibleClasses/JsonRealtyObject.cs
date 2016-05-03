namespace RieltorBase.WebSite.JsonCompatibleClasses
{
    using System;
    using System.Collections.Generic;

    using RieltorBase.Domain.Interfaces;

    public class JsonRealtyObject : IRealtyObject
    {
        private readonly Dictionary<string, string> additionalProps
            = new Dictionary<string, string>();

        public int RealtyObjectId { get; set; }

        public string TypeName { get; set; }

        public DateTime Date { get; set; }

        public string AdditionalInfo { get; set; }

        public string Note { get; set; }

        public string FirmName { get; set; }

        public string AgentName { get; set; }

        public string Phone { get; set; }

        public string Cost { get; set; }

        public Dictionary<string, string> AdditionalAttributes
        {
            get { return this.additionalProps; }
        }
    }
}