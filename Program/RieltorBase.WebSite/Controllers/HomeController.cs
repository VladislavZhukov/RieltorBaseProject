namespace RieltorBase.WebSite.Controllers
{
    using System.Web.Mvc;

    using RieltorBase.Domain.Interfaces;

    public class HomeController : Controller
    {
        private IRealtyBaseContext context
            = RBDependencyResolver.Current.Resolve<IRealtyBaseContext>();

        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateMetadata()
        {
            this.context.CreateStandardMetadata();
            this.context.SaveChanges();

            return this.RedirectToAction("Index");
        }

        public ActionResult ClearDatabase()
        {
            this.context.ClearDatabase();
            this.context.SaveChanges();

            return this.RedirectToAction("Index");
        }

        public ActionResult CreateTestObjects()
        {
            this.context.CreateFewObjects();
            this.context.SaveChanges();

            return this.RedirectToAction("Index");
        }
    }
}