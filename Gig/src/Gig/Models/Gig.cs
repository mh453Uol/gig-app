using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Gig.Models
{
    public class Gig
    {
        public Gig()
        {
            Attendances = new Collection<Attendance>();
            Notification = new Collection<Notification>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [ForeignKey("Artist")]
        [Required]
        public string ArtistId { get; set; }

        public ApplicationUser Artist { get; set; }
        public DateTime DateAndTime { get; set; }

        [ForeignKey("Genre")]
        [Required]
        public byte GenreId { get; set; }

        public Genre Genre { get; set; }

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }

        public bool IsCancelled { get; private set; }
        public ICollection<Attendance> Attendances { get; set; }
        public ICollection<Notification> Notification { get; set; }

        public void Cancel()
        {
            IsCancelled = true;

            var notification = new Notification(Id, NotificationType.GigCancelled);

            Notification.Add(notification);

            Attendances.Select(f => f.Attendee).ToList()
                .ForEach(a => a.Notify(notification));
        }

        public bool CantCancel()
        {
            if (IsCancelled || DateAndTime < DateTime.Now)
            {
                return true;
            }
            return false;
        }

        public void Updated()
        {
            var notification = new Notification(Id, NotificationType.GigUpdated);

            Notification.Add(notification);

            Attendances.Select(f => f.Attendee).ToList().
                ForEach(a => a.Notify(notification));
        }
    }
}
