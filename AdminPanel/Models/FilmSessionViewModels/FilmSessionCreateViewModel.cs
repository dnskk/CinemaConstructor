using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AdminPanel.Models.FilmSessionViewModels
{
    public class FilmSessionCreateViewModel
    {
        [Required]
        [Display(Name = "Film")]
        public string SelectedFilm { get; set; }

        [Required]
        [Display(Name = "Hall")]
        public string SelectedHall { get; set; }

        [Required]
        [Display(Name = "Date")]
        public string Date { get; set; }

        [Required]
        [Display(Name = "Time")]
        public string Time { get; set; }

        [Required]
        [Display(Name = "Ticker price")]
        public string Price { get; set; }

        public List<Film> Films { get; set; }

        public List<IGrouping<string, Hall>> GroupedHalls { get; set; }

        public long CompanyId { get; set; }
    }
}
