namespace RieltorBase.Domain.Interfaces
{
    /// <summary>
    /// Информация о зарегистрированном пользователе.
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// Id пользователя.
        /// </summary>
        public int AgentId;

        /// <summary>
        /// Имя агента.
        /// </summary>
        public string AgentName;

        /// <summary>
        /// Телефон агента.
        /// </summary>
        public string AgentPhone;

        /// <summary>
        /// Id фирмы, в которой работает пользователь.
        /// </summary>
        public int FirmId;

        /// <summary>
        /// Название фирмы, в которой работает пользователь.
        /// </summary>
        public string FirmName;

        /// <summary>
        /// Пользователь - директор фирмы.
        /// </summary>
        public bool IsFirmAdmin;

        /// <summary>
        /// Пользователь - администратор, имеющий все возможные права.
        /// </summary>
        public bool IsGlobalAdmin;
    }
}
