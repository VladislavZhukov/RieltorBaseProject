namespace RieltorBase.Domain.Users
{
    using System.Collections.Generic;

    public class Agent
    {
        private readonly List<UserCommand> cancelableCommands
            = new List<UserCommand>();

        private readonly List<UserCommand> repeatableCommands
            = new List<UserCommand>();

        /// <summary>
        /// Отменить последнее действие.
        /// </summary>
        public void CancelLastOperation()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Повторить отмененное действие.
        /// </summary>
        public void RepeatLastOperation()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Получить объекты недвижимости агента.
        /// </summary>
        /// <returns>Набор объектов недвижимости агента.</returns>
        public object[] GetOwnAppartments()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Добавить новый объект недвижимости.
        /// </summary>
        /// <returns>Созданный пустой объект недвижимости.</returns>
        private object AddAppartment()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Обновить данные объекта недвижимости.
        /// </summary>
        /// <param name="appartment">Объект недвижимости.</param>
        private void UpdateAppartment(object appartment)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Удалить объект недвижимости.
        /// </summary>
        /// <param name="appartment">Объект недвижимости.</param>
        private void RemoveAppartment(object appartment)
        {
            throw new System.NotImplementedException();
        }
    }
}
