namespace TrueSnow.Web.Hub
{
    using System.Linq;
    using Data;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.SignalR;

    public class Chat : Hub
    {
        private TrueSnowDbContext db = new TrueSnowDbContext();

        public void SendMessage(string message)
        {
            var msg = string.Format("{0}: {1}", this.Context.ConnectionId, message);
            var id = this.Context.User.Identity.GetUserId();
            var currentUser = this.db.Users.FirstOrDefault(x => x.Id == id);
            this.Clients.All.addMessage(currentUser.FirstName + " " + currentUser.LastName + " : " + msg);
        }

        public void JoinRoom(string room)
        {
            this.Groups.Add(this.Context.ConnectionId, room);
            this.Clients.Caller.joinRoom(room);
        }

        public void SendMessageToRoom(string message, string[] rooms)
        {
            var msg = string.Format("{0}: {1}", this.Context.ConnectionId, message);

            for (int i = 0; i < rooms.Length; i++)
            {
                this.Clients.Group(rooms[i]).addMessage(msg);
            }
        }
    }
}