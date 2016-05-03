namespace RieltorBase.Domain.EntityFrameworkImpl
{
    using RieltorBase.Domain.Interfaces;

    /// <summary>
    /// Обертка класса <see cref="Firm"/>, реализующая интерфейс
    /// <see cref="IFirm"/>.
    /// </summary>
    public class FirmWrap : IWrapBase<Firm>, IFirm
    {
        /// <summary>
        /// Реальный объект фирмы.
        /// </summary>
        private readonly Firm firmEF;

        /// <summary>
        /// Создать обертку фирмы на основе реальной фирмы EF.
        /// </summary>
        /// <param name="firm">Объект фирмы EF.</param>
        public FirmWrap(Firm firm)
        {
            this.firmEF = firm;
        }

        /// <summary>
        /// Создать обертку фирмы на основе любого интерфейса фирмы.
        /// </summary>
        /// <param name="iFirm">Интерфейс фирмы.</param>
        public FirmWrap(IFirm iFirm)
        {
            this.firmEF = new Firm()
            {
                FirmId = iFirm.FirmId,
                Name = iFirm.Name
            };
        }

        /// <summary>
        /// Id фирмы.
        /// </summary>
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

        /// <summary>
        /// Название фирмы.
        /// </summary>
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

        /// <summary>
        /// Получить объект EF фирмы.
        /// </summary>
        /// <returns>Объект EF фирмы.</returns>
        public Firm GetRealObject()
        {
            return this.firmEF;
        }
    }
}
