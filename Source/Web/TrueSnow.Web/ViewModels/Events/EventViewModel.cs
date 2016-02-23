namespace TrueSnow.Web.Models.Events
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    using AutoMapper;

    using Data.Models;
    using Infrastructure;
    using Infrastructure.Mapping;
    using Services.Web;
    using Services.Web.Contracts;

    public class EventViewModel : IMapFrom<Event>, IHaveCustomMappings
    {
        private ISanitizer sanitizer;

        public EventViewModel()
        {
            this.sanitizer = new HtmlSanitizerAdapter();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        [AllowHtml]
        public string Description { get; set; }

        [AllowHtml]
        public string DescriptionSanitized
        {
            get
            {
                return this.sanitizer.Sanitize(this.Description);
            }
        }

        public User Creator { get; set; }

        public DateTime CreatedOn { get; set; }

        public File Photo { get; set; }

        public string Url
        {
            get
            {
                IIdentifierProvider identifier = new IdentifierProvider();
                return $"/Events/ById/{identifier.EncodeId(this.Id)}";
            }
        }

        public ICollection<User> Attendands { get; set; }

        public int AttendantsCount { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<Event, EventViewModel>()
                .ForMember(x => x.AttendantsCount, opts => opts.MapFrom(x => x.Attendants.Count));
        }
    }
}
