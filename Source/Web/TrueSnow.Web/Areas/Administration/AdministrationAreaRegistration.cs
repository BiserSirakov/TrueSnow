namespace TrueSnow.Web.Areas.Administration
{
    using System.Web.Mvc;

    public class AdministrationAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Administration";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Administration_articles",
                "Administration/Articles",
                new { controller = "AdminArticles", action = "Index" });

            context.MapRoute(
                "Administration_comments",
                "Administration/Comments",
                new { controller = "AdminComments", action = "Index" });

            context.MapRoute(
                "Administration_events",
                "Administration/Events",
                new { controller = "AdminEvents", action = "Index" });

            context.MapRoute(
                "Administration_posts",
                "Administration/Posts",
                new { controller = "AdminPosts", action = "Index" });

            context.MapRoute(
                "Administration_users",
                "Administration/Users",
                new { controller = "AdminUsers", action = "Index" });

            context.MapRoute(
                "Administration_default",
                "Administration/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "TrueSnow.Web.Areas.Administration.Controllers" });
        }
    }
}