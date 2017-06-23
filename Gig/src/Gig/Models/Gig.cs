using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gig.Models
{
    public class Gig
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [ForeignKey("Artist")]
        [Required]
        public string ArtistId { get; set; }

        public ApplicationUser Artist { get; set; }
        public DateTime DateAndTime { get; set; }

        [ForeignKey("Genre")]
        [Required]
        public byte GenreId { get; set; }

        public Genre Genre { get; set; }

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }

        public bool IsCancelled { get; set; }
    }
}
