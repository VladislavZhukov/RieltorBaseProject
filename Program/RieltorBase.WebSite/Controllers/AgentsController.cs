namespace RieltorBase.WebSite.Controllers
{
    using System.Collections.Generic;
    using System.Security.Authentication;
    using System.Web.Http;

    using RieltorBase.Domain.Interfaces;
    using RieltorBase.WebSite.JsonCompatibleClasses;

    /// <summary>
    /// Контроллер для работы с агентами.
    /// </summary>
    public class AgentsController : RealtyBaseCommonController
    {
        /// <summary>
        /// Сообщение об отсутствии прав на чтение.
        /// </summary>
        private const string DontHaveAccessToRead =
            "Пользователь не имеет права на просмотр информации об агентах.";

        /// <summary>
        /// Репозиторий (хранилище) агентов.
        /// </summary>
        private IAgentRepository agentsRepository = RBDependencyResolver.Current
            .CreateInstance<IAgentRepository>();

        /// <summary>
        /// Получить всех агентов недвижимости.
        /// </summary>
        /// <returns>Все агенты.</returns>
        /// <remarks>Пример запроса: GET api/v1/agents</remarks>
        public IEnumerable<IAgent> Get()
        {
            this.AuthorizeUserToReadData(
                AgentsController.DontHaveAccessToRead);
            return this.agentsRepository.GetAll();
        }

        /// <summary>
        /// Получить определенного агента недвижимости по id.
        /// </summary>
        /// <param name="id">Id агента недвижимости.</param>
        /// <returns>Агент недвижимости, если есть.</returns>
        /// <remarks>Пример запроса: GET api/v1/agents/5</remarks>
        public IAgent Get(int id)
        {
            this.AuthorizeUserToReadData(
                AgentsController.DontHaveAccessToRead);
            return this.agentsRepository.Find(id);
        }

        /// <summary>
        /// Найти агентов недвижимости по части имени.
        /// </summary>
        /// <param name="partOfName">Часть имени агента.</param>
        /// <returns>Найденные агенты.</returns>
        /// <remarks>Пример запроса: GET api/v1/agents?partOfName=Вася.</remarks>
        public IEnumerable<IAgent> Get(string partOfName)
        {
            this.AuthorizeUserToReadData(
                AgentsController.DontHaveAccessToRead);
            return this.agentsRepository.FindByName(partOfName);
        }

        /// <summary>
        /// Добавить нового агента недвижимости.
        /// </summary>
        /// <param name="value">Новый агент недвижимости.</param>
        /// <returns>Добавленный агент недвижимости.</returns>
        /// <remarks>Пример запроса: POST api/v1/agents
        /// (в теле запроса - <see cref="JsonAgent"/>).</remarks>
        public IAgent Post([FromBody]JsonAgent value)
        {
            bool canAdd = this.AuthorizationMechanism.CanUserAddAgent(
                this.CurrentUserInfo,
                value);

            if (!canAdd)
            {
                throw new AuthenticationException(
                    "Текущий пользователь не может добавить данного агента.");
            }

            IAgent addedAgent = this.agentsRepository.Add(value);
            this.agentsRepository.SaveChanges();
            return addedAgent;
        }

        /// <summary>
        /// Обновить данные агента недвижимости.
        /// </summary>
        /// <param name="id">Id агента недвижимости.</param>
        /// <param name="value">Обновляемый агент недвижимости.</param>
        /// <returns>Обновленный агент недвижимости.</returns>
        /// <remarks>Пример запроса: PUT api/v1/agents/5
        /// (в теле запроса - <see cref="JsonAgent"/>).</remarks>
        public IAgent Put(int id, [FromBody]JsonAgent value)
        {
            bool canChange = this.AuthorizationMechanism.CanUserChangeAgent(
                this.CurrentUserInfo,
                value);

            if (!canChange)
            {
                throw new AuthenticationException(
                    "Текущий пользователь не может изменить информацию о данном агенте.");
            }

            IAgent updatedAgent = this.agentsRepository.Update(value);
            this.agentsRepository.SaveChanges();
            return updatedAgent;
        }

        /// <summary>
        /// Удалить агента недвижимости.
        /// </summary>
        /// <param name="id">Id удаляемого агента.</param>
        /// <remarks>Пример запроса: DELETE api/v1/agents/5.</remarks>
        public void Delete(int id)
        {
            bool canDelete = this.AuthorizationMechanism.CanUserRemoveAgent(
                this.CurrentUserInfo,
                this.agentsRepository.Find(id));

            if (!canDelete)
            {
                throw new AuthenticationException(
                    "Текущий пользователь не может удалить данного агента.");
            }

            this.agentsRepository.Delete(id);
            this.agentsRepository.SaveChanges();
        }
    }
}
