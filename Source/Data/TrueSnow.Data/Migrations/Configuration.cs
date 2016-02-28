namespace TrueSnow.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Microsoft.AspNet.Identity.EntityFramework;

    using Web.Infrastructure.Constants;

    public sealed class Configuration : DbMigrationsConfiguration<TrueSnowDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(TrueSnowDbContext context)
        {
            if (!context.Roles.Any())
            {
                context.Roles.Add(new IdentityRole { Name = IdentityRoles.Administrator });
                context.Roles.Add(new IdentityRole { Name = IdentityRoles.User });
                context.SaveChanges();
            }
        }
    }
}
