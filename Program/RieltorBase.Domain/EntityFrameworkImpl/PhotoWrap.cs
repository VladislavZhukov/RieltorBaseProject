namespace RieltorBase.Domain.EntityFrameworkImpl
{
    using RieltorBase.Domain.Interfaces;

    public class PhotoWrap : IWrapBase<Photo, IPhoto>, IPhoto
    {
        private Photo photo;

        public PhotoWrap()
        {
            this.photo = new Photo();
        }

        public PhotoWrap(Photo photo)
        {
            this.photo = photo;
        }

        public PhotoWrap(IPhoto iPhoto)
        {
            this.photo = new Photo()
            {
                PhotoId = iPhoto.PhotoId,
                FirmId = iPhoto.FirmId,
                RealtyObjectId = iPhoto.RealtyObjectId,
                RelativeSource = iPhoto.RelativeSource
            };
        }

        public int PhotoId
        {
            get
            {
                return this.photo.PhotoId;
            }
            set
            {
                this.photo.PhotoId = value;
            }
        }

        public int RealtyObjectId
        {
            get
            {
                return this.photo.RealtyObjectId;
            }
            set
            {
                this.photo.RealtyObjectId = value;
            }
        }

        public int FirmId
        {
            get
            {
                return this.photo.FirmId;
            }
            set
            {
                this.photo.FirmId = value;
            }
        }

        public string RelativeSource
        {
            get
            {
                return this.photo.RelativeSource;
            }
            set
            {
                this.photo.RelativeSource = value;
            }
        }

        public Photo GetRealObject()
        {
            return this.photo;
        }
    }
}
