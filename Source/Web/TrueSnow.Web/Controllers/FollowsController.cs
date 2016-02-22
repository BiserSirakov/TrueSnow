namespace TrueSnow.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;

    using Data.Models;
    using Infrastructure.Mapping;
    using Models.Users;

    public class FollowsController : BaseController
    {
        private readonly UserManager<User> userManager;

        public FollowsController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        private User CurrentUser
        {
            get
            {
                var currentUserId = this.HttpContext.User.Identity.GetUserId();
                return this.userManager.FindById(currentUserId);
            }
        }

        public ActionResult Follow(string id)
        {
            this.TempData["userId"] = id;

            var userToFollow = this.userManager.FindById(id);

            this.CurrentUser.Following.Add(userToFollow);
            var result = this.userManager.Update(this.CurrentUser);

            return this.Redirect(this.Request.UrlReferrer.ToString());
        }

        public ActionResult Unfollow(string id)
        {
            this.TempData["userId"] = id;

            var userToUnfollow = this.userManager.FindById(id);

            this.CurrentUser.Following.Remove(userToUnfollow);
            this.userManager.Update(this.CurrentUser);

            return this.Redirect(this.Request.UrlReferrer.ToString());
        }

        public ActionResult Following()
        {
            // Not the CurrentUser but the user`s profile that is opened
            var currentUserId = this.TempData["userId"].ToString();
            var currentUser = this.userManager.FindById(currentUserId);

            var model = currentUser
                .Following
                .AsQueryable()
                .To<ProfileViewModel>()
                .ToList();

            return this.PartialView("Follow", model);
        }

        public ActionResult Followers()
        {
            // Not the CurrentUser but the user`s profile that is opened
            var currentUserId = this.TempData["userId"].ToString();
            var currentUser = this.userManager.FindById(currentUserId);

            var model = currentUser
                .Followers
                .AsQueryable()
                .To<ProfileViewModel>()
                .ToList();

            return this.PartialView("Follow", model);
        }
    }
}