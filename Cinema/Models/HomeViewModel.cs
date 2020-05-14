using System.Collections.Generic;
using CinemaConstructor.Database.Entities;

namespace Cinema.Models
{
    public class HomeViewModel
    {
        public Company Company { get; set; }

        public List<Film> CurrentFilms { get; set; }

        public List<Film> UpcomingFilms { get; set; }
    }
}
