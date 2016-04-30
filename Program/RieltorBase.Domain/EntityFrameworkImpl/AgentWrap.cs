namespace RieltorBase.Domain.EntityFrameworkImpl
{
    using System;
    using RieltorBase.Domain.Interfaces;

    public class AgentWrap : IWrapBase<Agent, IAgent>, IAgent
    {
        private readonly Agent _agentEF;
        private readonly VolgaInfoDBEntities _context;

        private AgentWrap(VolgaInfoDBEntities context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            this._context = context;
        }

        internal AgentWrap(Agent agent, VolgaInfoDBEntities context) : this(context)
        {
            this._agentEF = agent;
        }

        internal AgentWrap(IAgent iAgent, VolgaInfoDBEntities context) : this(context)
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

        public Agent GetRealObject()
        {
            return _agentEF;
        }

        public void UpdateAgent(Agent realObject)
        {
            realObject.Name = Name;
            realObject.LastName = LastName;
            realObject.Addres = Addres;
            realObject.PhoneNumber = PhoneNumber;
            realObject.Id_firm = Id_firm;
            realObject.IsFirmAdmin = IsFirmAdmin;
        }
    }
}
