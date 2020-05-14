using System;
using System.ComponentModel.DataAnnotations;

namespace CinemaConstructor.Entities
{
    public class Film
    {
        [Key]
        public long Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public TimeSpan Duration { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string Genre { get; set; }

        public string TrailerUrl { get; set; }

        public Company Company { get; set; }
    }
}
