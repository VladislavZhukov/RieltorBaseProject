namespace RieltorBase.Domain.EntityFrameworkImpl
{
    using System.Collections.Generic;
    using System.Linq;

    using RieltorBase.Domain.Interfaces;

    public class FirmsRepository : EFRepository<IFirm>, IFirmsRepository
    {
        public override IEnumerable<IFirm> GetAll()
        {
            return this.Context.Firms.ToList().Select(
                firm => new FirmWrap(firm));
        }

        public override IFirm Find(int id)
        {
            Firm firm = this.Context.Firms.Find(id);
            return firm != null ? new FirmWrap(firm) : null;
        }

        public IEnumerable<IFirm> FindByName(string partOfName)
        {
            IQueryable<Firm> firms = this.Context.Firms.Where(
                f => f.Name.Contains(partOfName));
            return firms.ToList().Select(f => new FirmWrap(f));
        }

        public override IFirm Add(IFirm newEntity)
        {
            FirmWrap wrap = new FirmWrap(newEntity);
            this.Context.Firms.Add(wrap.GetRealObject());
            return wrap;
        }

        public override IFirm Update(IFirm changedEntity)
        {
            Firm updatedFirm = this.Context.Firms.First(firm => 
                firm.FirmId == changedEntity.FirmId);

            FirmWrap wrap = new FirmWrap(changedEntity);
            wrap.UpdateRealObject(updatedFirm);

            return wrap;
        }

        public override void Delete(int id)
        {
            this.Context.Firms.Remove(
                this.Context.Firms.First(firm => firm.FirmId == id));
        }
    }
}
