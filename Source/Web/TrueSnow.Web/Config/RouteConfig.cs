﻿namespace TrueSnow.Web.Config
{
    using System.Web.Mvc;
    using System.Web.Routing;

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Home",
                url: "Home",
                defaults: new { controller = "Home", action = "Index" });

            routes.MapRoute(
                name: "Profile",
                url: "Profile/{id}",
                defaults: new { controller = "Profile", action = "Index" });

            routes.MapRoute(
                name: "Register",
                url: "Register",
                defaults: new { controller = "Account", action = "Register" });

            routes.MapRoute(
                name: "Post",
                url: "Post/{id}",
                defaults: new { controller = "Posts", action = "ById" });

            routes.MapRoute(
                name: "Articles",
                url: "Articles/{action}/{id}",
                defaults: new { controller = "Articles", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute(
                name: "File",
                url: "File/{id}",
                defaults: new { controller = "File", action = "Index" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional });
        }
    }
}
