namespace PictureAnalyzer.DAL.PictureAnalyzerMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserRoles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.ApplicationUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.IdentityRoles", t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id);
            
            CreateTable(
                "dbo.Colors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(nullable: false, maxLength: 600),
                        PersonalityTraits = c.String(nullable: false, maxLength: 600),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.Paintings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(maxLength: 600),
                        CurrentOwner = c.String(nullable: false, maxLength: 100),
                        HarmonyIndex = c.Double(nullable: false),
                        ConstrastIndex = c.Double(nullable: false),
                        LuminosityIndex = c.Double(nullable: false),
                        Link = c.String(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        PainterID = c.Int(nullable: false),
                        TypeID = c.Int(nullable: false),
                        InfluenceID = c.Int(nullable: false),
                        ProfileID = c.Int(nullable: false),
                        GalleryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Galleries", t => t.GalleryID, cascadeDelete: true)
                .ForeignKey("dbo.Influences", t => t.InfluenceID, cascadeDelete: true)
                .ForeignKey("dbo.Painters", t => t.PainterID, cascadeDelete: true)
                .ForeignKey("dbo.Profiles", t => t.ProfileID, cascadeDelete: true)
                .ForeignKey("dbo.Types", t => t.TypeID, cascadeDelete: true)
                .ForeignKey("dbo.ApplicationUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.Name, unique: true)
                .Index(t => t.UserId)
                .Index(t => t.PainterID)
                .Index(t => t.TypeID)
                .Index(t => t.InfluenceID)
                .Index(t => t.ProfileID)
                .Index(t => t.GalleryID);
            
            CreateTable(
                "dbo.Galleries",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.Influences",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false, maxLength: 600),
                        BeginYear = c.Int(),
                        EndYear = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.Painters",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(maxLength: 600),
                        Country = c.String(nullable: false, maxLength: 100),
                        Link = c.String(),
                        BirthDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        PassDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false, maxLength: 800),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.Types",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false, maxLength: 600),
                        PersonalityTraits = c.String(nullable: false, maxLength: 600),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.IdentityRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PaintingColors",
                c => new
                    {
                        Painting_ID = c.Int(nullable: false),
                        Color_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Painting_ID, t.Color_ID })
                .ForeignKey("dbo.Paintings", t => t.Painting_ID, cascadeDelete: true)
                .ForeignKey("dbo.Colors", t => t.Color_ID, cascadeDelete: true)
                .Index(t => t.Painting_ID)
                .Index(t => t.Color_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRoles", "IdentityRole_Id", "dbo.IdentityRoles");
            DropForeignKey("dbo.Paintings", "UserId", "dbo.ApplicationUsers");
            DropForeignKey("dbo.Paintings", "TypeID", "dbo.Types");
            DropForeignKey("dbo.Paintings", "ProfileID", "dbo.Profiles");
            DropForeignKey("dbo.Paintings", "PainterID", "dbo.Painters");
            DropForeignKey("dbo.Paintings", "InfluenceID", "dbo.Influences");
            DropForeignKey("dbo.Paintings", "GalleryID", "dbo.Galleries");
            DropForeignKey("dbo.PaintingColors", "Color_ID", "dbo.Colors");
            DropForeignKey("dbo.PaintingColors", "Painting_ID", "dbo.Paintings");
            DropForeignKey("dbo.IdentityUserRoles", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IdentityUserLogins", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropForeignKey("dbo.IdentityUserClaims", "ApplicationUser_Id", "dbo.ApplicationUsers");
            DropIndex("dbo.PaintingColors", new[] { "Color_ID" });
            DropIndex("dbo.PaintingColors", new[] { "Painting_ID" });
            DropIndex("dbo.Types", new[] { "Name" });
            DropIndex("dbo.Profiles", new[] { "Name" });
            DropIndex("dbo.Painters", new[] { "Name" });
            DropIndex("dbo.Influences", new[] { "Name" });
            DropIndex("dbo.Galleries", new[] { "Name" });
            DropIndex("dbo.Paintings", new[] { "GalleryID" });
            DropIndex("dbo.Paintings", new[] { "ProfileID" });
            DropIndex("dbo.Paintings", new[] { "InfluenceID" });
            DropIndex("dbo.Paintings", new[] { "TypeID" });
            DropIndex("dbo.Paintings", new[] { "PainterID" });
            DropIndex("dbo.Paintings", new[] { "UserId" });
            DropIndex("dbo.Paintings", new[] { "Name" });
            DropIndex("dbo.Colors", new[] { "Name" });
            DropIndex("dbo.IdentityUserRoles", new[] { "IdentityRole_Id" });
            DropIndex("dbo.IdentityUserRoles", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserLogins", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaims", new[] { "ApplicationUser_Id" });
            DropTable("dbo.PaintingColors");
            DropTable("dbo.IdentityRoles");
            DropTable("dbo.Types");
            DropTable("dbo.Profiles");
            DropTable("dbo.Painters");
            DropTable("dbo.Influences");
            DropTable("dbo.Galleries");
            DropTable("dbo.Paintings");
            DropTable("dbo.Colors");
            DropTable("dbo.IdentityUserRoles");
            DropTable("dbo.IdentityUserLogins");
            DropTable("dbo.IdentityUserClaims");
            DropTable("dbo.ApplicationUsers");
        }
    }
}
