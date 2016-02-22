namespace TrueSnow.Web.Controllers
{
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;

    using Data.Models;
    using Models.Posts;
    using Infrastructure.Mapping;
    using Services.Data.Contracts;

    public class PostsController : BaseController
    {
        private const int HomepagePostsCount = 15;

        private readonly IPostsService posts;
        private readonly ILikesService likes;
        private readonly UserManager<User> userManager;

        public PostsController(IPostsService posts, ILikesService likes, UserManager<User> userManager)
        {
            this.posts = posts;
            this.likes = likes;
            this.userManager = userManager;
        }

        public ActionResult Index()
        {
            var currentUser = this.userManager.FindById(this.User.Identity.GetUserId());
            var followingPosts = this.posts.GetFollowingPostsByUserFollowing(currentUser.Following);

            var postsViewModel = followingPosts
                .To<PostViewModel>()
                .Take(HomepagePostsCount)
                .ToList();

            return this.PartialView("Index", postsViewModel);
        }

        public ActionResult ById(int id)
        {
            var post = this.posts.GetById(id);
            var postModel = this.Mapper.Map<PostViewModel>(post);

            var like = this.likes.GetByUserAndPostId(this.User.Identity.GetUserId(), id);

            var model = new PostByIdViewModel
            {
                PostViewModel = postModel,
                PostLikedByCurrentUser = like != null
            };

            return this.View("ById", model);
        }

        public ActionResult ByUserId(string userId)
        {
            var postsViewModel = this.posts
                .GetByUserId(userId)
                .To<PostViewModel>()
                .ToList();

            return this.PartialView("ByUser", postsViewModel);
        }

        public ActionResult GetCreate()
        {
            return this.PartialView("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostViewModel post, HttpPostedFileBase upload)
        {
            if (this.ModelState.IsValid)
            {
                var postToAdd = new Post
                {
                    Title = post.Title,
                    Content = post.Content,
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

                    postToAdd.Photo = photo;
                }

                this.posts.Add(postToAdd);
            }

            return this.Redirect(this.Request.UrlReferrer.ToString());
        }
    }
}
