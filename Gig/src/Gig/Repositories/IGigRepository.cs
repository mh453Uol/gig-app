using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gig.Models;

namespace Gig.Repositories
{
    public interface IGigRepository
    {
        void Add(Models.Gig gig);
        Task<List<Models.Gig>> GetArtistGigsWithGenres(string userId);
        Task<Models.Gig> GetGig(Guid gigId);
        Models.Gig GetGigWithArtist(Guid gigId);
        Task<Models.Gig> GetGigWithAttendees(Guid gigId);
        IEnumerable<Models.Gig> GetUpcomingGigs(string query = null);
    }
}