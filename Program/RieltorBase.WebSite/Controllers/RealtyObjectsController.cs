namespace RieltorBase.WebSite.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    using RieltorBase.Domain;
    using RieltorBase.Domain.InfoClasses;

    public class RealtyObjectsController : ApiController
    {
        VolgaInfoDBEntities context = new VolgaInfoDBEntities();

        // GET api/realtyobjects/api/v1/RealtyObjects
        public IEnumerable<RealtyObject> Get()
        {
            List<RealtyObject> result = this.context.RealtyObjects.ToList();
            return result;
        }

        // GET api/realtyobjects/api/v1/RealtyObjects?minCost=2&maxCost=35&partOfAddress=Ленина&realtyObjectType=Квартира&minDate=22.11.16&maxDate=24.11.16
        public IEnumerable<RealtyObject> Get(
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

            // сейчас возвращаются ВСЕ квартиры, независимо от параметров поиска.
            return this.context.RealtyObjects.ToList();
        }

        // GET api/realtyobjects/5
        public string Get(int id)
        {
            // заменить на нормальный Response
            throw new NotImplementedException();
        }

        // POST api/realtyobjects
        public void Post([FromBody]string value)
        {
            // заменить на нормальный Response
            throw new NotImplementedException();
        }

        // PUT api/realtyobjects/5
        public void Put(int id, [FromBody]string value)
        {
            // заменить на нормальный Response
            throw new NotImplementedException();
        }

        // DELETE api/realtyobjects/5
        public void Delete(int id)
        {
            // заменить на нормальный Response
            throw new NotImplementedException();
        }
    }
}
