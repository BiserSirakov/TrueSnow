namespace TrueSnow.Web.Controllers
{
    using System.Web.Mvc;

    public class MessagesController : BaseController
    {
        public ActionResult Index()
        {
            return this.View();
        }
    }
}