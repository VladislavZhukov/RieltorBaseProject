namespace RieltorBase.WebSite.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;

    using RieltorBase.Domain;

    public class RealtyObjectsController : ApiController
    {
        // GET api/realtyobjects
        public IEnumerable<string> Get(
            int minCost, 
            int maxCost, 
            int partOfAddress, 
            string realtyObjectType, 
            DateTime minDate, 
            DateTime maxDate)
        {
            return new string[] { "value1", "value2" };
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
