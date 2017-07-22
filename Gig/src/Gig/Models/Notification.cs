using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Gig.Models
{
    public class Notification
    {
        public Notification(Guid gigId, NotificationType type)
        {
            if (GigId == Guid.Empty) new ArgumentException("Empty Gig Id");
            this.GigId = gigId;
            this.Type = type;
            DateTime = DateTime.Now;
        }

        protected Notification()
        {
            Type = NotificationType.GigCreated;
        }

        [Key]
        public int Id { get; private set; }

        [Required]
        public DateTime DateTime { get; private set; }

        public NotificationType Type { get; private set; }
        public DateTime? OriginalDateTime { get; set; }
        public string OriginalVenue { get; set; }

        [ForeignKey("Gig")]
        [Required]
        public Guid GigId { get; private set; }

        public Gig Gig { get; private set; }

        public override string ToString()
        {
            var message = "";
            switch (Type)
            {
                case NotificationType.GigCancelled:
                    message = String.Format(@"{0} has cancelled
                    the gig at {1} on {2} at {3}",
                    Gig.Artist.FullName,
                    Gig.Venue,
                    Gig.DateAndTime.ToString("dd MMM yyyy"),
                    Gig.DateAndTime.ToString("HH tt"));
                    break;
                case NotificationType.GigUpdated:
                    message = String.Format(@"{0} has changed 
                    the date of the gig at {1} from {2} to {3}",
                    Gig.Artist.FullName,
                    Gig.Venue,
                    Gig.DateAndTime.ToString("dd MMM yyyy hh tt"),
                    DateTime.ToString("dd MMM yyyy hh tt"));
                    break;
                default:
                    message = String.Format("{0} is performing at {1} at {2}",
                        Gig.Artist.FullName,
                        Gig.Venue,
                        Gig.DateAndTime.ToString("dd MMM yyyy"));
                    break;
            }
            return message;
        }
    }
}

