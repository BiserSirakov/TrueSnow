namespace TrueSnow.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Common.Models;

    public class Post : BaseModel<int>
    {
        private ICollection<Comment> comments;
        private ICollection<Like> likes;

        public Post()
        {
            this.comments = new HashSet<Comment>();
            this.likes = new HashSet<Like>();
        }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(255)]
        public string Content { get; set; }

        [Required]
        public string CreatorId { get; set; }

        public virtual User Creator { get; set; }

        [Required]
        public int PhotoId { get; set; }

        public virtual File Photo { get; set; }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        public virtual ICollection<Like> Likes
        {
            get { return this.likes; }
            set { this.likes = value; }
        }
    }
}
