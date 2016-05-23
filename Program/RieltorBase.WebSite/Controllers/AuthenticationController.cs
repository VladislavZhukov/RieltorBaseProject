namespace RieltorBase.WebSite.Controllers
{
    using RieltorBase.Domain.Interfaces;

    /// <summary>
    /// Контроллер аутентификации пользователя.
    /// </summary>
    public class AuthenticationController : RealtyBaseCommonController
    {
        /// <summary>
        /// Получить данные пользователя.
        /// </summary>
        /// <returns>Данные пользователя.</returns>
        /// <remarks>Пример запроса: GET api/v1/authentication
        /// (запрос должен содержать заголовок аутентификации (Basic).</remarks>
        public UserInfo Get()
        {
            return this.CurrentUserInfo;
        }
    }
}
