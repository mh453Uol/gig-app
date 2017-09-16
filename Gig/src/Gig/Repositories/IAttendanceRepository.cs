using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gig.Models;

namespace Gig.Repositories
{
    public interface IAttendanceRepository
    {
        void Add(Attendance attendance);
        Attendance GetAttendance(string userId, Guid gigId, bool IsCancelled = false);
        IEnumerable<Attendance> GetGigsUserAttending(IEnumerable<Models.Gig> gigs, string userId);
        Task<List<Attendance>> GetGigsUserAttending(string userId);
    }
}