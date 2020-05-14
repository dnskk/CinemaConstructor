using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CinemaConstructor.Entities;

namespace CinemaConstructor.Models.HallViewModels
{
    public class HallCreateViewModel
    {
        [Required]
        [Display(Name = "Hall name")]
        public string Name { get; set; }

        public bool Is3D { get; set; }

        public bool IsIMAX { get; set; }

        [Display(Name = "Cinema")]
        public string SelectedCinema { get; set; }

        public int ActiveTab { get; set; } = 1;

        public int Rows { get; set; } = 10;

        public int Columns { get; set; } = 20;

        public string HallTableJson { get; set; }

        public List<Cinema> Cinemas { get; set; }
    }
}
