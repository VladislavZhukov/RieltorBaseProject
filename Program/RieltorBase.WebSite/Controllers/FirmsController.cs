using System;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text;

namespace RieltorBase.WebSite.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    using RieltorBase.Domain.Interfaces;

    using RieltorBase.WebSite.JsonCompatibleClasses;

    /// <summary>
    /// Web API Контроллер для работы с фирмами.
    /// </summary>
    public class FirmsController : ApiController
    {
        /// <summary>
        /// Репозиторий фирм.
        /// </summary>
        private readonly IFirmsRepository firmsRepo =
            RBDependencyResolver.Current.Resolve<IFirmsRepository>();

        /// <summary>
        /// Метод обработки запроса GET без параметров.
        /// </summary>
        /// <returns>Все фирмы из репозитория.</returns>
        /// <remarks>Пример запроса: GET api/v1/firms.</remarks>
        public IEnumerable<IFirm> Get()
        {
            return this.firmsRepo.GetAll().ToList();
        }

        /// <summary>
        /// Метод обработки запроса GET с параметром имени фирмы.
        /// </summary>
        /// <param name="nameFirm">Часть имени фирмы.</param>
        /// <returns>Найденные фирмы.</returns>
        /// <remarks>Пример запроса: GET api/v1/Firms?nameFirm=ИмяФирмы</remarks>
        public IEnumerable<IFirm> Get(
            string nameFirm)
        {
            return this.firmsRepo.FindByName(nameFirm);
        }

        /// <summary>
        /// Метод обработки запроса GET с параметром ID фирмы.
        /// </summary>
        /// <param name="id">Id фирмы.</param>
        /// <returns>Фирма с заданным Id.</returns>
        /// <remarks>Пример запроса: GET api/v1/firms/5</remarks>
        public IFirm Get(int id)
        {
            return this.firmsRepo.Find(id);
        }

        /// <summary>
        /// Метод обработки запроса на добавление фирмы.
        /// </summary>
        /// <param name="newFirm">Объект новой фирмы (из тела запроса).</param>
        /// <remarks>Пример запроса: POST api/v1/firms
        /// (в теле запроса - JSON-объект фирмы).</remarks>
        public void Post([FromBody]JsonFirm newFirm)
        {

            this.AuthorizeAdmin();
            this.firmsRepo.Add(newFirm);
            this.firmsRepo.SaveChanges();
        }

        /// <summary>
        /// Метод обработки запроса на обновление фирмы.
        /// </summary>
        /// <param name="id">Id обновляемой фирмы.</param>
        /// <param name="firm">Объект обновляемой фирмы 
        /// (из тела запроса).</param>
        /// <remarks>Пример запроса: PUT api/v1/firms/5 
        /// (в теле запроса - JSON-объект фирмы).</remarks>
        public void Put(int id, [FromBody]JsonFirm firm)
        {
            this.AuthorizeAdmin();
            this.firmsRepo.Update(firm);
            this.firmsRepo.SaveChanges();
        }

        /// <summary>
        /// Обработка запроса на удаление фирмы.
        /// </summary>
        /// <param name="id">Id удаляемой фирмы.</param>
        /// <remarks>Пример запроса: DELETE api/v1/firms/5.</remarks>
        public void Delete(int id)
        {
            this.AuthorizeAdmin();
            this.firmsRepo.Delete(id);
            this.firmsRepo.SaveChanges();
        }

        /// <summary>
        /// Провести аутентификацию (убедиться, что это администратор) 
        /// и авторизацию (не вызывать исключение, что позволит выполнить дальнейшие операции)
        /// пользователя.
        /// </summary>
        private void AuthorizeAdmin()
        {
            AuthenticationHeaderValue auth = this.Request.Headers.Authorization;

            if (auth == null || auth.Scheme != "Basic")
            {
                throw new AuthenticationException(
                    "Нет прав на добавление, изменение или удаление фирм.");
            }

            string encodedCredentials = auth.Parameter; 
            byte[] credentialBytes = Convert.FromBase64String(encodedCredentials); 
            string[] credentials = Encoding.ASCII.GetString(credentialBytes).Split(':');

            if (credentials[0] != "Admin")
            {
                throw new AuthenticationException(
                    "Неправильный логин. Должно быть: \"Admin\".");
            }

            if (credentials[1] != "123")
            {
                throw new AuthenticationException(
                    "Неправильный пароль. Должно быть \"123\"");
            }
        }
    }
}
