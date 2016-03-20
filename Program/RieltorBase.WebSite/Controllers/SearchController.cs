namespace RieltorBase.WebSite.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using RieltorBase.Domain;

    public class SearchController : Controller
    {
        //
        // GET: /Search/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FindResults(
            AppartmentSearchOptions options)
        {
            IEnumerable<AppartmentInfo> result
                = SharedOperations.GetAppartments(options);

            return View(result);
        }
	}
}