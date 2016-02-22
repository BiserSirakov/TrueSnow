namespace TrueSnow.Web.Controllers
{
    using System.Web.Mvc;

    using TrueSnow.Services.Data.Contracts;

    public class FileController : BaseController
    {
        private readonly IFilesService files;

        public FileController(IFilesService files)
        {
            this.files = files;
        }

        public ActionResult Index(int id)
        {
            var fileToRetrieve = this.files.GetById(id);
            return this.File(fileToRetrieve.Content, fileToRetrieve.ContentType);
        }
    }
}