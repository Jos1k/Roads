using System.Web.Http;

namespace Roads.Web.Mvc
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "Ajax",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { controller = "Ajax", action = "GetSuggestion", id = RouteParameter.Optional }
            );
        }
    }
}
