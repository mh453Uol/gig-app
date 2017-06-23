using Gig.Helper.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gig.Models.GigsViewModels
{
    public class GigsFormViewModel
    {
        public GigsFormViewModel()
        {
            Genres = new List<Genre>();
        }
        public Guid Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }

        [Required]
        [FutureDate]
        public string Date { get; set; }

        [Required]
        [Time]
        public string Time { get; set; }

        [Required]
        public byte Genre { get; set; }

        public IEnumerable<Genre> Genres { get; set; }

        public DateTime DateAndTime()
        {
            return DateTime.Parse(Date).Add(TimeSpan.Parse(Time));
        }
    }
}