using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PictureAnalyzer.Models
{
    public class Painter
    {
        public Painter()
        {
            Paintings = new List<Painting>();
        }

        public int ID { get; set; }

        [Required]
        [MaxLength(100)]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        [MaxLength(600)]
        public string Description { get; set; }

        [Required]
        [MaxLength(100)]
        public string Country { get; set; }

        public string Link { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "DateTime2")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name ="Date of birth")]
        public DateTime? BirthDate { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "DateTime2")]
        [Display(Name ="Date of decease")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? PassDate { get; set; }

        public virtual ICollection<Painting> Paintings { get; set; }
    }
}