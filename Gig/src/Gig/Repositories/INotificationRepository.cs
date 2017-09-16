using System.Collections.Generic;
using Gig.Models;

namespace Gig.Repositories
{
    public interface INotificationRepository
    {
        IEnumerable<UserNotification> GetUnreadNotificationsWithArtist(string userId);
        List<UserNotification> GetUnreadNotifications(string userId);

    }
}