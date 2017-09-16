using Gig.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gig.Models;

namespace Gig.Repositories
{
    public class GigRepository : IGigRepository
    {
        private readonly ApplicationDbContext _db;

        public GigRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Gig.Models.Gig> GetUpcomingGigs(string query = null)
        {
            var upcomingGigs = _db.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => g.DateAndTime > DateTime.Now && g.IsCancelled == false);

            if (!String.IsNullOrWhiteSpace(query))
            {
                upcomingGigs = upcomingGigs
                    .Where(g => g.Artist.FirstName.Contains(query) ||
                           g.Artist.Surname.Contains(query) ||
                           g.Genre.Name.Contains(query) ||
                           g.Venue.Contains(query));

            }

            return upcomingGigs.ToList();
        }

        public async Task<Models.Gig> GetGigWithAttendees(Guid gigId)
        {
            return await _db.Gigs
                .Include(g => g.Attendances)
                    .ThenInclude(g => g.Attendee)
                .SingleOrDefaultAsync(g => g.Id == gigId);
        }
        public async Task<Models.Gig> GetGig(Guid gigId)
        {
            return await _db.Gigs
                .AsNoTracking()
                .SingleAsync(g => g.Id == gigId);
        }

        public async Task<List<Models.Gig>> GetArtistGigsWithGenres(string userId)
        {
            return await _db.Gigs
                .Include(g => g.Genre)
                .Where(g => g.ArtistId == userId)
                .OrderBy(g => g.DateAndTime)
                .ToListAsync();
        }

        public Models.Gig GetGigWithArtist(Guid gigId)
        {
            return _db.Gigs.Include(g => g.Artist)
                 .FirstOrDefault(g => g.Id == gigId);
        }

        public void Add(Models.Gig gig)
        {
            _db.Gigs.Add(gig);
        }
    }
}
