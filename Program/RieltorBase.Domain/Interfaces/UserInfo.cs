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
    }
}
