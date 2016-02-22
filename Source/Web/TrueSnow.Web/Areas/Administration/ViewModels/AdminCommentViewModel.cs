namespace TrueSnow.Web.Areas.Administration.ViewModels
{
    public class AdminCommentViewModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string CreatorId { get; set; }

        public int? EventId { get; set; }

        public int? PostId { get; set; }
    }
}