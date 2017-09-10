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
        protected UserNotification()
        {

        }

        public UserNotification(string userId, Notification notification)
        {
            if (String.IsNullOrEmpty(userId)) new ArgumentException("User Id is not valid");
            this.UserId = userId;
            this.Notification = notification;
        }

        public bool IsRead { get; private set; }

        [Required]
        [ForeignKey("User")]
        public string UserId { get; private set; }
        public ApplicationUser User { get; private set; }

        [Required]
        [ForeignKey("Notification")]
        public int NotificationId { get; private set; }

        public Notification Notification { get; private set; }

        public void Read()
        {
            IsRead = true;
        }

    }
}
