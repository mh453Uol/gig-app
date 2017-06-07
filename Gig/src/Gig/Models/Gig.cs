using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gig.Models
{
    public class Gig
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public int ArtistId { get; set; }
        public ApplicationUser Artist { get; set; }
        public DateTime DateAndTime { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
