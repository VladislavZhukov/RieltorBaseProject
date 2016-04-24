
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using RieltorBase.Domain;

namespace RieltorBase.WebSite.Controllers
{
    /// <summary>
    /// ВСЕ Firm заменить на FirmInfo(обертку)!!!!!!!!!!!
    /// </summary>
    public class FirmsController : ApiController
    {
        private readonly VolgaInfoDBEntities context =
            new VolgaInfoDBEntities();

        // GET api/v1/firms
        public IEnumerable<Firm> Get()
        {
            return this.context.Firms.ToList();
        }

        // api/v1/Firms?nameFirm=ИмяФирмы
        public IEnumerable<Firm> Get(
            string nameFirm)
        {
            try
            {
                var firmFound = context.Firms.Where(w => w.Name.Contains(nameFirm)).ToList();
                if (firmFound.Count != 0)
                {
                    return firmFound;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // GET api/v1/firms/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/v1/firms
        public void Post([FromBody]Firm newFirm)
        {
            try
            {
                //Firm newFirm = FirmInfo.Сделать_нормальную_фирму()

                //должен ли я удолять пробелы, или это должно решаться на уровне интерфейса?
                newFirm.Name = newFirm.Name.Trim();
                if (context.Firms.Count(w => w.Name == newFirm.Name) == 0)
                {
                    context.Firms.Add(newFirm);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // PUT api/v1/firms/5
        public void Put(int id, [FromBody]Firm firm)
        {
            try
            {
                //данные улетают в базу даде без saveChanges
                //context.Firms.Where(f => f.FirmId == newFirm.FirmId).Update(f => new Firm() { Name = newFirm.Name });
                //Осторожно, запрос уже улетел в БД 
                //и, несмотря на отсутствие вызова  context.SaveChanges(), данные будут изменены
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // DELETE api/v1/firms/5
        public void Delete(int id)
        {
            try
            {
                //Firm newFirm = FirmInfo.Сделать_нормальную_фирму()

                //context.Firms.Remove(
                //    this.context.Where(w => w.FirmId == id).ToList().First());
                //Осторожно, запрос уже улетел в БД 
                //и, несмотря на отсутствие вызова  context.SaveChanges(), данные были удалены
                //context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
