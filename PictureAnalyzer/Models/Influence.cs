using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PictureAnalyzer.Models
{
    public class Influence
    {
        public Influence()
        {
            Paintings = new List<Painting>();
        }

        public int ID { get; set; }

        [Required]
        [MaxLength(100)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        [Required]
        [MaxLength(600)]
        public string Description { get; set; }

        [Display(Name = "Beginning year")]
        public int? BeginYear { get; set; }

        [Display(Name = "Ending year")]
        public int? EndYear { get; set; }

        public virtual ICollection<Painting> Paintings { get; set; }
    }
}