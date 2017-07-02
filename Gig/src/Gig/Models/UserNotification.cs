using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gig.Models
{
    public class UserNotification
    {
        public UserNotification(string userId, int notificationId)
        {
            if (String.IsNullOrEmpty(userId)) new ArgumentException("User Id is not valid");
            this.UserId = userId;
            this.NotificationId = notificationId;
        }

        public bool IsRead { get; set; }

        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [Required]
        [ForeignKey("Notification")]
        public int NotificationId { get; set; }

        public Notification Notification { get; set; }

    }
}
