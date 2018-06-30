namespace PictureAnalyzer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Colors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(nullable: false, maxLength: 600),
                        PersonalityTraits = c.String(nullable: false, maxLength: 600),
                        Keywords = c.String(nullable: false, maxLength: 600),
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
                        CurrentOwner = c.String(maxLength: 100),
                        HarmonyIndex = c.Double(nullable: false),
                        ConstrastIndex = c.Double(nullable: false),
                        LuminosityIndex = c.Double(nullable: false),
                        Link = c.String(nullable: false),
                        ApplicationUserId = c.String(nullable: false, maxLength: 128),
                        PainterID = c.Int(nullable: false),
                        TypeID = c.Int(nullable: false),
                        InfluenceID = c.Int(nullable: false),
                        GalleryID = c.Int(nullable: false),
                        TypeAPercentage = c.Double(),
                        TypeBPercentage = c.Double(),
                        TypeCPercentage = c.Double(),
                        TypeDPercentage = c.Double(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId, cascadeDelete: true)
                .ForeignKey("dbo.Galleries", t => t.GalleryID, cascadeDelete: true)
                .ForeignKey("dbo.Influences", t => t.InfluenceID, cascadeDelete: true)
                .ForeignKey("dbo.Painters", t => t.PainterID, cascadeDelete: true)
                .ForeignKey("dbo.Types", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.Name, unique: true)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.PainterID)
                .Index(t => t.TypeID)
                .Index(t => t.InfluenceID)
                .Index(t => t.GalleryID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
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
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
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
            
            CreateTable(
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
            
            CreateTable(
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
                        Keywords = c.String(nullable: false, maxLength: 800),
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
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
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
            
            CreateTable(
                "dbo.PaintingProfiles",
                c => new
                    {
                        Painting_ID = c.Int(nullable: false),
                        Profile_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Painting_ID, t.Profile_ID })
                .ForeignKey("dbo.Paintings", t => t.Painting_ID, cascadeDelete: true)
                .ForeignKey("dbo.Profiles", t => t.Profile_ID, cascadeDelete: true)
                .Index(t => t.Painting_ID)
                .Index(t => t.Profile_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Paintings", "TypeID", "dbo.Types");
            DropForeignKey("dbo.PaintingProfiles", "Profile_ID", "dbo.Profiles");
            DropForeignKey("dbo.PaintingProfiles", "Painting_ID", "dbo.Paintings");
            DropForeignKey("dbo.Paintings", "PainterID", "dbo.Painters");
            DropForeignKey("dbo.Paintings", "InfluenceID", "dbo.Influences");
            DropForeignKey("dbo.Paintings", "GalleryID", "dbo.Galleries");
            DropForeignKey("dbo.PaintingColors", "Color_ID", "dbo.Colors");
            DropForeignKey("dbo.PaintingColors", "Painting_ID", "dbo.Paintings");
            DropForeignKey("dbo.Paintings", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.PaintingProfiles", new[] { "Profile_ID" });
            DropIndex("dbo.PaintingProfiles", new[] { "Painting_ID" });
            DropIndex("dbo.PaintingColors", new[] { "Color_ID" });
            DropIndex("dbo.PaintingColors", new[] { "Painting_ID" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Types", new[] { "Name" });
            DropIndex("dbo.Profiles", new[] { "Name" });
            DropIndex("dbo.Painters", new[] { "Name" });
            DropIndex("dbo.Influences", new[] { "Name" });
            DropIndex("dbo.Galleries", new[] { "Name" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Paintings", new[] { "GalleryID" });
            DropIndex("dbo.Paintings", new[] { "InfluenceID" });
            DropIndex("dbo.Paintings", new[] { "TypeID" });
            DropIndex("dbo.Paintings", new[] { "PainterID" });
            DropIndex("dbo.Paintings", new[] { "ApplicationUserId" });
            DropIndex("dbo.Paintings", new[] { "Name" });
            DropIndex("dbo.Colors", new[] { "Name" });
            DropTable("dbo.PaintingProfiles");
            DropTable("dbo.PaintingColors");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Types");
            DropTable("dbo.Profiles");
            DropTable("dbo.Painters");
            DropTable("dbo.Influences");
            DropTable("dbo.Galleries");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Paintings");
            DropTable("dbo.Colors");
        }
    }
}
