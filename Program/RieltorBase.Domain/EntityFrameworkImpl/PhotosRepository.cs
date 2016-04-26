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
            Photo existingPhoto = this.Context.Photos.First(
                ph => ph.PhotoId == changedEntity.PhotoId);

            new PhotoWrap(changedEntity)
                .UpdateRealObject(existingPhoto);

            return changedEntity;
        }

        public override void Delete(int id)
        {
            this.Context.Photos.Remove(
                this.Context.Photos.First(ph => ph.PhotoId == id));
        }
    }
}
