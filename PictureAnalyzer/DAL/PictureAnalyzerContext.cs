using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PictureAnalyzer.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PictureAnalyzer.DAL
{
    public class PictureAnalyzerContext : DbContext
    {
        public PictureAnalyzerContext() : base("PictureAnalyzerContext")
        {

        }

        public DbSet<Painter> Painters { get; set; }
        public DbSet<Painting> Paintings { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Influence> Influences { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Models.Type> Types { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
        }

    }
}
  
