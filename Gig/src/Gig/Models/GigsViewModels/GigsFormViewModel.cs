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

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }

        [Required]
        public string Date { get; set; }

        [Required]
        public string Time { get; set; }

        [Required]
        public byte Genre { get; set; }

        public IEnumerable<Genre> Genres { get; set; }

        public DateTime DateAndTime {
            get
            {
                return DateTime.Parse(Date).Add(TimeSpan.Parse(Time));
            }
        }
    }
}