namespace TrueSnow.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;

    using Data.Models;
    using Infrastructure.Mapping;
    using Models.Users;
    using Models.Articles;
    using Models.Events;
    using Services.Data.Contracts;
    using ViewModels.Search;

    public class SearchController : BaseController
    {
        private readonly UserManager<User> userManager;
        private readonly IArticlesService articles;
        private readonly IEventsService events;

        public SearchController(UserManager<User> userManager, IArticlesService articles, IEventsService events)
        {
            this.userManager = userManager;
            this.articles = articles;
            this.events = events;
        }

        public ActionResult GetSearch()
        {
            return this.PartialView("GetSearch");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(string query)
        {
            var users = this.userManager
                .Users
                .Where(x => x.FirstName.ToLower().Contains(query.ToLower()) || x.LastName.ToLower().Contains(query.ToLower()))
                .To<ProfileViewModel>()
                .ToList();

            var articles = this.articles
                .GetAll()
                .Where(x => x.Title.ToLower().Contains(query.ToLower()) || x.Content.ToLower().Contains(query.ToLower()))
                .To<ArticleViewModel>()
                .ToList();

            var events = this.events
                .GetAll()
                .Where(x => x.Title.ToLower().Contains(query.ToLower()) || x.Description.ToLower().Contains(query.ToLower()))
                .To<EventViewModel>()
                .ToList();

            var model = new SearchViewModel
            {
                Query = query,
                Users = users,
                Articles = articles,
                Events = events
            };

            return this.View(model);
        }
    }
}