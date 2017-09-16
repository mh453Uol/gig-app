using Gig.Data;
using Gig.Models;
using Gig.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gig.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationDbContext _db;
        public NotificationRepository(ApplicationDbContext db)
        {
            this._db = db;
        }

        public List<UserNotification> GetUnreadNotifications(string userId)
        {
            return _db.UserNotification
                .Where(n => n.UserId == userId && !n.IsRead)
                .ToList();
        }

        public IEnumerable<UserNotification> GetUnreadNotificationsWithArtist(string userId)
        {
            return _db.UserNotification
                 .Where(u => u.UserId == userId && u.IsRead == false)
                 .Include(u => u.Notification)
                     .ThenInclude(u => u.Gig.Artist)
                 .AsNoTracking()
                 .ToList();
        }
    }
}
