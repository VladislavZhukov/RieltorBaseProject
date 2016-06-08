namespace RieltorBase.Domain.EntityFrameworkImpl
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using RieltorBase.Domain.Interfaces;

    /// <summary>
    /// EF-реализация интерфейса хранилища фирм.
    /// </summary>
    public class FirmsRepository : EFRepository<IFirm>, IFirmsRepository
    {
        /// <summary>
        /// Получить все фирмы.
        /// </summary>
        /// <returns>Все имеющиеся фирмы.</returns>
        public override IEnumerable<IFirm> GetAll()
        {
            return this.Context.Firms.ToList().Select(
                firm => new FirmWrap(firm));
        }

        /// <summary>
        /// Найти конкретную фирму.
        /// </summary>
        /// <param name="id">Id фирмы.</param>
        /// <returns>Найденная фирма.</returns>
        public override IFirm Find(int id)
        {
            Firm firm = this.Context.Firms.Find(id);
            return firm != null ? new FirmWrap(firm) : null;
        }

        /// <summary>
        /// Найти фирмы по имени.
        /// </summary>
        /// <param name="partOfName">Часть имени фирмы.</param>
        /// <returns>Найденные фирмы.</returns>
        public IEnumerable<IFirm> FindByName(string partOfName)
        {
            IQueryable<Firm> firms = this.Context.Firms.Where(
                f => f.Name.Contains(partOfName));
            return firms.ToList().Select(f => new FirmWrap(f));
        }

        /// <summary>
        /// Добавить новую фирму.
        /// </summary>
        /// <param name="newEntity">Новая фирма.</param>
        /// <returns>Добавленная фирма.</returns>
        public override IFirm Add(IFirm newEntity)
        {
            FirmWrap wrap = new FirmWrap(newEntity);
            this.Context.Firms.Add(wrap.GetRealObject());
            return wrap;
        }

        /// <summary>
        /// Обновить данные существующей фирмы.
        /// </summary>
        /// <param name="changedEntity">Фирма с обновленными данными.</param>
        /// <returns>Обновленная фирма.</returns>
        public override IFirm Update(IFirm changedEntity)
        {
            if (!this.Context.Firms.Any(firm =>
                firm.FirmId == changedEntity.FirmId))
            {
                throw new InvalidOperationException(
                    "Попытка обновления несуществующего объекта. "
                    + "Фирмы с id=" + changedEntity.FirmId + " не существует.");
            }

            FirmWrap wrap = new FirmWrap(changedEntity);

            Firm updatedFirm = wrap.GetRealObject();

            Firm existingFirm = this.Context.Firms.First(f =>
                f.FirmId == updatedFirm.FirmId);

            existingFirm.Name = updatedFirm.Name;
            existingFirm.Address = updatedFirm.Address;
            existingFirm.Phone = updatedFirm.Phone;

            return wrap;
        }

        /// <summary>
        /// Удалить конкретную фирму.
        /// </summary>
        /// <param name="id">Id удаляемой фирмы.</param>
        public override void Delete(int id)
        {
            this.Context.Firms.Remove(
                this.Context.Firms.First(firm => firm.FirmId == id));
        }
    }
}
