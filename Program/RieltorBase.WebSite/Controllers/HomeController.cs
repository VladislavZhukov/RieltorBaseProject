using System.Web.Mvc;
using RieltorBase.Domain;

namespace RieltorBase.WebSite.Controllers
{
    public class HomeController : Controller
    {
        private VolgaInfoDBEntities context;

        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }

        public VolgaInfoDBEntities Context
        {
            get
            {
                return this.context 
                    ?? (this.context = new VolgaInfoDBEntities());
            }
        }

        public ActionResult CreateMetadata()
        {
            this.Context.CreateStandardMetadata();
            this.Context.SaveChanges();

            return this.RedirectToAction("Index");
        }

        public ActionResult ClearDatabase()
        {
            this.Context.ClearDatabase();
            this.Context.SaveChanges();

            return this.RedirectToAction("Index");
        }

        public ActionResult CreateTestObjects()
        {
            this.Context.CreateFewObjects();
            this.Context.SaveChanges();

            return this.RedirectToAction("Index");
        }
    }
}