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
        public int Id { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        public NotificationType Type { get; set; }
        public DateTime? OriginalDateTime { get; set; }
        public string OriginalVenue { get; set; }

        [ForeignKey("Gig")]
        [Required]
        public Guid GigId { get; set; }

        public Gig Gig { get; set; }
    }
}
