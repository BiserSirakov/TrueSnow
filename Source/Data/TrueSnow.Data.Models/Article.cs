namespace TrueSnow.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using TrueSnow.Data.Common.Models;

    public class Article : BaseModel<int>
    {
        private ICollection<Like> likes;
        private ICollection<Comment> comments;

        public Article()
        {
            this.likes = new HashSet<Like>();
            this.comments = new HashSet<Comment>();
        }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(5000)]
        public string Content { get; set; }

        [Required]
        public string CreatorId { get; set; }

        public virtual User Creator { get; set; }

        public int? PhotoId { get; set; }

        [Required]
        public virtual File Photo { get; set; }

        public string VideoUrl { get; set; }

        public virtual ICollection<Like> Likes
        {
            get { return this.likes; }
            set { this.likes = value; }
        }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }
    }
}
