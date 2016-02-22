namespace TrueSnow.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using Infrastructure.Constants;
    using TrueSnow.Services.Data.Contracts;
    using TrueSnow.Web.Areas.Administration.ViewModels;

    [Authorize(Roles = IdentityRoles.Administrator)]
    public class AdminEventsController : Controller
    {
        private readonly IEventsService events;

        public AdminEventsController(IEventsService events)
        {
            this.events = events;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Events_Read([DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult result = this.events
                .GetAll()
                .ToDataSourceResult(
                request,
                c => new AdminEventViewModel
                {
                    Id = c.Id,
                    Title = c.Title,
                    Description = c.Description,
                    CreatorId = c.CreatorId
                });

            return this.Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Events_Update([DataSourceRequest]DataSourceRequest request, AdminEventViewModel currentEvent)
        {
            if (this.ModelState.IsValid)
            {
                var entity = this.events.GetAll().First(x => x.Id == currentEvent.Id);

                entity.Title = currentEvent.Title;
                entity.Description = currentEvent.Description;
                this.events.Save();
            }

            return this.Json(new[] { currentEvent }.ToDataSourceResult(request, this.ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Events_Destroy([DataSourceRequest]DataSourceRequest request, AdminEventViewModel currentEvent)
        {
            if (this.ModelState.IsValid)
            {
                this.events.Delete(currentEvent.Id);
            }

            return this.Json(new[] { currentEvent }.ToDataSourceResult(request, this.ModelState));
        }
    }
}
