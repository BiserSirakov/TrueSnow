﻿namespace TrueSnow.Web.Controllers
{
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;

    using Infrastructure.Mapping;
    using TrueSnow.Services.Data.Contracts;
    using Models.Articles;
    using Data.Models;
    using System.IO;
    public class ArticlesController : BaseController
    {
        private readonly IArticlesService articles;

        public ArticlesController(IArticlesService articles)
        {
            this.articles = articles;
        }

        public ActionResult Index()
        {
            var articlesModel = this.articles
                .GetAll()
                .To<ArticleViewModel>()
                .ToList();

            return this.View(articlesModel);
        }

        public ActionResult ById(string id)
        {
            var article = this.articles.GetById(id);
            var model = this.Mapper.Map<ArticleViewModel>(article);
            return this.View(model);
        }

        public ActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ArticleViewModel model, HttpPostedFileBase upload)
        {
            if (this.ModelState.IsValid)
            {
                var articleToAdd = new Article
                {
                    Title = model.Title,
                    Content = model.Content,
                    CreatorId = this.User.Identity.GetUserId()
                };

                if (upload != null && upload.ContentLength > 0)
                {
                    var photo = new Data.Models.File
                    {
                        FileName = Path.GetFileName(upload.FileName),
                        FileType = FileType.Photo,
                        ContentType = upload.ContentType
                    };

                    using (var reader = new BinaryReader(upload.InputStream))
                    {
                        photo.Content = reader.ReadBytes(upload.ContentLength);
                    }

                    articleToAdd.Photo = photo;
                }

                this.articles.Add(articleToAdd);
            }

            return this.View(model);
        }
    }
}