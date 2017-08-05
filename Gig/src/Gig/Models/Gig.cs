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
            Notifications = new Collection<Notification>();
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
        public ICollection<Notification> Notifications { get; set; }

        public void Created()
        {
            var notification = Notification.GigCreated(Id);

            Notifications.Add(notification);

            Artist.Followees.Where(f => f.IsDeleted = false)
                .Select(f => f.Follower).ToList()
                .ForEach(f => f.Notify(notification));
        }

        public void Cancelled()
        {
            IsCancelled = true;

            var notification = Notification.GigCancelled(Id);

            Notifications.Add(notification);

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

        public void Updated(DateTime originalDateTime, string originalVenue)
        {
            var notification = Notification.GigUpdated(Id, originalDateTime, originalVenue);

            Notifications.Add(notification);

            Attendances.Select(f => f.Attendee).ToList().
                ForEach(a => a.Notify(notification));
        }
    }
}
