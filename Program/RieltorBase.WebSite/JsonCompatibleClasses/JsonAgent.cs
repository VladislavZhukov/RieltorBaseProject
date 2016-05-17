namespace RieltorBase.WebSite.JsonCompatibleClasses
{
    using RieltorBase.Domain.Interfaces;

    /// <summary>
    /// Реализация <see cref="IAgent"/>, поддерживающая
    /// сериализацию JSON.
    /// </summary>
    public class JsonAgent : IAgent
    {
        /// <summary>
        /// Id агента (риэлтора).
        /// </summary>
        public int Id_agent { get; set; }

        /// <summary>
        /// Имя.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Фамилия.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Адрес.
        /// </summary>
        public string Addres { get; set; }

        /// <summary>
        /// Телефон.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Id фирмы, в которой работает агент.
        /// </summary>
        public int Id_firm { get; set; }

        /// <summary>
        /// Агент (риэлтор, сотрудник фирмы) является директором фирмы.
        /// </summary>
        /// <remarks>Директором, или администратором, т.е. лицом, у которого
        /// есть права действовать от имени любого агента фирмы, а также 
        /// добавлять, удалять агентов, и корректировать информацию о фирме.</remarks>
        public bool IsFirmAdmin { get; set; }
    }
}