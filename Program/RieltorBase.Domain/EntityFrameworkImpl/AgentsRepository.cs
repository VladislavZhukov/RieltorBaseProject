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
    // ReSharper disable once ClassNeverInstantiated.Global Инстанцируется контейнеро
    public class AgentsRepository : EFRepository<IAgent>, IAgentRepository
    {
        /// <summary>
        /// Получить всех агентов недвижимости.
        /// </summary>
        /// <returns>Все имеющиеся агенты (риэлторы).</returns>
        public override IEnumerable<IAgent> GetAll()
        {
            return this.Context.Agents.ToList().Select(
                agent => new AgentWrap(agent));
        }

        /// <summary>
        /// Найти агента недвижимости с указанным id.
        /// </summary>
        /// <param name="id">Id агента недвижимости.</param>
        /// <returns>Найденный агент.</returns>
        public override IAgent Find(int id)
        {
            Agent obj = Context.Agents.FirstOrDefault(fod => 
                fod.Id_agent == id);

            return obj != null ? new AgentWrap(obj) : null;
        }

        /// <summary>
        /// Добавить агента недвижимости.
        /// </summary>
        /// <param name="newEntity">Новый агент недвижимости.</param>
        /// <returns>Добавленный агент недвижимости.</returns>
        public override IAgent Add(IAgent newEntity)
        {
            AgentWrap wrap = new AgentWrap(newEntity);

            if (this.Context.Agents.Any(a =>
                a.Name == newEntity.Name
                && a.LastName == newEntity.LastName
                && a.PhoneNumber == newEntity.PhoneNumber))
            {
                throw new InvalidOperationException(
                    "Агент с таким именем, фамилией и телефоном, уже существует.");
            }
            
            this.Context.Agents.Add(wrap.GetRealObject());
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
                f.Id_agent == changedEntity.IdAgent))
            {
                throw new InvalidOperationException(
                    "Попытка обновления данных несуществующего агента"
                    + " (id = " + changedEntity.IdAgent + "). ");
            }

            AgentWrap wrap = new AgentWrap(changedEntity);

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
            this.Context.Agents.Remove(
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
            string[] parts = partOfName.Split(
                new []{' '}, 
                StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 2)
            {
                // могли написать "Сидоров Иван" и "Иван Сидоров"
                var result1 = this.FindByFIO(parts[0], parts[1]);
                var result2 = this.FindByFIO(parts[1], parts[0]);
                return result1.Union(result2, new AgentsComparer());
            }
            else
            {
                // просто любое совпадение (в имени или фамилии)
                return this.Context.Agents.Where(a =>
                    a.LastName.Contains(partOfName) || a.Name.Contains(partOfName))
                    .ToList()
                    .Select(ag => new AgentWrap(ag));
            }
        }
        
        /// <summary>
        /// Найти всех агентов определенной фирмы.
        /// </summary>
        /// <param name="firmId">Id фирмы.</param>
        /// <returns>Все агенты, работающие в этой фирме.</returns>
        public IEnumerable<IAgent> FindByFirmId(int firmId)
        {
            return this.Context.Agents.Where(ag => ag.Id_firm == firmId)
                .ToList().Select(agent => new AgentWrap(agent));
        }

        /// <summary>
        /// Найти агентов по имени и фамилии.
        /// </summary>
        /// <param name="firstNamePart">Часть имени.</param>
        /// <param name="lastNamePart">Часть фамилии.</param>
        /// <returns>Агенты, имя и фамилия которых похожи 
        /// на заданные параметры.</returns>
        private IEnumerable<IAgent> FindByFIO(
            string firstNamePart,
            string lastNamePart)
        {
            return this.Context.Agents.Where(ag =>
                ag.Name.Contains(firstNamePart)
                && ag.LastName.Contains(lastNamePart))
                .ToList()
                .Select(a => new AgentWrap(a));
        }

        /// <summary>
        /// Класс определения эквивалентности агентов.
        /// </summary>
        private class AgentsComparer : IEqualityComparer<IAgent>
        {
            /// <summary>
            /// Агенты эквивалентны.
            /// </summary>
            /// <param name="x">Агент X.</param>
            /// <param name="y">Агент Y.</param>
            /// <returns>Агенты эквивалентны.</returns>
            public bool Equals(IAgent x, IAgent y)
            {
                return x.IdAgent == y.IdAgent;
            }

            /// <summary>
            /// Получить хэш-код.
            /// </summary>
            /// <param name="obj">Агент.</param>
            /// <returns>Хэш-код агента.</returns>
            public int GetHashCode(IAgent obj)
            {
                throw new NotImplementedException();
            }
        }
    }
}
