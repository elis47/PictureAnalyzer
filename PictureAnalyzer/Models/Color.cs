using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PictureAnalyzer.Models
{
    public class Color
    {
        public Color()
        {
            Paintings = new HashSet<Painting>();
        }

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

        [Required]
        [MaxLength(600)]
        public string Keywords { get; set; }

        public virtual ICollection<Painting> Paintings { get; set; }
    }
}