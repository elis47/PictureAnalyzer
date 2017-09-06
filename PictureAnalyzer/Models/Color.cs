using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PictureAnalyzer.Models
{
    public class Color
    {
        public int ID { get; set; }
        [Required]
        [MaxLength(50)]
        [Index(IsUnique = true)]
        public string Name { get; set; }
        [Required]
        [MaxLength(600)]
        public string Description { get; set; }
        [Required]
        [MaxLength(600)]
        public string PersonalityTraits { get; set; }

        public virtual ICollection<Painting> Paintings { get; set; }
    }
}