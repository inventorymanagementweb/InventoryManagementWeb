using System.Web.Mvc;
using System.Web.Routing;

namespace InventoryManagement.WebApplication
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Dashboards", action = "Dashboard_1", id = UrlParameter.Optional }
            );
        }
    }
}
