namespace RieltorBase.WebSite.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Security.Authentication;
    using System.Web.Http;

    using RieltorBase.Domain.Interfaces;
    using RieltorBase.WebSite.JsonCompatibleClasses;
    using System.Net.Http;
    using System.Net;
    
    /// <summary>
    /// Web API Контроллер для работы с объектами недвижимости.
    /// </summary>
    public class RealtyObjectsController : RealtyBaseCommonController
    {
        /// <summary>
        /// Репозиторий объектов недвижимости.
        /// </summary>
        private readonly IRealtyObjectsRepository realtyObjects
            = RBDependencyResolver.Current.CreateInstance<IRealtyObjectsRepository>();

        /// <summary>
        /// Сообщение об отсутствии прав на чтение.
        /// </summary>
        private const string DontHaveRightsToRead
            = "Пользователь не имеет прав на просмотр объектов недвижимости.";

        /// <summary>
        /// Метод обработки запроса GET без параметров.
        /// </summary>
        /// <returns>Все объекты недвижимости.</returns>
        /// <remarks>Пример запроса: 
        /// GET api/realtyobjects/api/v1/RealtyObjects.</remarks>
        public IEnumerable<IRealtyObject> Get()
        {
            /*
                Exception Handling in ASP.NET Web API:
                http://www.asp.net/web-api/overview/error-handling/exception-handling

                Необязательно читать:
                ASP.NET Web API: Correct way to return a 401 / unauthorised response:
                http://stackoverflow.com/questions/31205599/asp-net-web-api-correct-way-to-return-a-401-unauthorised-response
                How do you return status 401 from WebAPI to AngularJS and also include a custom message?:
                http://stackoverflow.com/questions/23025884/how-do-you-return-status-401-from-webapi-to-angularjs-and-also-include-a-custom
            */


            try
            {
                this.AuthorizeUserToReadData(
                    RealtyObjectsController.DontHaveRightsToRead);

                return this.realtyObjects.GetAll();
            }
            catch (AuthenticationException e)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                {
                    Content = new StringContent(e.Message)//,
                    //ReasonPhrase = "Product ID Not Found"
                };
                throw new HttpResponseException(resp);
            }
        }

        /// <summary>
        /// Метод обработки запроса GET с параметрами поиска.
        /// </summary>
        /// <param name="minCost">Минимальная стоимость.</param>
        /// <param name="maxCost">Максимальная стоимость.</param>
        /// <param name="partOfAddress">Часть адреса.</param>
        /// <param name="realtyObjectType">Имя типа объектов недвижимости.</param>
        /// <param name="minDate">Минимальная дата.</param>
        /// <param name="maxDate">Максимальная дата.</param>
        /// <returns>Объекты недвижимости, удовлетворяющие условиям поиска.</returns>
        /// <remarks>Пример запроса:
#pragma warning disable 1570
        /// GET api/v1/RealtyObjects?minCost=2&maxCost=35&partOfAddress=Ленина&realtyObjectType=Квартиры&minDate=5.11.16&maxDate=10.11.16</remarks>
#pragma warning restore 1570
        public IEnumerable<IRealtyObject> Get(
            int minCost, 
            int maxCost, 
            string partOfAddress, 
            string realtyObjectType, 
            DateTime minDate, 
            DateTime maxDate)
        {
            this.AuthorizeUserToReadData(
                RealtyObjectsController.DontHaveRightsToRead);

            RealtyObjectSearchOptions options = 
                new RealtyObjectSearchOptions()
                {
                    MinCost = minCost,
                    MaxCost = maxCost,
                    PartOfAddress = partOfAddress,
                    RealtyObjectType = realtyObjectType,
                    MinDate = minDate,
                    MaxDate = maxDate
                };

            return this.realtyObjects.FindByParams(options);
        }

        /// <summary>
        /// Метод обработки запроса GET с параметром ID.
        /// </summary>
        /// <param name="id">Id объекта недвижимости.</param>
        /// <returns>Объект недвижимости с заданным Id.</returns>
        /// <remarks>Пример запроса: GET api/realtyobjects/5.</remarks>
        public IRealtyObject Get(int id)
        {
            this.AuthorizeUserToReadData(
                RealtyObjectsController.DontHaveRightsToRead);

            return this.realtyObjects.Find(id);
        }

        /// <summary>
        /// Метод обработки запроса на добавление объекта недвижимости.
        /// </summary>
        /// <param name="value">Новый объект недвижимости 
        /// (в теле запроса).</param>
        /// <returns>Добавленный объект недвижимости.</returns>
        /// <remarks>Пример запроса: POST api/realtyobjects
        /// (в теле запроса - JSON-объект недвижимости).</remarks>
        public IRealtyObject Post([FromBody]JsonRealtyObject value)
        {
            bool canAddRealtyObject = this.AuthorizationMechanism
                .CanUserAddRealtyObject(this.CurrentUserInfo, value);

            if (!canAddRealtyObject)
            {
                throw new AuthenticationException(
                    "Данному пользователю не разрешено добавлять данный объект недвижимости.");
            }

            IRealtyObject newObj = this.realtyObjects.Add(value);
            this.realtyObjects.SaveChanges();
            return newObj;
        }

        /// <summary>
        /// Метод обработки запроса на обновление объекта недвижимости.
        /// </summary>
        /// <param name="id">Id обновляемого объекта недвижимости.</param>
        /// <param name="value">Обновленный объект недвижимости
        /// (в теле запроса).</param>
        /// <returns>Обновленный объект недвижимости.</returns>
        /// <remarks>Пример запроса: PUT api/realtyobjects/5
        /// (в теле запроса - JSON-объект недвижимости).</remarks>
        public IRealtyObject Put(int id, [FromBody]JsonRealtyObject value)
        {
            bool canUpdateRealtyObject = this.AuthorizationMechanism
                .CanUserUpdateRealtyObject(this.CurrentUserInfo, value);

            if (!canUpdateRealtyObject)
            {
                throw new AuthenticationException(
                    "Данному пользователю не разрешено изменять данный объект недвижимости.");
            }

            IRealtyObject updatedObj = this.realtyObjects.Update(value);
            this.realtyObjects.SaveChanges();
            return updatedObj;
        }

        /// <summary>
        /// Метод обработки запроса на удаление объекта недвижимости.
        /// </summary>
        /// <param name="id">Id удаляемого объекта недвижимости.</param>
        /// <remarks>Пример запроса: DELETE api/realtyobjects/5.</remarks>
        public void Delete(int id)
        {
            IRealtyObject obj = this.realtyObjects.Find(id);

            bool canDelete = this.AuthorizationMechanism
                .CanUserDeleteRealtyObject(this.CurrentUserInfo, obj);

            if (!canDelete)
            {
                throw new AuthenticationException(
                    "Данному пользователю не разрешено удалять данный объект недвижимости.");
            }

            this.realtyObjects.Delete(id);
            this.realtyObjects.SaveChanges();
        }
    }
}
