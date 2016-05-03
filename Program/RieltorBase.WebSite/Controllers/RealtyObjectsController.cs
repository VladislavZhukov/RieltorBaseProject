namespace RieltorBase.WebSite.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;

    using RieltorBase.Domain.Interfaces;
    using RieltorBase.WebSite.JsonCompatibleClasses;

    public class RealtyObjectsController : ApiController
    {
        private readonly IRealtyObjectsRepository realtyObjects 
            = RBDependencyResolver.Current.Resolve<IRealtyObjectsRepository>();

        // GET api/realtyobjects/api/v1/RealtyObjects
        public IEnumerable<IRealtyObject> Get()
        {
            return this.realtyObjects.GetAll();
        }

        // GET api/realtyobjects/api/v1/RealtyObjects?minCost=2&maxCost=35&partOfAddress=Ленина&realtyObjectType=Квартира&minDate=22.11.16&maxDate=24.11.16
        public IEnumerable<IRealtyObject> Get(
            int minCost, 
            int maxCost, 
            string partOfAddress, 
            string realtyObjectType, 
            DateTime minDate, 
            DateTime maxDate)
        {
            RealtyObjectSearchOptions options = 
                new RealtyObjectSearchOptions()
                {
                    MinCost = minCost,
                    MaxCost = maxCost,
                    PartOfAddress = partOfAddress,
                    RealtyObjectTypes = realtyObjectType,
                    MinDate = minDate,
                    MaxDate = maxDate
                };

            return this.realtyObjects.FindByParams(options);
        }

        // GET api/realtyobjects/5
        public IRealtyObject Get(int id)
        {
            return this.realtyObjects.Find(id);
        }

        // POST api/realtyobjects
        public IRealtyObject Post([FromBody]JsonRealtyObject value)
        {
            IRealtyObject newObj = this.realtyObjects.Add(value);
            this.realtyObjects.SaveChanges();
            return newObj;
        }

        // PUT api/realtyobjects/5
        public IRealtyObject Put(int id, [FromBody]JsonRealtyObject value)
        {
            IRealtyObject updatedObj = this.realtyObjects.Update(value);
            this.realtyObjects.SaveChanges();
            return updatedObj;
        }

        // DELETE api/realtyobjects/5
        public void Delete(int id)
        {
            this.realtyObjects.Delete(id);
            this.realtyObjects.SaveChanges();
        }
    }
}
