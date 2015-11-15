using System.Web.Mvc;
using System.Web.Routing;

namespace Roads.Web.Mvc
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Localization", 
                url: "{culture}/{controller}/{action}/{id}", 
                defaults:
                new
                {                
                    controller = "Roads",
                    action = "FindRoad",
                    id = UrlParameter.Optional
                },
                constraints: new {culture = "[a-z]{2}"}
                );

            routes.MapRoute(
                name:"SecondRoute", 
                url:"{controller}/{action}/{id}",
                defaults:
               new
               {
                   controller = "Roads",
                   action = "FindRoad",
                   id = UrlParameter.Optional
               }
               );
        }
    }
}