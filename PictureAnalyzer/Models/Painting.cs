using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PictureAnalyzer.Models
{
    public class Painting
    {
        public int ID { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The name can not be longer than 100 characters.")]
        [Index(IsUnique = true)]
        public string Name { get; set; }
        [StringLength(600, ErrorMessage = "The description can not be longer than 600 characters.")]
        public string Description { get; set; }
        [Required]
        [MaxLength(100)]
        public string CurrentOwner { get; set; }
        [Required]
        public double HarmonyIndex { get; set; }
        [Required]
        public double ConstrastIndex { get; set; }
        [Required]
        public double LuminosityIndex { get; set; }

        //public string ApplicationUserID { get; set; }
        public int PainterID { get; set; }
        public int TypeID { get; set; }
        public int InfluenceID { get; set; }
        public int ProfileID { get; set; }
        public int GalleryID { get; set; }

        //public virtual ApplicationUser User { get; set; }
        public virtual Painter Painter { get; set; }
        public virtual Type Type { get; set; }
        public virtual Influence Influence { get; set; }
        public virtual Profile Profile { get; set; }
        public virtual Gallery Gallery { get; set; }
        public virtual ICollection<Color> Colors { get; set; }
    }
}