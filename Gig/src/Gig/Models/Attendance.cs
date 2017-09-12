using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gig.Models
{
    public class Attendance
    {
        public Attendance()
        {

        }

        public Attendance(string userId, Guid gigId)
        {
            AttendeeId = userId;
            GigId = gigId;
        }

        [Required]
        public Guid GigId { get; set; }

        [ForeignKey("GigId")]
        public Models.Gig Gig { get; set; }

        [Required]
        public string AttendeeId { get; set; }

        [ForeignKey("AttendeeId")]
        public ApplicationUser Attendee { get; set; }

        public bool IsCancelled { get; set; }

        public void Cancel()
        {
            IsCancelled = true;
        }

        public void Attend()
        {
            IsCancelled = false;
        }
    }
}
