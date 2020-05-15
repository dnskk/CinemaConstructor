using System.Collections.Generic;
using CinemaConstructor.Database.Entities;

namespace Cinema.Models
{
    public class FilmViewModel
    {
        public Company Company { get; set; }

        public Film Film { get; set; }

        public string Poster { get; set; }

        public List<Film> UpcomingFilms { get; set; }

        public Dictionary<long, string> Posters { get; set; }
    }
}
