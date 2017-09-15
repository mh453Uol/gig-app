using Gig.Data;
using Gig.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gig.Persistence
{
    public class UnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public readonly AttendanceRepository Attendance;
        public readonly GigRepository Gig;
        public readonly GenreRepository Genre;
        public readonly ApplicationUserRepository ApplicationUser;
        public readonly FollowingRepository Following;

        public UnitOfWork(ApplicationDbContext _db,
            AttendanceRepository attendanceRepository,
            GigRepository gigRepository,
            GenreRepository genreRepository,
            ApplicationUserRepository applicationUserRepository,
            FollowingRepository followingRepository)
        {
            this._db = _db;
            this.Attendance = attendanceRepository;
            this.Gig = gigRepository;
            this.Genre = genreRepository;
            this.ApplicationUser = applicationUserRepository;
            this.Following = followingRepository;
        }

        public void Complete()
        {
            _db.SaveChanges();
        }

        public async Task<int> CompleteAsync()
        {
            return await _db.SaveChangesAsync();
        }
    }
}
