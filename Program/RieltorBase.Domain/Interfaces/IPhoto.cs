namespace RieltorBase.Domain.Interfaces
{
    public interface IPhoto
    {
        int PhotoId { get; set; }

        int RealtyObjectId { get; set; }

        int FirmId { get; set; }

        string RelativeSource { get; set; }
    }
}
