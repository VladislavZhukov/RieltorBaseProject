namespace RieltorBase.WebSite.JsonCompatibleClasses
{
    using RieltorBase.Domain.Interfaces;

    public class JsonFirm : IFirm
    {
        public int FirmId { get; set; }

        public string Name { get; set; }
    }
}