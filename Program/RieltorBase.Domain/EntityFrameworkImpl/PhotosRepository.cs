using System;
using System.Data.Entity;

namespace RieltorBase.Domain.EntityFrameworkImpl
{
    using System.Collections.Generic;
    using System.Linq;

    using RieltorBase.Domain.Interfaces;

    public class PhotosRepository : EFRepository<IPhoto>
    {
        public override IEnumerable<IPhoto> GetAll()
        {
            return this.Context.Photos.ToList().Select(
                ph => new PhotoWrap(ph));
        }

        public override IPhoto Find(int id)
        {
            Photo photo = this.Context.Photos.Find(id);
            return photo != null ? new PhotoWrap(photo) : null;
        }

        public override IPhoto Add(IPhoto newEntity)
        {
            PhotoWrap wrap = new PhotoWrap(newEntity);
            this.Context.Photos.Add(wrap.GetRealObject());
            return wrap;
        }

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

        public override void Delete(int id)
        {
            this.Context.Photos.Remove(
                this.Context.Photos.First(ph => ph.PhotoId == id));
        }
    }
}
