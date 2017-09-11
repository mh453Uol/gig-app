using Gig.Models.CommonViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gig.Models.GigsViewModels
{
    public class GigDetailViewModel : IsAuthenticatedViewModel
    {
        public GigDetailViewModel()
        {
            IsAttending = false;
            IsFollowingArtist = false;
            IsAuthenticated = false;
        }
        public Gig Gig { get; set; }
        public bool IsAttending { get; set; }
        public bool IsFollowingArtist { get; set; }

    }
}
