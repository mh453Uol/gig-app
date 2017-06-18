using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gig.Models
{
    public class Attendance
    {

        [Required]
        public Guid GigId { get; set; }

        [ForeignKey("GigId")]
        public Models.Gig Gig { get; set; }

        [Required]
        public string AttendeeId { get; set; }

        [ForeignKey("AttendeeId")]
        public ApplicationUser Attendee { get; set; }
    }
}
