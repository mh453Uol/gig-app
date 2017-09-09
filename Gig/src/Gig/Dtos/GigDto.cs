using System;

namespace Gig.Dtos
{
    public class GigDto
    {
        public Guid Id { get; set; }
        public UserDto Artist { get; set; }
        public DateTime DateAndTime { get; set; }
        public GenreDto Genre { get; set; }
        public string Venue { get; set; }
        public bool IsCancelled { get; set; }
    }
}