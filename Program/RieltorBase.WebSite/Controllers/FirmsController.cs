namespace RieltorBase.WebSite.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Authentication;
    using System.Web.Http;

    using RieltorBase.Domain.Interfaces;

    using RieltorBase.WebSite.JsonCompatibleClasses;

    /// <summary>
    /// Web API Контроллер для работы с фирмами.
    /// </summary>
    public class FirmsController : RealtyBaseCommonController
    {
        /// <summary>
        /// Репозиторий фирм.
        /// </summary>
        private readonly IFirmsRepository firmsRepo =
            RBDependencyResolver.Current.CreateInstance<IFirmsRepository>();

        /// <summary>
        /// Сообщение об отсутствии прав на чтение.
        /// </summary>
        private const string DontHaveAccessToRead = 
            "Пользователь не имеет доступа к информации о фирмах.";

        /// <summary>
        /// Метод обработки запроса GET без параметров.
        /// </summary>
        /// <returns>Все фирмы из репозитория.</returns>
        /// <remarks>Пример запроса: GET api/v1/firms.</remarks>
        public IEnumerable<IFirm> Get()
        {
            this.AuthorizeUserToReadData(FirmsController.DontHaveAccessToRead);
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
            this.AuthorizeUserToReadData(FirmsController.DontHaveAccessToRead);
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
            this.AuthorizeUserToReadData(FirmsController.DontHaveAccessToRead);
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
            this.AuthorizeGlobalAdmin(
                "Только глобальный администратор может добавлять фирмы.");

            this.firmsRepo.Add(newFirm);
            this.firmsRepo.SaveChanges();
        }

        /// <summary>
        /// Метод обработки запроса на обновление фирмы.
        /// </summary>
        /// <param name="id">Id обновляемой фирмы.</param>
        /// <param name="updatedFirm">Объект обновляемой фирмы 
        /// (из тела запроса).</param>
        /// <remarks>Пример запроса: PUT api/v1/firms/5 
        /// (в теле запроса - JSON-объект фирмы).</remarks>
        public void Put(int id, [FromBody]JsonFirm updatedFirm)
        {
            IFirm existingFirm = this.firmsRepo.Find(updatedFirm.FirmId);

            if (updatedFirm.Name != existingFirm.Name)
            {
                this.AuthorizeGlobalAdmin(
                    "Только глобальный администратор может изменять название фирмы.");
            }
            else
            {
                if (!this.AuthorizationMechanism.CanUserEditFirm(
                    this.CurrentUserInfo, existingFirm))
                {
                    throw new AuthenticationException(
                        "Данный пользователь не может изменять данные этой фирмы.");
                }
            }

            this.firmsRepo.Update(updatedFirm);
            this.firmsRepo.SaveChanges();
        }

        /// <summary>
        /// Обработка запроса на удаление фирмы.
        /// </summary>
        /// <param name="id">Id удаляемой фирмы.</param>
        /// <remarks>Пример запроса: DELETE api/v1/firms/5.</remarks>
        public void Delete(int id)
        {
            this.AuthorizeGlobalAdmin(
                "Только глобальный администратор может удалять фирмы.");

            this.firmsRepo.Delete(id);
            this.firmsRepo.SaveChanges();
        }
    }
}
