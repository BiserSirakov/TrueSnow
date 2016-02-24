namespace TrueSnow.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            this.CreateTable(
                "dbo.Articles",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Title = c.String(nullable: false),
                    Content = c.String(nullable: false),
                    CreatorId = c.String(nullable: false, maxLength: 128),
                    PhotoId = c.Int(),
                    VideoUrl = c.String(),
                    CreatedOn = c.DateTime(nullable: false),
                    ModifiedOn = c.DateTime(),
                    IsDeleted = c.Boolean(nullable: false),
                    DeletedOn = c.DateTime(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatorId, cascadeDelete: true)
                .ForeignKey("dbo.Files", t => t.PhotoId)
                .Index(t => t.CreatorId)
                .Index(t => t.PhotoId)
                .Index(t => t.IsDeleted);

            this.CreateTable(
                "dbo.Comments",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Content = c.String(nullable: false),
                    CreatorId = c.String(maxLength: 128),
                    EventId = c.Int(),
                    PostId = c.Int(),
                    CreatedOn = c.DateTime(nullable: false),
                    ModifiedOn = c.DateTime(),
                    IsDeleted = c.Boolean(nullable: false),
                    DeletedOn = c.DateTime(),
                    Article_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.PostId)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatorId)
                .ForeignKey("dbo.Events", t => t.EventId)
                .ForeignKey("dbo.Articles", t => t.Article_Id)
                .Index(t => t.CreatorId)
                .Index(t => t.EventId)
                .Index(t => t.PostId)
                .Index(t => t.IsDeleted)
                .Index(t => t.Article_Id);

            this.CreateTable(
                "dbo.AspNetUsers",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    FirstName = c.String(nullable: false),
                    LastName = c.String(nullable: false),
                    Email = c.String(maxLength: 256),
                    EmailConfirmed = c.Boolean(nullable: false),
                    PasswordHash = c.String(),
                    SecurityStamp = c.String(),
                    PhoneNumber = c.String(),
                    PhoneNumberConfirmed = c.Boolean(nullable: false),
                    TwoFactorEnabled = c.Boolean(nullable: false),
                    LockoutEndDateUtc = c.DateTime(),
                    LockoutEnabled = c.Boolean(nullable: false),
                    AccessFailedCount = c.Int(nullable: false),
                    UserName = c.String(nullable: false, maxLength: 256),
                    Event_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Events", t => t.Event_Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.Event_Id);

            this.CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserId = c.String(nullable: false, maxLength: 128),
                    ClaimType = c.String(),
                    ClaimValue = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            this.CreateTable(
                "dbo.Files",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    FileName = c.String(maxLength: 255),
                    ContentType = c.String(maxLength: 100),
                    Content = c.Binary(nullable: false),
                    FileType = c.Int(nullable: false),
                    CreatedOn = c.DateTime(nullable: false),
                    ModifiedOn = c.DateTime(),
                    IsDeleted = c.Boolean(nullable: false),
                    DeletedOn = c.DateTime(),
                    User_Id = c.String(maxLength: 128),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.IsDeleted)
                .Index(t => t.User_Id);

            this.CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                {
                    LoginProvider = c.String(nullable: false, maxLength: 128),
                    ProviderKey = c.String(nullable: false, maxLength: 128),
                    UserId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            this.CreateTable(
                "dbo.Posts",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Title = c.String(nullable: false),
                    Content = c.String(nullable: false),
                    CreatorId = c.String(nullable: false, maxLength: 128),
                    PhotoId = c.Int(nullable: false),
                    CreatedOn = c.DateTime(nullable: false),
                    ModifiedOn = c.DateTime(),
                    IsDeleted = c.Boolean(nullable: false),
                    DeletedOn = c.DateTime(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatorId, cascadeDelete: true)
                .ForeignKey("dbo.Files", t => t.PhotoId, cascadeDelete: true)
                .Index(t => t.CreatorId)
                .Index(t => t.PhotoId)
                .Index(t => t.IsDeleted);

            this.CreateTable(
                "dbo.Likes",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    CreatorId = c.String(nullable: false, maxLength: 128),
                    PostId = c.Int(),
                    CreatedOn = c.DateTime(nullable: false),
                    ModifiedOn = c.DateTime(),
                    IsDeleted = c.Boolean(nullable: false),
                    DeletedOn = c.DateTime(),
                    Article_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatorId, cascadeDelete: true)
                .ForeignKey("dbo.Posts", t => t.PostId)
                .ForeignKey("dbo.Articles", t => t.Article_Id)
                .Index(t => t.CreatorId)
                .Index(t => t.PostId)
                .Index(t => t.IsDeleted)
                .Index(t => t.Article_Id);

            this.CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                {
                    UserId = c.String(nullable: false, maxLength: 128),
                    RoleId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);

            this.CreateTable(
                "dbo.Events",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Title = c.String(nullable: false),
                    Description = c.String(nullable: false),
                    CreatorId = c.String(maxLength: 128),
                    PhotoId = c.Int(nullable: false),
                    CreatedOn = c.DateTime(nullable: false),
                    ModifiedOn = c.DateTime(),
                    IsDeleted = c.Boolean(nullable: false),
                    DeletedOn = c.DateTime(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatorId)
                .ForeignKey("dbo.Files", t => t.PhotoId, cascadeDelete: true)
                .Index(t => t.CreatorId)
                .Index(t => t.PhotoId)
                .Index(t => t.IsDeleted);

            this.CreateTable(
                "dbo.Conversations",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    ConnectionIdUser = c.String(),
                    ConnectionIdAnotherUser = c.String(),
                    UserGroup = c.String(),
                    Message = c.String(),
                    StartTime = c.String(),
                    EndTime = c.String(),
                    MsgDate = c.String(),
                    MsgDuration = c.String(),
                    UserId = c.String(),
                    AnotherUserId = c.String(),
                    CreatedOn = c.DateTime(nullable: false),
                    ModifiedOn = c.DateTime(),
                    IsDeleted = c.Boolean(nullable: false),
                    DeletedOn = c.DateTime(),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.IsDeleted);

            this.CreateTable(
                "dbo.AspNetRoles",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Name = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");

            this.CreateTable(
                "dbo.UserUsers",
                c => new
                {
                    User_Id = c.String(nullable: false, maxLength: 128),
                    User_Id1 = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.User_Id, t.User_Id1 })
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id1)
                .Index(t => t.User_Id)
                .Index(t => t.User_Id1);
        }

        public override void Down()
        {
            this.DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            this.DropForeignKey("dbo.Articles", "PhotoId", "dbo.Files");
            this.DropForeignKey("dbo.Likes", "Article_Id", "dbo.Articles");
            this.DropForeignKey("dbo.Articles", "CreatorId", "dbo.AspNetUsers");
            this.DropForeignKey("dbo.Comments", "Article_Id", "dbo.Articles");
            this.DropForeignKey("dbo.Events", "PhotoId", "dbo.Files");
            this.DropForeignKey("dbo.Events", "CreatorId", "dbo.AspNetUsers");
            this.DropForeignKey("dbo.Comments", "EventId", "dbo.Events");
            this.DropForeignKey("dbo.AspNetUsers", "Event_Id", "dbo.Events");
            this.DropForeignKey("dbo.Comments", "CreatorId", "dbo.AspNetUsers");
            this.DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            this.DropForeignKey("dbo.Posts", "PhotoId", "dbo.Files");
            this.DropForeignKey("dbo.Likes", "PostId", "dbo.Posts");
            this.DropForeignKey("dbo.Likes", "CreatorId", "dbo.AspNetUsers");
            this.DropForeignKey("dbo.Posts", "CreatorId", "dbo.AspNetUsers");
            this.DropForeignKey("dbo.Comments", "PostId", "dbo.Posts");
            this.DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            this.DropForeignKey("dbo.UserUsers", "User_Id1", "dbo.AspNetUsers");
            this.DropForeignKey("dbo.UserUsers", "User_Id", "dbo.AspNetUsers");
            this.DropForeignKey("dbo.Files", "User_Id", "dbo.AspNetUsers");
            this.DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            this.DropIndex("dbo.UserUsers", new[] { "User_Id1" });
            this.DropIndex("dbo.UserUsers", new[] { "User_Id" });
            this.DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            this.DropIndex("dbo.Conversations", new[] { "IsDeleted" });
            this.DropIndex("dbo.Events", new[] { "IsDeleted" });
            this.DropIndex("dbo.Events", new[] { "PhotoId" });
            this.DropIndex("dbo.Events", new[] { "CreatorId" });
            this.DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            this.DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            this.DropIndex("dbo.Likes", new[] { "Article_Id" });
            this.DropIndex("dbo.Likes", new[] { "IsDeleted" });
            this.DropIndex("dbo.Likes", new[] { "PostId" });
            this.DropIndex("dbo.Likes", new[] { "CreatorId" });
            this.DropIndex("dbo.Posts", new[] { "IsDeleted" });
            this.DropIndex("dbo.Posts", new[] { "PhotoId" });
            this.DropIndex("dbo.Posts", new[] { "CreatorId" });
            this.DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            this.DropIndex("dbo.Files", new[] { "User_Id" });
            this.DropIndex("dbo.Files", new[] { "IsDeleted" });
            this.DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            this.DropIndex("dbo.AspNetUsers", new[] { "Event_Id" });
            this.DropIndex("dbo.AspNetUsers", "UserNameIndex");
            this.DropIndex("dbo.Comments", new[] { "Article_Id" });
            this.DropIndex("dbo.Comments", new[] { "IsDeleted" });
            this.DropIndex("dbo.Comments", new[] { "PostId" });
            this.DropIndex("dbo.Comments", new[] { "EventId" });
            this.DropIndex("dbo.Comments", new[] { "CreatorId" });
            this.DropIndex("dbo.Articles", new[] { "IsDeleted" });
            this.DropIndex("dbo.Articles", new[] { "PhotoId" });
            this.DropIndex("dbo.Articles", new[] { "CreatorId" });
            this.DropTable("dbo.UserUsers");
            this.DropTable("dbo.AspNetRoles");
            this.DropTable("dbo.Conversations");
            this.DropTable("dbo.Events");
            this.DropTable("dbo.AspNetUserRoles");
            this.DropTable("dbo.Likes");
            this.DropTable("dbo.Posts");
            this.DropTable("dbo.AspNetUserLogins");
            this.DropTable("dbo.Files");
            this.DropTable("dbo.AspNetUserClaims");
            this.DropTable("dbo.AspNetUsers");
            this.DropTable("dbo.Comments");
            this.DropTable("dbo.Articles");
        }
    }
}
