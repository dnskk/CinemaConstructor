using System.Collections.Generic;

namespace AdminPanel.Models.FilmSessionViewModels
{
    public class FilmSessionAllViewModel
    {
        public List<FilmSession> UpcomingSessions { get; set; }

        public List<FilmSession> PastSessions { get; set; }
    }
}
