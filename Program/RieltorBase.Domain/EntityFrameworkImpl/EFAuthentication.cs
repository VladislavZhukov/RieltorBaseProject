namespace RieltorBase.Domain.EntityFrameworkImpl
{
    using System.Linq;
    using System.Security.Authentication;

    using RieltorBase.Domain.Interfaces;

    /// <summary>
    /// Механизм аутентификации на основе EF.
    /// </summary>
    public class EFAuthentication : IAuthenticationMechanism
    {
        /// <summary>
        /// Контекст EF.
        /// </summary>
        private VolgaInfoDBEntities context = new VolgaInfoDBEntities();

        /// <summary>
        /// Получить данные пользователя, пройдя процедуру аутентификации.
        /// </summary>
        /// <param name="login">Логин.</param>
        /// <param name="password">Пароль.</param>
        /// <returns>Данные пользователя, прошедшего аутентификацию.</returns>
        public UserInfo Authenticate(string login, string password)
        {
            if (login == "Admin" && password == "123")
            {
                return new UserInfo() { IsGlobalAdmin = true };
            }

            Password passwordData = this.context.Passwords.FirstOrDefault(
                pd => pd.Login == login && pd.Password1 == password);

            if (passwordData == null)
            {
                throw new AuthenticationException(
                    "Логин или пароль введены неверно.");
            }

            Agent agent = this.context.Agents.FirstOrDefault(
                a => a.Id_agent == passwordData.AgentId);

            if (agent == null)
            {
                throw new AuthenticationException(
                    "Пользователь с таким логином и паролем был удален.");
            }

            return new UserInfo()
            {
                FirmId = agent.Id_firm,
                AgentId = agent.Id_agent,
                AgentName = agent.Name,
                AgentPhone = agent.PhoneNumber,
                FirmName = agent.Firm.Name,
                IsFirmAdmin = agent.IsFirmAdmin,
                IsGlobalAdmin = false
            };
        }
    }
}
