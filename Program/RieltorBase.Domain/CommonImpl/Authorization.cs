namespace RieltorBase.Domain.CommonImpl
{
    using RieltorBase.Domain.Interfaces;

    /// <summary>
    /// Общий механизм авторизации.
    /// </summary>
    public class Authorization : IAuthorizationMechanism
    {
        /// <summary>
        /// Может ли пользователь смотреть общедоступную информацию.
        /// </summary>
        /// <param name="user">Информация о пользователе.</param>
        /// <returns>True - есть права на чтение, false - нет.</returns>
        /// <remarks>Относится ко всей общедоступной информации
        /// (фирмы, объекты недвижимости, фотографии и т.д.).</remarks>
        public bool CanUserReadData(UserInfo user)
        {
            return user != null;
        }

        /// <summary>
        /// Может ли пользователь добавить объект недвижимости.
        /// </summary>
        /// <param name="user">Информация о пользователе.</param>
        /// <param name="object">Объект недвижимости.</param>
        /// <returns>True - права есть, false - нет.</returns>
        public bool CanUserAddRealtyObject(
            UserInfo user, 
            IRealtyObject @object)
        {
            return user != null
                && @object != null
                && this.CanActWithRealtyObject(user, @object);
        }

        /// <summary>
        /// Может ли пользователь обновить объект недвижимости.
        /// </summary>
        /// <param name="user">Информация о пользователе.</param>
        /// <param name="object">Объект недвижимости.</param>
        /// <returns>True - права есть, false - нет.</returns>
        public bool CanUserUpdateRealtyObject(
            UserInfo user, 
            IRealtyObject @object)
        {
            return user != null
                && @object != null
                && this.CanActWithRealtyObject(user, @object);
        }

        /// <summary>
        /// Может ли пользователь удалить объект недвижимости.
        /// </summary>
        /// <param name="user">Информация о пользователе.</param>
        /// <param name="object">Объект недвижимости.</param>
        /// <returns>True - права есть, false - нет.</returns>
        public bool CanUserDeleteRealtyObject(
            UserInfo user, 
            IRealtyObject @object)
        {
            return user != null
                && @object != null
                && this.CanActWithRealtyObject(user, @object);
        }

        /// <summary>
        /// Может ли пользователь совершать все операции с фирмой.
        /// </summary>
        /// <param name="user">Информация о пользователе.</param>
        /// <param name="firm">Фирма.</param>
        /// <returns>True - права есть, false - нет.</returns>
        public bool CanUserEditFirm(
            UserInfo user, 
            IFirm firm)
        {
            return user != null
                && firm != null
                && (user.IsGlobalAdmin || this.IsUserFirmAdmin(user, firm));
        }

        /// <summary>
        /// Может ли пользователь добавить агента.
        /// </summary>
        /// <param name="user">Информация о пользователе.</param>
        /// <param name="agent">Агент недвижимости.</param>
        /// <returns>True - права есть, false - нет.</returns>
        public bool CanUserAddAgent(
            UserInfo user,
            IAgent agent)
        {
            return user.IsGlobalAdmin
                || this.IsUserFirmAdmin(user, agent);
        }

        /// <summary>
        /// Может ли пользователь изменить агента.
        /// </summary>
        /// <param name="user">Информация о пользователе.</param>
        /// <param name="agent">Агент недвижимости.</param>
        /// <returns>True - права есть, false - нет.</returns>
        public bool CanUserChangeAgent(
            UserInfo user,
            IAgent agent)
        {
            return user.IsGlobalAdmin
                || this.IsUserFirmAdmin(user, agent)
                || user.AgentId == agent.Id_agent;
        }

        /// <summary>
        /// Может ли пользователь удалить агента.
        /// </summary>
        /// <param name="user">Информация о пользователе.</param>
        /// <param name="agent">Агент недвижимости.</param>
        /// <returns>True - права есть, false - нет.</returns>
        public bool CanUserRemoveAgent(
            UserInfo user,
            IAgent agent)
        {
            return user.IsGlobalAdmin
                || this.IsUserFirmAdmin(user, agent);
        }

        /// <summary>
        /// Может ли пользователь редактировать фотографии фирмы.
        /// </summary>
        /// <param name="user">Информация о пользователе.</param>
        /// <param name="firm">Фирма.</param>
        /// <returns>True - права есть, false - нет.</returns>
        public bool UserHasAccessToPhotos(
            UserInfo user, 
            IFirm firm)
        {
            return this.CanUserEditFirm(user, firm);
        }

        /// <summary>
        /// Может ли пользователь редактировать фотографии объекта недвижимости.
        /// </summary>
        /// <param name="user">Информация о пользователе.</param>
        /// <param name="realtyObject">Объект недвижимости.</param>
        /// <returns>True - права есть, false - нет.</returns>
        public bool UserHasAccessToPhotos(
            UserInfo user, 
            IRealtyObject realtyObject)
        {
            return user.IsGlobalAdmin ||
                this.RealtyObjectBelongsToUser(user, realtyObject);
        }

        /// <summary>
        /// Пользователь может совершать любые действия с объектом недвижимости.
        /// </summary>
        /// <param name="user">Информация о пользователе.</param>
        /// <param name="object">Объект недвижимости.</param>
        /// <returns>True - права есть, false - нет.</returns>
        private bool CanActWithRealtyObject(UserInfo user, IRealtyObject @object)
        {
            return user.IsGlobalAdmin
                || this.IsUserFirmAdmin(user, @object)
                || this.RealtyObjectBelongsToUser(user, @object);
        }

        /// <summary>
        /// Пользователь является директором фирмы.
        /// </summary>
        /// <param name="user">Информация о пользователе.</param>
        /// <param name="firm">Фирма.</param>
        /// <returns>True - пользователь - директор фирмы, false - нет.</returns>
        private bool IsUserFirmAdmin(UserInfo user, IFirm firm)
        {
            return user.FirmId == firm.FirmId;
        }

        /// <summary>
        /// Пользователь является директором фирмы, в которой работает агент.
        /// </summary>
        /// <param name="user">Информация о пользователе.</param>
        /// <param name="agent">Агент.</param>
        /// <returns>True - пользователь - директор фирмы, false - нет.</returns>
        private bool IsUserFirmAdmin(UserInfo user, IAgent agent)
        {
            return user.FirmId == agent.Id_firm && user.IsFirmAdmin;
        }

        /// <summary>
        /// Пользователь является директором фирмы, в которой
        /// работает агент, чей объект недвижимости редактируется.
        /// </summary>
        /// <param name="user">Информация о пользователе.</param>
        /// <param name="object">Объект недвижимости.</param>
        /// <returns>True - пользователь - директор фирмы, к которой
        /// относится объект недвижимости.</returns>
        /// <remarks>Директор фирмы может редактировать объекты
        /// недвижимости своих подчиненных.</remarks>
        private bool IsUserFirmAdmin(
            UserInfo user, 
            IRealtyObject @object)
        {
            return user.IsFirmAdmin
                && @object.FirmName == user.FirmName;
        }

        /// <summary>
        /// Объект недвижимости принадлежит пользователю.
        /// </summary>
        /// <param name="user">Информация о пользователе.</param>
        /// <param name="realtyObject">Объект недвижимости.</param>
        /// <returns>True - принадлежит, false - нет.</returns>
        private bool RealtyObjectBelongsToUser(
            UserInfo user, 
            IRealtyObject realtyObject)
        {
            return user.AgentName == realtyObject.AgentName
                && user.FirmName == realtyObject.FirmName
                && user.AgentPhone == realtyObject.Phone;
        }

        /// <summary>
        /// Пользователь является глобальным администратором
        /// и может выполнять любые действия.
        /// </summary>
        /// <param name="user">Информация о пользователе.</param>
        /// <returns>True - пользователь может выполнять любые действия.</returns>
        public bool IsUserGlobalAdmin(UserInfo user)
        {
            return user != null
                && user.IsGlobalAdmin;
        }
    }
}
