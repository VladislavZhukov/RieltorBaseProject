namespace RieltorBase.WebSite
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Security.Authentication;
    using System.Text;

    /// <summary>
    /// Вспомогательные методы.
    /// </summary>
    internal static class Utilities
    {
        /// <summary>
        /// Получить логин и пароль, которые были переданы в запросе.
        /// </summary>
        /// <param name="request">HTTP-запрос.</param>
        /// <returns>Логин и пароль.</returns>
        internal static KeyValuePair<string, string> GetLoginAndPassword(
            HttpRequestMessage request)
        {
            AuthenticationHeaderValue auth = request.Headers.Authorization;

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