namespace RieltorBase.Domain.EntityFrameworkImpl
{
    using RieltorBase.Domain.Interfaces;

    public class FirmWrap : IWrapBase<Firm, IFirm>, IFirm
    {
        private readonly Firm firmEF;

        public FirmWrap(Firm firm)
        {
            this.firmEF = firm;
        }

        public FirmWrap(IFirm iFirm)
        {
            this.firmEF = new Firm()
            {
                FirmId = iFirm.FirmId,
                Name = iFirm.Name
            };
        }

        public int FirmId
        {
            get
            {
                return this.firmEF.FirmId;
            }
            set
            {
                this.FirmId = value;
            }
        }

        public string Name
        {
            get
            {
                return this.firmEF.Name;
            }
            set
            {
                this.firmEF.Name = value;
            }
        }

        public Firm GetRealObject()
        {
            return this.firmEF;
        }

        public void UpdateRealObject(Firm realObject)
        {
            realObject.Name = this.Name;
        }
    }
}
