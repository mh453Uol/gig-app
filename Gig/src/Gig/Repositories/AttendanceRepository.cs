using Gig.Data;
using Gig.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gig.Repositories
{
    public class AttendanceRepository
    {
        private readonly ApplicationDbContext _db;

        public AttendanceRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<Attendance>> GetGigsUserAttending(string userId)
        {
            return await _db.Attendances
                .Where(g => g.AttendeeId == userId && !g.IsCancelled)
                .Include(g => g.Gig)
                    .ThenInclude(g => g.Genre)
                .Include(g => g.Gig)
                    .ThenInclude(g => g.Artist)
                .AsNoTracking()
                .ToListAsync();
        }

        public IEnumerable<Attendance> GetGigsUserAttending(IEnumerable<Gig.Models.Gig> gigs, string userId)
        {
            var gigsIdToCheckAttendace = gigs.Select(g => g.Id);

            return _db.Attendances.Where(a => gigsIdToCheckAttendace.Contains(a.GigId) &&
                        a.AttendeeId == userId &&
                        !a.IsCancelled)
                        .AsNoTracking();
        }

        public IEnumerable<Attendance> GetAttendance(string userId, Guid gigId)
        {
            return _db.Attendances.Where(a => a.AttendeeId == userId &&
                        a.GigId == gigId && a.IsCancelled == false)
                        .AsNoTracking()
                        .ToList();
        }
    }
}
