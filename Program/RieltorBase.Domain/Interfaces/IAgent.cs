namespace RieltorBase.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс агента недвижимости (риэлтора, сотрудника фирмы).
    /// </summary>
    public interface IAgent
    {
        /// <summary>
        /// Id агента (риэлтора).
        /// </summary>
        int IdAgent { get; set; }

        /// <summary>
        /// Имя.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Фамилия.
        /// </summary>
        string LastName { get; set; }

        /// <summary>
        /// Адрес.
        /// </summary>
        string Addres { get; set; }

        /// <summary>
        /// Телефон.
        /// </summary>
        string PhoneNumber { get; set; }

        /// <summary>
        /// Id фирмы, в которой работает агент.
        /// </summary>
        int IdFirm { get; set; }

        /// <summary>
        /// Агент (риэлтор, сотрудник фирмы) является директором фирмы.
        /// </summary>
        /// <remarks>Директором, или администратором, т.е. лицом, у которого
        /// есть права действовать от имени любого агента фирмы, а также 
        /// добавлять, удалять агентов, и корректировать информацию о фирме.</remarks>
        bool IsFirmAdmin { get; set; }
    }
}
