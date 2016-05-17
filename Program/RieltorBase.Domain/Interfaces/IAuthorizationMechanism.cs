namespace RieltorBase.Domain.Interfaces
{
    /// <summary>
    /// Механизм авторизации (т.е. предоставления определенных
    /// полномочий пользователям).
    /// </summary>
    public interface IAuthorizationMechanism
    {
        /// <summary>
        /// Может ли пользователь смотреть общедоступную информацию.
        /// </summary>
        /// <param name="user">Информация о пользователе.</param>
        /// <returns>True - есть права на чтение, false - нет.</returns>
        /// <remarks>Относится ко всей общедоступной информации
        /// (фирмы, объекты недвижимости, фотографии и т.д.).</remarks>
        bool CanUserReadData(
            UserInfo user);

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
        /// Может ли пользователь совершать ВСЕ операции с фирмой.
        /// </summary>
        /// <param name="user">Информация о пользователе.</param>
        /// <param name="firm">Фирма.</param>
        /// <returns>True - права есть, false - нет.</returns>
        bool CanUserEditFirm(
            UserInfo user,
            IFirm firm);

        /// <summary>
        /// Может ли пользователь добавить агента.
        /// </summary>
        /// <param name="user">Информация о пользователе.</param>
        /// <param name="agent">Агент недвижимости.</param>
        /// <returns>True - права есть, false - нет.</returns>
        bool CanUserAddAgent(
            UserInfo user,
            IAgent agent);

        /// <summary>
        /// Может ли пользователь изменить агента.
        /// </summary>
        /// <param name="user">Информация о пользователе.</param>
        /// <param name="agent">Агент недвижимости.</param>
        /// <returns>True - права есть, false - нет.</returns>
        bool CanUserChangeAgent(
            UserInfo user,
            IAgent agent);

        /// <summary>
        /// Может ли пользователь удалить агента.
        /// </summary>
        /// <param name="user">Информация о пользователе.</param>
        /// <param name="agent">Агент недвижимости.</param>
        /// <returns>True - права есть, false - нет.</returns>
        bool CanUserRemoveAgent(
            UserInfo user,
            IAgent agent);

        /// <summary>
        /// Может ли пользователь редактировать фотографии фирмы.
        /// </summary>
        /// <param name="user">Информация о пользователе.</param>
        /// <param name="firm">Фирма.</param>
        /// <returns>True - права есть, false - нет.</returns>
        bool UserHasAccessToPhotos(
            UserInfo user,
            IFirm firm);

        /// <summary>
        /// Может ли пользователь редактировать фотографии объекта недвижимости.
        /// </summary>
        /// <param name="user">Информация о пользователе.</param>
        /// <param name="realtyObject">Объект недвижимости.</param>
        /// <returns>True - права есть, false - нет.</returns>
        bool UserHasAccessToPhotos(
            UserInfo user,
            IRealtyObject realtyObject);

        /// <summary>
        /// Пользователь является глобальным администратором
        /// и может выполнять любые действия.
        /// </summary>
        /// <param name="user">Информация о пользователе.</param>
        /// <returns>True - пользователь может выполнять любые действия.</returns>
        bool IsUserGlobalAdmin(
            UserInfo user);
    }
}
