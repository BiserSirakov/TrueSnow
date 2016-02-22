namespace TrueSnow.Web.Models.Articles
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper;

    using Data.Models;
    using Infrastructure;
    using Infrastructure.Mapping;
    using Services.Web;
    using Services.Web.Contracts;

    public class ArticleViewModel : IMapFrom<Article>, IHaveCustomMappings
    {
        private ISanitizer sanitizer;

        public ArticleViewModel()
        {
            this.sanitizer = new HtmlSanitizerAdapter();
        }

        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MinLength(5)]
        [AllowHtml]
        public string Content { get; set; }

        [AllowHtml]
        public string ContentSanitized
        {
            get
            {
                return this.sanitizer.Sanitize(this.Content);
            }
        }

        public DateTime CreatedOn { get; set; }

        public File Photo { get; set; }

        public User Creator { get; set; }

        public string VideoUrl { get; set; }

        public int CommentsCount { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public int Likes { get; set; }

        public string Url
        {
            get
            {
                IIdentifierProvider identifier = new IdentifierProvider();
                return $"/Articles/ById/{identifier.EncodeId(this.Id)}";
            }
        }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Article, ArticleViewModel>()
                .ForMember(x => x.CommentsCount, opts => opts.MapFrom(x => x.Comments.Count))
                .ForMember(x => x.Likes, opts => opts.MapFrom(x => x.Likes.Where(l => !l.IsDeleted).Count()));
        }
    }
}