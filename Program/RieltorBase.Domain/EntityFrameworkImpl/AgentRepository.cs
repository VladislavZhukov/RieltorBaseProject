namespace RieltorBase.Domain.EntityFrameworkImpl
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using RieltorBase.Domain.Interfaces;

    public class AgentRepository : EFRepository<IAgent>, IAgentRepository
    {
        public override IEnumerable<IAgent> GetAll()
        {
            return Context.Agents.ToList().Select(
                agent => new AgentWrap(agent, this.Context));
        }

        public override IAgent Find(int id)
        {
            Agent obj = Context.Agents
                .FirstOrDefault(fod => fod.Id_agent == id);

            return obj != null
                ? new AgentWrap(obj, this.Context)
                : null;
        }

        public override IAgent Add(IAgent newEntity)
        {
            AgentWrap wrap =
                new AgentWrap(newEntity, this.Context);

            Context.Agents.Add(wrap.GetRealObject());
            return wrap;
        }

        public override IAgent Update(IAgent changedEntity)
        {
            Agent updatedObj = Context.Agents
                .First(f => f.Id_agent == changedEntity.Id_agent);

            AgentWrap wrap = new AgentWrap(
                changedEntity,
                this.Context);

            wrap.UpdateRealObject(updatedObj);

            return wrap;
        }

        public override void Delete(int id)
        {
            Context.Agents.Remove(
                this.Context.Agents.First(f =>
                    f.Id_agent == id));
        }

        public IEnumerable<IAgent> FindByName(string partOfName)
        {
            throw new NotImplementedException();
        }
        
        public IEnumerable<IAgent> FindByFirmId(int firmId)
        {
            return Context.Agents.Where(ag => ag.Id_firm == firmId)
                .ToList().Select(agent => new AgentWrap(agent, Context));
        }
    }
}
