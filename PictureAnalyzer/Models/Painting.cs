using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace PictureAnalyzer.Models
{
    public class Painting
    {
        public Painting()
        {
            Colors = new HashSet<Color>();
        }

        public int ID { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The name can not be longer than 100 characters.")]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        [StringLength(600, ErrorMessage = "The description can not be longer than 600 characters.")]
        public string Description { get; set; }

        [MaxLength(100)]
        [Display(Name ="Current owner")]
        public string CurrentOwner { get; set; }

        public double HarmonyIndex { get; set; }

        public double ConstrastIndex { get; set; }

        public double LuminosityIndex { get; set; }

        [Required]
        public string Link { get; set; }

        [Required]
        [NotMapped]
        public HttpPostedFileBase File { get; set; }

        [Display(Name="User")]
        public string ApplicationUserId { get; set; }

        [Display(Name = "Painter")]
        public int PainterID { get; set; }

        [Display(Name = "Type")]
        public int? TypeID { get; set; }

        [Display(Name = "Influence")]
        public int? InfluenceID { get; set; }

        [Display(Name = "Profile")]
        public int? ProfileID { get; set; }

        [Display(Name = "Gallery")]
        public int? GalleryID { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Painter Painter { get; set; }
        public virtual Type Type { get; set; }
        public virtual Influence Influence { get; set; }
        public virtual Profile Profile { get; set; }
        public virtual Gallery Gallery { get; set; }
        public virtual ICollection<Color> Colors { get; set; }

    }
}