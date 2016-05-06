namespace RieltorBase.Domain.Interfaces
{
    /// <summary>
    /// Механизм авторизации (т.е. предоставления определенных
    /// полномочий пользователям).
    /// </summary>
    public interface IAuthorizationMechanism
    {
        /// <summary>
        /// Может ли пользователь добавить объект недвижимости.
        /// </summary>
        /// <param name="user">Информация о пользователе.</param>
        /// <param name="object">Объект недвижимости.</param>
        /// <returns>True - права есть, false - нет.</returns>
        bool CanUserAddRealtyObject(
            UserInfo user,
            IRealtyObject @object);

        /// <summary>
        /// Может ли пользователь обновить объект недвижимости.
        /// </summary>
        /// <param name="user">Информация о пользователе.</param>
        /// <param name="object">Объект недвижимости.</param>
        /// <returns>True - права есть, false - нет.</returns>
        bool CanUserUpdateRealtyObject(
            UserInfo user,
            IRealtyObject @object);

        /// <summary>
        /// Может ли пользователь удалить объект недвижимости.
        /// </summary>
        /// <param name="user">Информация о пользователе.</param>
        /// <param name="object">Объект недвижимости.</param>
        /// <returns>True - права есть, false - нет.</returns>
        bool CanUserDeleteRealtyObject(
            UserInfo user,
            IRealtyObject @object);

        /// <summary>
        /// Может ли пользователь совершать все операции с фирмой.
        /// </summary>
        /// <param name="user">Информация о пользователе.</param>
        /// <param name="firm">Фирма.</param>
        /// <returns>True - права есть, false - нет.</returns>
        bool IsUserFirmAdmin(
            UserInfo user,
            IFirm firm);
    }
}
