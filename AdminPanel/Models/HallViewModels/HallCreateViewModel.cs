﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Models.HallViewModels
{
    public class HallCreateViewModel
    {
        [Required]
        [Display(Name = "Hall name")]
        public string Name { get; set; }

        public bool Is3D { get; set; }

        public bool IsIMAX { get; set; }

        public string SelectedCinema { get; set; }

        public int ActiveTab { get; set; } = 1;

        public List<Cinema> Cinemas { get; set; }
    }
}
