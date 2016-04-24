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

        }

        /// <summary>
        /// Удоляет фирмы из базы
        /// </summary>
        /// <param name="newFirm"></param>
        /// <param name="context"></param>
        public static void Delete(Firm newFirm, VolgaInfoDBEntities context)
        {
            

        }

        /// <summary>
        /// Изменяет даннные в базе
        /// </summary>
        /// <param name="newFirm"></param>
        /// <param name="context"></param>
        public static void Change(Firm newFirm, VolgaInfoDBEntities context)
        {

        }

        /// <summary>
        /// Возвращает фирмы по названию
        /// </summary>
        /// <param name="nameFirm"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static List<Firm> Get(string nameFirm, VolgaInfoDBEntities context)
        {
            return null;
        }
    }
}
