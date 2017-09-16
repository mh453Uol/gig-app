using Gig.Data;
using Gig.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gig.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public IAttendanceRepository Attendance { get; private set; }
        public IGigRepository Gig { get; private set; }
        public IGenreRepository Genre { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }
        public IFollowingRepository Following { get; private set; }
        public INotificationRepository Notification { get; private set; }

        public UnitOfWork(ApplicationDbContext _db,
            IAttendanceRepository attendanceRepository,
            IGigRepository gigRepository,
            IGenreRepository genreRepository,
            IApplicationUserRepository applicationUserRepository,
            IFollowingRepository followingRepository,
            INotificationRepository notificationRepository)
        {
            this._db = _db;
            this.Attendance = attendanceRepository;
            this.Gig = gigRepository;
            this.Genre = genreRepository;
            this.ApplicationUser = applicationUserRepository;
            this.Following = followingRepository;
            this.Notification = notificationRepository;
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
