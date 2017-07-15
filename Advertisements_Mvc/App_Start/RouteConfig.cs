using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Advertisements_Mvc
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "LocalizedDefault",
                url: "{lang}/{controller}/{action}",
                defaults: new { controller = "Home", action = "Index" },
                constraints: new { lang = "ua-UKR|en-US" }
                );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional,lang = "ua-UKR" }
            );
            
        }
    }
}
