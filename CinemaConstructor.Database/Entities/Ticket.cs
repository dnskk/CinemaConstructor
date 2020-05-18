using System.ComponentModel.DataAnnotations;

namespace CinemaConstructor.Database.Entities
{
    public class Ticket
    {
        [Key]
        public long Id { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public long Row { get; set; }

        public long Column { get; set; }

        public FilmSession FilmSession { get; set; }
    }
}
