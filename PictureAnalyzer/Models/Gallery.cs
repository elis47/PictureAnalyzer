using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PictureAnalyzer.Models
{
    public class Gallery
    {
        public Gallery()
        {
            Paintings = new List<Painting>();
        }

        public int ID { get; set; }

        [Required]
        [MaxLength(100)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        public virtual ICollection<Painting> Paintings { get; set; }
    }
}