using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace PictureAnalyzer.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        //public DbSet<Painting> Paintings { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public virtual ICollection<Painting> Paintings { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext() : base("DefaultConnection", throwIfV1Schema: false)
        {

        }


        public virtual DbSet<Painting> Paintings { get; set; }
        public virtual DbSet<Color> Colors { get; set; }
        public virtual DbSet<Gallery> Galleries { get; set; }
        public virtual DbSet<Influence> Influences { get; set; }
        public virtual DbSet<Painter> Painters { get; set; }
        public virtual DbSet<Profile> Profiles { get; set; }
        public virtual DbSet<Type> Types { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //One-to-many
            modelBuilder.Entity<Painting>()
                .HasRequired(g => g.Gallery)
                .WithMany(p => p.Paintings);
            modelBuilder.Entity<Painting>()
                .HasRequired(t => t.Type)
                .WithMany(p => p.Paintings);
            modelBuilder.Entity<Painting>()
                .HasRequired(i => i.Influence)
                .WithMany(p => p.Paintings);
            modelBuilder.Entity<Painting>()
                .HasRequired(i => i.Painter)
                .WithMany(p => p.Paintings);
            modelBuilder.Entity<Painting>()
                .HasRequired(a => a.ApplicationUser)
                .WithMany(p => p.Paintings);

            //Many-to-many
            modelBuilder.Entity<Painting>()
                .HasMany(c => c.Colors)
                .WithMany(p => p.Paintings);
            modelBuilder.Entity<Painting>()
                .HasMany(c => c.Profiles)
                .WithMany(p => p.Paintings);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}