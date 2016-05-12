namespace RieltorBase.WebSite.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Security.Authentication;
    using System.Web.Http;

    using RieltorBase.Domain.Interfaces;

    using RieltorBase.WebSite.JsonCompatibleClasses;

    /// <summary>
    /// Контроллер фотографий.
    /// </summary>
    public class PhotosController : RealtyBaseCommonController
    {
        /// <summary>
        /// Репозиторий фотографий.
        /// </summary>
        private readonly IRepository<IPhoto> photos =
            RBDependencyResolver.Current.CreateInstance<IRepository<IPhoto>>();

        /// <summary>
        /// Репозиторий фирм.
        /// </summary>
        private readonly IRepository<IFirm> firms =
            RBDependencyResolver.Current.CreateInstance<IRepository<IFirm>>();

        /// <summary>
        /// Репозиторий объектов недвижимости.
        /// </summary>
        private readonly IRepository<IRealtyObject> realtyObjects =
            RBDependencyResolver.Current.CreateInstance<IRepository<IRealtyObject>>();

        /// <summary>
        /// Сообщение об отсутствии прав на чтение.
        /// </summary>
        private const string DontHaveRightToRead
            = "Пользователю не разрешено просматривать фотографии.";

        /// <summary>
        /// Получить все фотографии.
        /// </summary>
        /// <returns>Все фотографии.</returns>
        /// <remarks>Пример запроса: GET api/v1/photos</remarks>
        public IEnumerable<IPhoto> Get()
        {
            this.AuthorizeUserToReadData(PhotosController.DontHaveRightToRead);
            return this.photos.GetAll();
        }

        /// <summary>
        /// Получить фотографию по id.
        /// </summary>
        /// <param name="id">Id фотографии.</param>
        /// <returns>Фотография с данным id.</returns>
        /// <remarks>Пример запроса: GET api/v1/photos/5</remarks>
        public IPhoto Get(int id)
        {
            this.AuthorizeUserToReadData(PhotosController.DontHaveRightToRead);
            return this.photos.Find(id);
        }

        /// <summary>
        /// Добавить фотографию.
        /// </summary>
        /// <param name="photo">Новая фотография.</param>
        /// <returns>Добавленная фотография.</returns>
        /// <remarks>Пример запроса: POST api/v1/photos 
        /// (в теле - JSON-объект фотографии).</remarks>
        public IPhoto Post([FromBody]JsonPhoto photo)
        {
            this.AuthorizeForPhotoOperations(photo);
            IPhoto addedPhoto = this.photos.Add(photo);
            this.photos.SaveChanges();
            return addedPhoto;
        }

        /// <summary>
        /// Изменить фотографию.
        /// </summary>
        /// <param name="id">Id фотографии.</param>
        /// <param name="photo">Изменяемая фотография.</param>
        /// <returns>Измененная фотография.</returns>
        /// <remarks>Пример запроса: PUT api/v1/photos/5 
        /// (в теле - JSON-объект фотографии).</remarks>
        public IPhoto Put(int id, [FromBody]JsonPhoto photo)
        {
            this.AuthorizeForPhotoOperations(photo);
            IPhoto updatedPhoto = this.photos.Update(photo);
            this.photos.SaveChanges();
            return updatedPhoto;
        }

        /// <summary>
        /// Удалить фотографию.
        /// </summary>
        /// <param name="id">Id удаляемой фотографии.</param>
        /// <remarks>Пример запроса: DELETE api/v1/photos/5.</remarks>
        public void Delete(int id)
        {
            this.AuthorizeForPhotoOperations(this.photos.Find(id));
            this.photos.Delete(id);
            this.photos.SaveChanges();
        }

        /// <summary>
        /// Авторизовать пользователя для изменения (добавления,
        /// удаления) фотографии.
        /// </summary>
        /// <param name="photo">Фотография.</param>
        private void AuthorizeForPhotoOperations(IPhoto photo)
        {
            if (photo.FirmId != 0 && photo.RealtyObjectId != 0)
            {
                throw new InvalidOperationException(
                    "Фотография не может иметь id фирмы и id объекта недвижимости одновременно.");
            }

            if (photo.FirmId != 0)
            {
                if (!this.AuthorizationMechanism.UserHasAccessToPhotos(
                    this.CurrentUserInfo, this.firms.Find(photo.FirmId)))
                {
                    throw new AuthenticationException(
                        "Данный пользователь не может редактировать фотографии данной фирмы.");
                }
            }
            else if (photo.RealtyObjectId != 0)
            {
                if (!this.AuthorizationMechanism.UserHasAccessToPhotos(
                    this.CurrentUserInfo, this.realtyObjects.Find(photo.RealtyObjectId)))
                {
                    throw new AuthenticationException(
                        "Данный пользователь не может редактировать фотографии данного объекта недвижимости.");
                }
            }
            else
            {
                throw new InvalidOperationException(
                    "Фотография должна иметь либо Id фирмы, либо Id объекта недвижимости.");
            }
        }
    }
}
