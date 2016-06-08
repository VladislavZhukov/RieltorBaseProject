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

            WebApiConfig.MapAdditionalRoutes(config);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/v1/{controller}/{id}",
                defaults: new
                {
                    id = RouteParameter.Optional
                }
            );
        }

        /// <summary>
        /// Настроить специальные маршруты.
        /// </summary>
        /// <param name="config">Конфигурация HTTP.</param>
        private static void MapAdditionalRoutes(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "FindRealtyObjectsByFirm",
                routeTemplate: "api/v1/realtyobjects/GetByFirm/{firmId}",
                defaults: new
                {
                    controller = "realtyobjects",
                    firmId = RouteParameter.Optional
                }
            );

            config.Routes.MapHttpRoute(
                name: "FindRealtyObjectsByAgent",
                routeTemplate: "api/v1/realtyobjects/GetByAgent/{agentId}",
                defaults: new
                {
                    controller = "realtyobjects",
                    agentId = RouteParameter.Optional
                }
            );

            config.Routes.MapHttpRoute(
                name: "FindFirmPhotos",
                routeTemplate: "api/v1/photos/GetFirmPhotos/{firmId}",
                defaults: new
                {
                    controller = "photos",
                    firmId = RouteParameter.Optional
                }
            );

            config.Routes.MapHttpRoute(
                name: "FindRealtyObjectPhotos",
                routeTemplate: "api/v1/photos/GetRealtyObjectPhotos/{realtyObjectId}",
                defaults: new
                {
                    controller = "photos",
                    realtyObjectId = RouteParameter.Optional
                }
            );
        }
    }
}
