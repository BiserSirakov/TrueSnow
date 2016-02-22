namespace TrueSnow.Services.Data
{
    using System.Linq;

    using TrueSnow.Data.Common;
    using TrueSnow.Data.Models;
    using TrueSnow.Services.Data.Contracts;

    public class LikesService : ILikesService
    {
        private readonly IDbRepository<Like> likes;

        public LikesService(IDbRepository<Like> likes)
        {
            this.likes = likes;
        }

        public void Add(Like likeToAdd)
        {
            this.likes.Add(likeToAdd);
            this.likes.Save();
        }

        public void Remove(Like likeToRemove)
        {
            this.likes.Delete(likeToRemove);
            this.likes.Save();
        }

        public IQueryable<Like> GetByPostId(int id)
        {
            return this.likes
                .All()
                .Where(l => l.PostId == id && !l.IsDeleted);
        }

        public Like GetByUserAndPostId(string userId, int postId)
        {
            return this.GetByPostId(postId)
                .FirstOrDefault(l => l.CreatorId == userId);
        }
    }
}
