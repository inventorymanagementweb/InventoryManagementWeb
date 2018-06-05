namespace InventoryManagement.Repositories.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class InitDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Guid(false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "User.UserRoles",
                c => new
                    {
                        UserId = c.Guid(false),
                        RoleId = c.Guid(false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Roles", t => t.RoleId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "User.UserClaims",
                c => new
                    {
                        Id = c.Int(false, true),
                        UserId = c.Guid(false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(false),
                        UserName = c.String(false, 128),
                        Email = c.String(false, 128),
                        FullName = c.String(false, 128),
                        EmailConfirmed = c.Boolean(false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(false),
                        TwoFactorEnabled = c.Boolean(false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(false),
                        AccessFailedCount = c.Int(false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.Email, unique: true, name: "EmailIndex");
            
            CreateTable(
                "User.UserLogins",
                c => new
                    {
                        LoginProvider = c.String(false, 128),
                        ProviderKey = c.String(false, 128),
                        UserId = c.Guid(false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("User.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("User.UserLogins", "UserId", "dbo.Users");
            DropForeignKey("User.UserClaims", "UserId", "dbo.Users");
            DropForeignKey("User.UserRoles", "RoleId", "dbo.Roles");
            DropIndex("User.UserLogins", new[] { "UserId" });
            DropIndex("dbo.Users", "EmailIndex");
            DropIndex("dbo.Users", "UserNameIndex");
            DropIndex("User.UserClaims", new[] { "UserId" });
            DropIndex("User.UserRoles", new[] { "RoleId" });
            DropIndex("User.UserRoles", new[] { "UserId" });
            DropTable("User.UserLogins");
            DropTable("dbo.Users");
            DropTable("User.UserClaims");
            DropTable("User.UserRoles");
            DropTable("dbo.Roles");
        }
    }
}
