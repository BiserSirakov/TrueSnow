namespace TrueSnow.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Microsoft.AspNet.Identity;

    using Infrastructure.Constants;
    using TrueSnow.Data.Models;
    using ViewModels;

    [Authorize(Roles = IdentityRoles.Administrator)]
    public class AdminUsersController : Controller
    {
        private readonly UserManager<User> userManager;

        public AdminUsersController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Users_Read([DataSourceRequest]DataSourceRequest request)
        {
            DataSourceResult result = this.userManager.Users
                .ToDataSourceResult(request, user => new AdminUserViewModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email
                });

            return this.Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Users_Update([DataSourceRequest]DataSourceRequest request, AdminUserViewModel user)
        {
            if (this.ModelState.IsValid)
            {
                var userToUpdate = this.userManager.FindById(user.Id);
                userToUpdate.FirstName = user.FirstName;
                userToUpdate.LastName = user.LastName;
                userToUpdate.Email = user.Email;

                var result = this.userManager.Update(userToUpdate);
            }

            return this.Json(new[] { user }.ToDataSourceResult(request, this.ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Users_Destroy([DataSourceRequest]DataSourceRequest request, AdminUserViewModel user)
        {
            if (this.ModelState.IsValid)
            {
                var userToDelete = this.userManager.FindById(user.Id);
                var removeFromRole = this.userManager.RemoveFromRole(user.Id, "User");

                var result = this.userManager.Delete(userToDelete);
            }

            return this.Json(new[] { user }.ToDataSourceResult(request, this.ModelState));
        }
    }
}
