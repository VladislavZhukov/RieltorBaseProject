namespace RieltorBase.Domain.EntityFrameworkImpl
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using RieltorBase.Domain.Interfaces;

    /// <summary>
    /// EF-реализация хранилища фотографий.
    /// </summary>
    public class PhotosRepository : EFRepository<IPhoto>
    {
        /// <summary>
        /// Получить все фотографии.
        /// </summary>
        /// <returns>Все фотографии.</returns>
        public override IEnumerable<IPhoto> GetAll()
        {
            return this.Context.Photos.ToList().Select(
                ph => new PhotoWrap(ph));
        }

        /// <summary>
        /// Найти конкретную фотографию.
        /// </summary>
        /// <param name="id">Id фотографии.</param>
        /// <returns>Найденная фотография.</returns>
        public override IPhoto Find(int id)
        {
            Photo photo = this.Context.Photos.Find(id);
            return photo != null ? new PhotoWrap(photo) : null;
        }

        /// <summary>
        /// Добавить новую фотографию.
        /// </summary>
        /// <param name="newEntity">Новая фотография.</param>
        /// <returns>Добавленная фотография.</returns>
        public override IPhoto Add(IPhoto newEntity)
        {
            PhotoWrap wrap = new PhotoWrap(newEntity);
            this.Context.Photos.Add(wrap.GetRealObject());
            return wrap;
        }

        /// <summary>
        /// Обновить данные существующей фотографии.
        /// </summary>
        /// <param name="changedEntity">Фотография с обновленными
        /// данными.</param>
        /// <returns>Обновленная фотография.</returns>
        public override IPhoto Update(IPhoto changedEntity)
        {
            if (!this.Context.Photos.Any(ph =>
                ph.PhotoId == changedEntity.PhotoId))
            {
                throw new InvalidOperationException(
                    "Попытка обновления несуществующего объекта. "
                    + "Фотографии с id=" + changedEntity.PhotoId + " нет в БД.");
            }

            Photo photo = new PhotoWrap(changedEntity).GetRealObject();

            this.Context.Photos.Attach(photo);
            this.Context.Entry(photo).State = EntityState.Modified;
            return changedEntity;
        }

        /// <summary>
        /// Удалить фотографию.
        /// </summary>
        /// <param name="id">Id удаляемой фотографии.</param>
        public override void Delete(int id)
        {
            this.Context.Photos.Remove(
                this.Context.Photos.First(ph => ph.PhotoId == id));
        }
    }
}
