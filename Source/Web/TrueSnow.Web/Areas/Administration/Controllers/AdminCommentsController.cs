namespace TrueSnow.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using Infrastructure.Constants;
    using Services.Data.Contracts;
    using ViewModels;

    [Authorize(Roles = IdentityRoles.Administrator)]
    public class AdminCommentsController : Controller
    {
        private readonly ICommentsService comments;

        public AdminCommentsController(ICommentsService comments)
        {
            this.comments = comments;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Comments_Read([DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult result = this.comments
                .GetAll()
                .ToDataSourceResult(
                request,
                comment => new AdminCommentViewModel
                {
                    Id = comment.Id,
                    Content = comment.Content,
                    CreatorId = comment.CreatorId,
                    EventId = comment.EventId,
                    PostId = comment.PostId
                });

            return this.Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Comments_Update([DataSourceRequest]DataSourceRequest request, AdminCommentViewModel comment)
        {
            if (this.ModelState.IsValid)
            {
                var entity = this.comments.GetById(comment.Id);

                entity.Content = comment.Content;
                this.comments.Save();
            }

            return this.Json(new[] { comment }.ToDataSourceResult(request, this.ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Comments_Destroy([DataSourceRequest]DataSourceRequest request, AdminCommentViewModel comment)
        {
            if (this.ModelState.IsValid)
            {
                this.comments.Delete(comment.Id);
            }

            return this.Json(new[] { comment }.ToDataSourceResult(request, this.ModelState));
        }
    }
}
