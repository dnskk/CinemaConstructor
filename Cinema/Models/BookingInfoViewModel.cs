using CinemaConstructor.Database.Entities;

namespace Cinema.Models
{
    public class BookingInfoViewModel
    {
        public Company Company { get; set; }

        public Film Film { get; set; }

        public FilmSession FilmSession { get; set; }

        public string Poster { get; set; }
    }
}
