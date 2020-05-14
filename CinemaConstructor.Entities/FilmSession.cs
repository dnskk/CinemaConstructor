using System;
using System.ComponentModel.DataAnnotations;

namespace CinemaConstructor.Entities
{
    public class FilmSession
    {
        [Key]
        public long Id { get; set; }

        public Film Film { get; set; }

        public Hall Hall { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public long Price { get; set; }
    }
}
