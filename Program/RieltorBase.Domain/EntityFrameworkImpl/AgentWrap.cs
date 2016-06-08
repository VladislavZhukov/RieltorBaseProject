namespace RieltorBase.Domain.EntityFrameworkImpl
{
    using RieltorBase.Domain.Interfaces;

    /// <summary>
    /// Обертка класса EF <see cref="Agent"/>, реализующая 
    /// интерфейс <see cref="IAgent"/>.
    /// </summary>
    public class AgentWrap : IWrapBase<Agent>, IAgent
    {
        /// <summary>
        /// Реальный объект EF.
        /// </summary>
        private readonly Agent agentEF;

        /// <summary>
        /// Создать экземпляр класса на основе объекта EF.
        /// </summary>
        /// <param name="agent">Объект EF.</param>
        internal AgentWrap(Agent agent)
        {
            this.agentEF = agent;
        }

        /// <summary>
        /// Создать экземпляр класса на основе интерфейса агента.
        /// </summary>
        /// <param name="iAgent">Интерфейс агента.</param>
        internal AgentWrap(IAgent iAgent)
        {
            this.agentEF = new Agent
            {
                Id_agent = iAgent.IdAgent,
                Name = iAgent.Name,
                LastName = iAgent.LastName,
                Addres = iAgent.Addres,
                PhoneNumber = iAgent.PhoneNumber,
                Id_firm = iAgent.IdFirm,
                IsFirmAdmin = iAgent.IsFirmAdmin
            };
        }

        /// <summary>
        /// Id агента.
        /// </summary>
        public int IdAgent
        {
            get
            {
                return this.agentEF.Id_agent;
            }

            set
            {
                this.agentEF.Id_agent = value;
            }
        }

        /// <summary>
        /// Имя агента.
        /// </summary>
        public string Name
        {
            get
            {
                return this.agentEF.Name;
            }

            set
            {
                this.agentEF.Name = value;
            }
        }

        /// <summary>
        /// Фамилия агента.
        /// </summary>
        public string LastName
        {
            get
            {
                return this.agentEF.LastName;
            }
            set
            {
                this.agentEF.LastName = value;
            }
        }

        /// <summary>
        /// Адрес агента.
        /// </summary>
        public string Addres
        {
            get
            {
                return this.agentEF.Addres;
            }

            set
            {
                this.agentEF.Addres = value;
            }
        }

        /// <summary>
        /// Номер телефона агента.
        /// </summary>
        public string PhoneNumber
        {
            get
            {
                return this.agentEF.PhoneNumber;
            }

            set
            {
                this.agentEF.PhoneNumber = value;
            }
        }

        /// <summary>
        /// Id фирмы, в которой работает агент.
        /// </summary>
        public int IdFirm
        {
            get
            {
                return this.agentEF.Id_firm;
            }

            set
            {
                this.agentEF.Id_firm = value;
            }
        }

        /// <summary>
        /// Агент является директором/администратором/управляющим фирмы.
        /// </summary>
        public bool IsFirmAdmin
        {
            get
            {
                return this.agentEF.IsFirmAdmin;
            }
            set
            {
                this.agentEF.IsFirmAdmin = value;
            }
        }

        /// <summary>
        /// Получить объект EF.
        /// </summary>
        /// <returns>Объект EF.</returns>
        public Agent GetRealObject()
        {
            return agentEF;
        }
    }
}
