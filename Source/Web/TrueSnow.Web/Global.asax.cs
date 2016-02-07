﻿namespace TrueSnow.Web
{
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    using TrueSnow.Web.App_Start;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            DatabaseConfig.Initialize();

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // TODO: automapper config

        }
    }
}