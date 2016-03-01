namespace TrueSnow.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using TrueSnow.Data.Common.Models;

    public class Like : BaseModel<int>
    {
        [Required]
        public string CreatorId { get; set; }

        public virtual User Creator { get; set; }

        public int? PostId { get; set; }

        public virtual Post Post { get; set; }
    }
}
