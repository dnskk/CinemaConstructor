﻿using System.ComponentModel.DataAnnotations;

namespace CinemaConstructor.Models.FilmViewModels
{
    public class FilmEditViewModel
    {
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Release date")]
        public string ReleaseDate { get; set; }

        [Required]
        [Display(Name = "Duration")]
        public string Duration { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public string Genre { get; set; }

        [Display(Name = "Trailer URI")]
        public string TrailerUrl { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }
    }
}
