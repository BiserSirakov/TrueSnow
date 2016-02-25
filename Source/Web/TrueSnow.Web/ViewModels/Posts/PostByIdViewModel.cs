namespace TrueSnow.Web.Models.Posts
{
    using TrueSnow.Data.Models;

    public class PostByIdViewModel
    {
        public PostViewModel PostViewModel { get; set; }

        public bool PostLikedByCurrentUser { get; set; }
    }
}