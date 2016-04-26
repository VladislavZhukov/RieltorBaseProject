namespace RieltorBase.WebSite.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;

    using RieltorBase.Domain.Interfaces;

    public class PhotosController : ApiController
    {
        private IRepository<IPhoto> photos =
            RBDependencyResolver.Current.Resolve<IRepository<IPhoto>>();

        // GET api/v1/photos
        public IEnumerable<IPhoto> Get()
        {
            return this.photos.GetAll();
        }

        // GET api/v1/photos/5
        public IPhoto Get(int id)
        {
            return this.photos.Find(id);
        }

        // POST api/v1/photos
        public IPhoto Post([FromBody]IPhoto value)
        {
            IPhoto addedPhoto = this.photos.Add(value);
            this.photos.SaveChanges();
            return addedPhoto;
        }

        // PUT api/v1/photos/5
        public IPhoto Put(int id, [FromBody]IPhoto value)
        {
            IPhoto updatedPhoto = this.photos.Update(value);
            this.photos.SaveChanges();
            return updatedPhoto;
        }

        // DELETE api/v1/photos/5
        public void Delete(int id)
        {
            this.photos.Delete(id);
            this.photos.SaveChanges();
        }
    }
}
