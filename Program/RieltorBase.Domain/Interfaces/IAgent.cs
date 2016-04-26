namespace RieltorBase.Domain.Interfaces
{
    using System.Collections.Generic;

    public interface IAgent
    {
        int Id_agent { get; set; }

        string Name { get; set; }

        string LastName { get; set; }

        string Addres { get; set; }

        string PhoneNumber { get; set; }

        int Id_firm { get; set; }

        bool IsFirmAdmin { get; set; }
    }
}
