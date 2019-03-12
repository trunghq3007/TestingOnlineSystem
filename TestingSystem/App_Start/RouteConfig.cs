using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TestingSystem
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");



            routes.MapRoute(
                name: "Default1",
                url: "Login/{action}/{id}",
                defaults: new { controller = "_Login", action = "Login", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Default2",
                url: "Register/{action}/{id}",
                defaults: new { controller = "_Register", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Default3",
                url: "Account/{action}/{id}",
                defaults: new { controller = "_Account", action = "action", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

           
        }
    }
}
