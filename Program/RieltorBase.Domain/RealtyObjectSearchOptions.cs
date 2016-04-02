namespace RieltorBase.Domain
{
    using System;
    using System.Collections;

    public class RealtyObjectSearchOptions
    {
        public string RealtyObjectTypes { get; set; }

        public string PartOfAddress { get; set; }

        public int MinCost { get; set; }

        public int MaxCost { get; set; }

        public DateTime? MinDate { get; set; }

        public DateTime? MaxDate { get; set; }
    }
}
