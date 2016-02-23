namespace TrueSnow.Web.Models.Posts
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using AutoMapper;
    using Data.Models;
    using Infrastructure.Mapping;

    public class PostViewModel : IMapFrom<Post>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be between {2} and {1} characters long.", MinimumLength = 2)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "The {0} must be at least {1} characters long.")]
        [Display(Name = "Content")]
        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public File Photo { get; set; }

        public User Creator { get; set; }

        public int CommentsCount { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public int Likes { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Post, PostViewModel>()
                .ForMember(x => x.CommentsCount, opts => opts.MapFrom(x => x.Comments.Where(c => !c.IsDeleted).Count()))
                .ForMember(x => x.Likes, opts => opts.MapFrom(x => x.Likes.Where(l => !l.IsDeleted).Count()));
        }
    }
}
