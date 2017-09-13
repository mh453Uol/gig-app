using Gig.Models.CommonViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gig.Models.GigsViewModels
{
    public class GigsViewModel : IsAuthenticatedViewModel
    {
        public GigsViewModel()
        {
            UpcomingGigs = new Collection<Gig>();
        }
        public IEnumerable<Models.Gig> UpcomingGigs { get; set; }
        public string Heading { get; set; }

        [Required]
        [Display(Name = "Search Term")]
        [StringLength(60, ErrorMessage = "The {0} must be at least {2} characters long", MinimumLength = 1)]
        public string SearchTerm { get; set; }
        public ILookup<Guid, Attendance> Attending { get; internal set; }
        public ILookup<string, Following> Following { get; internal set; }
    }
}
