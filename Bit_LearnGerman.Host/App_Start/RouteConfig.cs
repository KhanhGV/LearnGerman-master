using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Oauth_2._0_v2
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "FileGet",
                url: "getfile",
                defaults: new { controller = "FileFillter", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Template",
                url: "Template",
                defaults: new { controller = "FileFillter", action = "GetTemplate", id = UrlParameter.Optional, en = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
