namespace RieltorBase.WebSite.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    using RieltorBase.Domain.Interfaces;

    using RieltorBase.WebSite.JsonCompatibleClasses;

    /// <summary>
    /// ВСЕ Firm заменить на FirmInfo(обертку)!!!!!!!!!!!
    /// </summary>
    public class FirmsController : ApiController
    {
        private readonly IFirmsRepository firmsRepo =
            RBDependencyResolver.Current.Resolve<IFirmsRepository>();

        // GET api/v1/firms
        public IEnumerable<IFirm> Get()
        {
            return this.firmsRepo.GetAll().ToList();
        }

        // api/v1/Firms?nameFirm=ИмяФирмы
        public IEnumerable<IFirm> Get(
            string nameFirm)
        {
            return this.firmsRepo.FindByName(nameFirm);
        }

        // GET api/v1/firms/5
        public IFirm Get(int id)
        {
            return this.firmsRepo.Find(id);
        }

        // POST api/v1/firms
        public void Post([FromBody]JsonFirm newFirm)
        {
            this.firmsRepo.Add(newFirm);
            this.firmsRepo.SaveChanges();
        }

        // PUT api/v1/firms/5
        public void Put(int id, [FromBody]JsonFirm firm)
        {
            this.firmsRepo.Update(firm);
            this.firmsRepo.SaveChanges();
        }

        // DELETE api/v1/firms/5
        public void Delete(int id)
        {
            this.firmsRepo.Delete(id);
            this.firmsRepo.SaveChanges();
        }
    }
}
