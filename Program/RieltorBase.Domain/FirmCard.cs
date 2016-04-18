using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using EntityFramework.Extensions;

namespace RieltorBase.Domain
{
    public static class FirmCard
    {
        /// <summary>
        /// Добавляет фирмы в базу
        /// </summary>
        /// <param name="newFirm">новая фирма</param>
        /// <param name="context"></param>
        public static void Add(Firm newFirm, VolgaInfoDBEntities context)
        {
            try
            {                
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

        /// <summary>
        /// Удоляет фирмы из базы
        /// </summary>
        /// <param name="newFirm"></param>
        /// <param name="context"></param>
        public static void Delete(Firm newFirm, VolgaInfoDBEntities context)
        {
            try
            {
                context.Firms.Where(w => w.Name == newFirm.Name).Delete();
                //Осторожно, запрос уже улетел в БД 
                //и, несмотря на отсутствие вызова  context.SaveChanges(), данные были удалены
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        /// <summary>
        /// Изменяет даннные в базе
        /// </summary>
        /// <param name="newFirm"></param>
        /// <param name="context"></param>
        public static void Change(Firm newFirm, VolgaInfoDBEntities context)
        {
            try
            {
                //данные улетают в базу даде без saveChanges
                context.Firms.Where(f => f.FirmId == newFirm.FirmId).Update(f => new Firm() { Name = newFirm.Name });
                //Осторожно, запрос уже улетел в БД 
                //и, несмотря на отсутствие вызова  context.SaveChanges(), данные будут изменены
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Возвращает фирмы по названию
        /// </summary>
        /// <param name="nameFirm"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static List<Firm> Get(string nameFirm, VolgaInfoDBEntities context)
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
    }
}
