using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Gig.Models.GigsViewModels
{
    public class MineGigViewModel
    {
        public MineGigViewModel(List<Gig> gigs)
        {
            this.UpcomingGigs = new List<Gig>();
            this.CancelledGigs = new List<Models.Gig>();
            this.PastGigs = new List<Models.Gig>();

            PopulateLists(gigs);
        }
        public ICollection<Models.Gig> UpcomingGigs { get; set; }
        public ICollection<Models.Gig> CancelledGigs { get; set; }
        public ICollection<Models.Gig> PastGigs { get; set; }

        private void PopulateLists(List<Models.Gig> gigs)
        {
            foreach (var gig in gigs)
            {
                if (gig.IsCancelled)
                {
                    CancelledGigs.Add(gig);
                }
                else if (gig.DateAndTime < DateTime.Now)
                {
                    PastGigs.Add(gig);
                }
                else
                {
                    UpcomingGigs.Add(gig);
                }
            }
        }
    }
}
