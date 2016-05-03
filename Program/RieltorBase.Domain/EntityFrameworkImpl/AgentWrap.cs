namespace RieltorBase.Domain.EntityFrameworkImpl
{
    using System;

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
        private readonly Agent _agentEF;

        /// <summary>
        /// Контекст базы данных (EF).
        /// </summary>
        private readonly VolgaInfoDBEntities _context;

        /// <summary>
        /// Общая часть конструктора.
        /// </summary>
        /// <param name="context">Контекст базы данных (EF).</param>
        private AgentWrap(VolgaInfoDBEntities context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            this._context = context;
        }

        /// <summary>
        /// Создать экземпляр класса на основе объекта EF.
        /// </summary>
        /// <param name="agent">Объект EF.</param>
        /// <param name="context">Контекст базы данных EF.</param>
        internal AgentWrap(Agent agent, VolgaInfoDBEntities context) 
            : this(context)
        {
            this._agentEF = agent;
        }

        /// <summary>
        /// Создать экземпляр класса на основе интерфейса агента.
        /// </summary>
        /// <param name="iAgent">Интерфейс агента.</param>
        /// <param name="context">Контекст базы данных EF.</param>
        internal AgentWrap(IAgent iAgent, VolgaInfoDBEntities context) 
            : this(context)
        {
            this._agentEF = new Agent();
            this._agentEF.Id_agent = iAgent.Id_agent;
            this._agentEF.Name = iAgent.Name;
            this._agentEF.LastName = iAgent.LastName;
            this._agentEF.Addres = iAgent.Addres;
            this._agentEF.PhoneNumber = iAgent.PhoneNumber;
            this._agentEF.Id_firm = iAgent.Id_firm;
            this._agentEF.IsFirmAdmin = iAgent.IsFirmAdmin;
        }

        /// <summary>
        /// Id агента.
        /// </summary>
        public int Id_agent
        {
            get
            {
                return this._agentEF.Id_agent;
            }
            set
            {
                this.Id_agent = value;
            }
        }

        /// <summary>
        /// Имя агента.
        /// </summary>
        public string Name
        {
            get
            {
                return this._agentEF.Name;
            }
            set
            {
                this.Name = value;
            }
        }

        /// <summary>
        /// Фамилия агента.
        /// </summary>
        public string LastName
        {
            get
            {
                return this._agentEF.LastName;
            }
            set
            {
                this.LastName = value;
            }
        }

        /// <summary>
        /// Адрес агента.
        /// </summary>
        public string Addres
        {
            get
            {
                return this._agentEF.Addres;
            }
            set
            {
                this.Addres = value;
            }
        }

        /// <summary>
        /// Номер телефона агента.
        /// </summary>
        public string PhoneNumber
        {
            get
            {
                return this._agentEF.PhoneNumber;
            }
            set
            {
                this.PhoneNumber = value;
            }
        }

        /// <summary>
        /// Id фирмы, в которой работает агент.
        /// </summary>
        public int Id_firm
        {
            get
            {
                return this._agentEF.Id_firm;
            }
            set
            {
                this.Id_firm = value;
            }
        }

        /// <summary>
        /// Агент является директором/администратором/управляющим фирмы.
        /// </summary>
        public bool IsFirmAdmin
        {
            get
            {
                return this._agentEF.IsFirmAdmin;
            }
            set
            {
                this.IsFirmAdmin = value;
            }
        }

        /// <summary>
        /// Получить объект EF.
        /// </summary>
        /// <returns>Объект EF.</returns>
        public Agent GetRealObject()
        {
            return _agentEF;
        }
    }
}
