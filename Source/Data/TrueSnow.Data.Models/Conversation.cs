namespace TrueSnow.Data.Models
{
    using TrueSnow.Data.Common.Models;

    public class Conversation : BaseModel<int>
    {
        public string ConnectionIdUser { get; set; }

        public string ConnectionIdAnotherUser { get; set; }

        public string UserGroup { get; set; }

        public string Message { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public string MsgDate { get; set; }

        public string MsgDuration { get; set; }

        public string UserId { get; set; }

        public string AnotherUserId { get; set; }
    }
}
