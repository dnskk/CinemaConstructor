using CinemaConstructor.Database.Entities;

namespace Cinema.Models
{
    public class BookingViewModel
    {
        public Company Company { get; set; }

        public Film Film { get; set; }

        public FilmSession FilmSession { get; set; }

        public  string UnavailableSeats { get; set; }

        public string Poster { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Places { get; set; }
    }
}
