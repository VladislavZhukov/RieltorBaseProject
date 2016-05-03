using System;

namespace RieltorBase.Domain.EntityFrameworkImpl
{
    using RieltorBase.Domain.Interfaces;

    /// <summary>
    /// Обертка класса <see cref="Photo"/>, реализующая 
    /// интерфейс <see cref="IPhoto"/>.
    /// </summary>
    public class PhotoWrap : IWrapBase<Photo>, IPhoto
    {
        /// <summary>
        /// EF-объект фотографии.
        /// </summary>
        private Photo photo;

        /// <summary>
        /// Создать обертку фотографии на основе реальной EF-фотографии.
        /// </summary>
        /// <param name="photo">EF-объект фотографии.</param>
        public PhotoWrap(Photo photo)
        {
            if (photo == null)
            {
                throw new ArgumentNullException("photo");
            }

            this.photo = photo;
        }

        /// <summary>
        /// Создать обертку EF-фотографии на основе любого объекта фотографии.
        /// </summary>
        /// <param name="iPhoto">Интерфейс фотографии.</param>
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

        /// <summary>
        /// Id фотографии.
        /// </summary>
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

        /// <summary>
        /// Id объекта недвижимости (если это фотография объекта недвижимости).
        /// </summary>
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

        /// <summary>
        /// Id фирмы (если это фотография фирмы).
        /// </summary>
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

        /// <summary>
        /// Относительная часть пути к фотографии (например, часть url).
        /// </summary>
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

        /// <summary>
        /// Получить EF-объект фотографии.
        /// </summary>
        /// <returns>EF-объект фотографии.</returns>
        public Photo GetRealObject()
        {
            return this.photo;
        }
    }
}
