using System.Collections.Generic;
using CinemaConstructor.Database.Entities;

namespace CinemaConstructor.Models.FilmSessionViewModels
{
    public class FilmSessionAllViewModel
    {
        public List<FilmSession> UpcomingSessions { get; set; }

        public List<FilmSession> PastSessions { get; set; }
    }
}
