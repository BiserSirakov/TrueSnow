namespace TrueSnow.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;

    using Data.Models;
    using Services.Data.Contracts;

    public class LikesController : BaseController
    {
        private readonly ILikesService likes;

        public LikesController(ILikesService likes)
        {
            this.likes = likes;
        }

        [HttpPost]
        public ActionResult Like(int postId)
        {
            var like = new Like
            {
                CreatorId = this.User.Identity.GetUserId(),
                PostId = postId,
                CreatedOn = DateTime.UtcNow
            };

            this.likes.Add(like);

            var postLikesCount = this.likes.GetByPostId(postId).Count().ToString();

            return this.Json(new { Count = postLikesCount });
        }

        [HttpPost]
        public ActionResult Unlike(int postId)
        {
            var currentUserId = this.User.Identity.GetUserId();
            var likeToRemove = this.likes.GetByUserAndPostId(currentUserId, postId);

            this.likes.Remove(likeToRemove);

            var postLikesCount = this.likes.GetByPostId(postId).Count().ToString();

            return this.Json(new { Count = postLikesCount });
        }
    }
}