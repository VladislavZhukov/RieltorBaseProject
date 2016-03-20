namespace RieltorBase.Domain
{
    using System;

    public class AppartmentSearchOptions
    {
        public object RealtyObjectType { get; set; }

        public string PartOfAddress { get; set; }

        public int MinCost { get; set; }

        public int MaxCost { get; set; }

        DateTime MinDate { get; set; }

        DateTime MaxDate { get; set; }
    }
}
