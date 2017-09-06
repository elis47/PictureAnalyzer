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
                .Index(t => t.Name, unique: true)
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
            DropForeignKey("dbo.Paintings", "TypeID", "dbo.Types");
            DropForeignKey("dbo.Paintings", "ProfileID", "dbo.Profiles");
            DropForeignKey("dbo.Paintings", "PainterID", "dbo.Painters");
            DropForeignKey("dbo.Paintings", "InfluenceID", "dbo.Influences");
            DropForeignKey("dbo.Paintings", "GalleryID", "dbo.Galleries");
            DropForeignKey("dbo.PaintingColors", "Color_ID", "dbo.Colors");
            DropForeignKey("dbo.PaintingColors", "Painting_ID", "dbo.Paintings");
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
            DropIndex("dbo.Paintings", new[] { "Name" });
            DropIndex("dbo.Colors", new[] { "Name" });
            DropTable("dbo.PaintingColors");
            DropTable("dbo.Types");
            DropTable("dbo.Profiles");
            DropTable("dbo.Painters");
            DropTable("dbo.Influences");
            DropTable("dbo.Galleries");
            DropTable("dbo.Paintings");
            DropTable("dbo.Colors");
        }
    }
}
