namespace RieltorBase.Domain.EntityFrameworkImpl
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using RieltorBase.Domain.Interfaces;

    /// <summary>
    /// Реализация интерфейса <see cref="IAgentRepository"/> для работы
    /// с контекстом Entity Framework.
    /// </summary>
    public class AgentRepository : EFRepository<IAgent>, IAgentRepository
    {
        /// <summary>
        /// Получить всех агентов недвижимости.
        /// </summary>
        /// <returns>Все имеющиеся агенты (риэлторы).</returns>
        public override IEnumerable<IAgent> GetAll()
        {
            return Context.Agents.ToList().Select(
                agent => new AgentWrap(agent, this.Context));
        }

        /// <summary>
        /// Найти агента недвижимости с указанным id.
        /// </summary>
        /// <param name="id">Id агента недвижимости.</param>
        /// <returns>Найденный агент.</returns>
        public override IAgent Find(int id)
        {
            Agent obj = Context.Agents
                .FirstOrDefault(fod => fod.Id_agent == id);

            return obj != null
                ? new AgentWrap(obj, this.Context)
                : null;
        }

        /// <summary>
        /// Добавить агента недвижимости.
        /// </summary>
        /// <param name="newEntity">Новый агент недвижимости.</param>
        /// <returns>Добавленный агент недвижимости.</returns>
        public override IAgent Add(IAgent newEntity)
        {
            AgentWrap wrap =
                new AgentWrap(newEntity, this.Context);

            Context.Agents.Add(wrap.GetRealObject());
            return wrap;
        }

        /// <summary>
        /// Обновить данные агента.
        /// </summary>
        /// <param name="changedEntity">Риэлтор, 
        /// данные которого обновлены.</param>
        /// <returns>Обновленный агент.</returns>
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

        /// <summary>
        /// Удалить конкретного агента недвижимости.
        /// </summary>
        /// <param name="id">Id удаляемого риэлтора.</param>
        public override void Delete(int id)
        {
            Context.Agents.Remove(
                this.Context.Agents.First(f =>
                    f.Id_agent == id));
        }

        /// <summary>
        /// Найти риэлтора по имени.
        /// </summary>
        /// <param name="partOfName">Часть имени риэлтора.</param>
        /// <returns>Найденные агенты.</returns>
        public IEnumerable<IAgent> FindByName(string partOfName)
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// Найти всех агентов определенной фирмы.
        /// </summary>
        /// <param name="firmId">Id фирмы.</param>
        /// <returns>Все агенты, работающие в этой фирме.</returns>
        public IEnumerable<IAgent> FindByFirmId(int firmId)
        {
            return Context.Agents.Where(ag => ag.Id_firm == firmId)
                .ToList().Select(agent => new AgentWrap(agent, Context));
        }
    }
}
