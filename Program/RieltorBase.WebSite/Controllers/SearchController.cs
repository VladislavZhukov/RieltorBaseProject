namespace RieltorBase.WebSite.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using RieltorBase.Domain;
    using RieltorBase.Domain.InfoClasses;

    public class SearchController : Controller
    {
        //
        // GET: /Search/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FindResults(
            RealtyObjectSearchOptions options)
        {
            IEnumerable<RealtyObjectInfo> result
                = SharedOperations.GetRealtyObjects(options);

            return View(result);
        }
	}
}