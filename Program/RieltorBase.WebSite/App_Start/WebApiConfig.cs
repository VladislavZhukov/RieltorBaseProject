namespace RieltorBase.WebSite
{
    using System.Web.Http;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // общий фильтр исключений
            config.Filters.Add(new CommonExceptionFilter());

			config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/v1/{controller}/{action}/{id}",
                defaults: new
                {
                    action = RouteParameter.Optional,
                    id = RouteParameter.Optional
                }
            );
        }
    }
}
