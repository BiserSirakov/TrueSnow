namespace TrueSnow.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    using Infrastructure.Constants;
    using Services.Data.Contracts;
    using ViewModels;

    [Authorize(Roles = IdentityRoles.Administrator)]
    public class AdminArticlesController : Controller
    {
        private readonly IArticlesService articles;

        public AdminArticlesController(IArticlesService articles)
        {
            this.articles = articles;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Articles_Read([DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult result = this.articles
                .GetAll()
                .ToDataSourceResult(
                request,
                c => new AdminArticleViewModel
                {
                    Id = c.Id,
                    Title = c.Title,
                    Content = c.Content,
                    CreatorId = c.CreatorId
                });

            return this.Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Articles_Update([DataSourceRequest]DataSourceRequest request, AdminArticleViewModel article)
        {
            if (this.ModelState.IsValid)
            {
                var entity = this.articles.GetAll().First(x => x.Id == article.Id);

                entity.Title = article.Title;
                entity.Content = article.Content;
                this.articles.Save();
            }

            return this.Json(new[] { article }.ToDataSourceResult(request, this.ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Articles_Destroy([DataSourceRequest]DataSourceRequest request, AdminArticleViewModel article)
        {
            if (this.ModelState.IsValid)
            {
                this.articles.Delete(article.Id);
            }

            return this.Json(new[] { article }.ToDataSourceResult(request, this.ModelState));
        }
    }
}
