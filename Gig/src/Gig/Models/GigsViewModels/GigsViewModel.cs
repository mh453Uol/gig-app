using Gig.Models.CommonViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Gig.Models.GigsViewModels
{
    public class GigsViewModel:IsAuthenticatedViewModel
    {
        public GigsViewModel()
        {
            UpcomingGigs = new Collection<Gig>();
        }
        public IEnumerable<Models.Gig> UpcomingGigs { get; set; }
    }
}
