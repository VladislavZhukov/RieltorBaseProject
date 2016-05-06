namespace RieltorBase.Domain.Interfaces
{
    /// <summary>
    /// Механизм аутентификации.
    /// </summary>
    public interface IAuthenticationMechanism
    {
        /// <summary>
        /// Провести идентификацию (установить, какой это пользователь)
        /// и аутентификацию (проверить, действительно ли это тот пользователь).
        /// </summary>
        /// <param name="login">Логин пользователя.</param>
        /// <param name="password">Пароль пользователя.</param>
        /// <returns>Проверенные данные пользователя.</returns>
        UserInfo Authenticate(string login, string password);
    }
}
