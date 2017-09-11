using System;
using System.ComponentModel.DataAnnotations;

namespace Gig.Dtos
{
    public class AttendanceDto
    {
        [Required]
        public Guid GigId { get; set; }
    }
}
