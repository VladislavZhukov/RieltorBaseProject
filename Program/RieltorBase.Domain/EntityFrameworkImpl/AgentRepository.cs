namespace RieltorBase.Domain.EntityFrameworkImpl
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
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
            if (!this.Context.Agents.Any(f =>
                f.Id_agent == changedEntity.Id_agent))
            {
                throw new InvalidOperationException(
                    "Попытка обновления данных несуществующего агента"
                    + " (id = " + changedEntity.Id_agent + "). ");
            }

            AgentWrap wrap = new AgentWrap(
                changedEntity,
                this.Context);

            Agent agent = wrap.GetRealObject();

            this.Context.Agents.Attach(agent);
            this.Context.Entry(agent).State = EntityState.Modified;
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
