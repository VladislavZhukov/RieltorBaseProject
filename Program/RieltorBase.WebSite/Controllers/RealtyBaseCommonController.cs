namespace RieltorBase.WebSite.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http.Headers;
    using System.Security.Authentication;
    using System.Text;
    using System.Web.Http;

    using RieltorBase.Domain.Interfaces;

    /// <summary>
    /// Общая часть любого контроллера "Базы данных недвижимости".
    /// </summary>
    public abstract class RealtyBaseCommonController : ApiController
    {
        /// <summary>
        /// Механизм аутентификации.
        /// </summary>
        private readonly IAuthenticationMechanism authentication
            = RBDependencyResolver.Current
            .CreateInstance<IAuthenticationMechanism>();

        /// <summary>
        /// Механизм авторизации.
        /// </summary>
        private IAuthorizationMechanism authorization;

        /// <summary>
        /// Механизм авторизации.
        /// </summary>
        protected IAuthorizationMechanism AuthorizationMechanism
        {
            get
            {
                return this.authorization ?? 
                    (this.authorization = RBDependencyResolver.Current.CreateInstance<IAuthorizationMechanism>());
            }
        }

        /// <summary>
        /// Достоверная информация о текущем пользователе.
        /// </summary>
        /// <remarks>Достоверная - проверенная, т.е. пользователь
        /// прошел процедуру аутентификации.</remarks>
        protected UserInfo CurrentUserInfo
        {
            get
            {
                KeyValuePair<string, string> loginAndPassword =
                this.GetLoginAndPassword();

                UserInfo user = this.authentication.Authenticate(
                    loginAndPassword.Key,
                    loginAndPassword.Value);

                if (user == null)
                {
                    throw new AuthenticationException(
                        "Недостаточно прав для совершения данной операции.");
                }

                return user;
            }
        }

        /// <summary>
        /// Подтвердить права на совершение любых действий 
        /// (включает процедуру аутентификации и авторизации).
        /// </summary>
        /// <param name="exceptionMessage">Сообщение в случае отсутствия прав.</param>
        /// <exception cref="AuthenticationException">Если пользователь
        /// не является глобальным администратором.</exception>
        protected void AuthorizeGlobalAdmin(
            string exceptionMessage)
        {
            if (!AuthorizationMechanism.IsUserGlobalAdmin(this.CurrentUserInfo))
            {
                throw new AuthenticationException(exceptionMessage);
            }
        }

        /// <summary>
        /// Подтвердить права пользователя на чтение данных
        /// (включает процедуру аутентификации и авторизации).
        /// </summary>
        /// <param name="exceptionMessage">Сообщение в случае отсутствия прав.</param>
        /// <exception cref="AuthenticationException">Если пользователь
        /// не имеет прав на чтение данных.</exception>
        protected void AuthorizeUserToReadData(
            string exceptionMessage)
        {
            if (!AuthorizationMechanism.CanUserReadData(this.CurrentUserInfo))
            {
                throw new AuthenticationException(exceptionMessage);
            }
        }

        /// <summary>
        /// Получить логин и пароль, которые были переданы в запросе.
        /// </summary>
        /// <returns>Логин и пароль.</returns>
        private KeyValuePair<string, string> GetLoginAndPassword()
        {
            AuthenticationHeaderValue auth = this.Request.Headers.Authorization;

            if (auth == null || auth.Scheme != "Basic")
            {
                throw new AuthenticationException(
                    "В запросе не передан заголовок авторизации (или заголовок не корректен - не соответствует схеме Basic).");
            }

            string encodedCredentials = auth.Parameter;
            byte[] credentialBytes = Convert.FromBase64String(encodedCredentials);
            string[] credentials = Encoding.ASCII.GetString(credentialBytes).Split(':');

            string login = credentials[0];
            string password = credentials[1];

            if (string.IsNullOrWhiteSpace(login)
                || string.IsNullOrWhiteSpace(password))
            {
                throw new AuthenticationException(
                    "Ни логин, ни пароль, не должны быть пустыми.");
            }

            return new KeyValuePair<string, string>(login, password);
        }
    }
}