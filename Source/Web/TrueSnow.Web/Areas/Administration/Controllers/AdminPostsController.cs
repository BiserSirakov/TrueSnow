namespace TrueSnow.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using Data.Models;
    using Infrastructure.Constants;
    using Services.Data.Contracts;
    using ViewModels;

    [Authorize(Roles = IdentityRoles.Administrator)]
    public class AdminPostsController : Controller
    {
        private readonly IPostsService posts;

        public AdminPostsController(IPostsService posts)
        {
            this.posts = posts;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Posts_Read([DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult result = this.posts
                .GetAll()
                .ToDataSourceResult(
                request,
                post => new AdminPostViewModel
                {
                    Id = post.Id,
                    Title = post.Title,
                    Content = post.Content,
                    CreatorId = post.CreatorId
                });

            return this.Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Posts_Update([DataSourceRequest]DataSourceRequest request, AdminPostViewModel post)
        {
            if (this.ModelState.IsValid)
            {
                var entity = this.posts.GetById(post.Id);

                entity.Title = post.Title;
                entity.Content = post.Content;
                this.posts.Save();
            }

            return this.Json(new[] { post }.ToDataSourceResult(request, this.ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Posts_Destroy([DataSourceRequest]DataSourceRequest request, Post post)
        {
            if (this.ModelState.IsValid)
            {
                this.posts.Delete(post.Id);
            }

            return this.Json(new[] { post }.ToDataSourceResult(request, this.ModelState));
        }
    }
}
