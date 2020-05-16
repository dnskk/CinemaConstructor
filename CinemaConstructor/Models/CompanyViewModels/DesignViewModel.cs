using System.ComponentModel.DataAnnotations;

namespace CinemaConstructor.Models.CompanyViewModels
{
    public class DesignViewModel
    {
        [Required]
        [Display(Name = "First accent color")]
        public string AccentColorFirst { get; set; }

        [Required]
        [Display(Name = "Second accent color")]
        public string AccentColorSecond { get; set; }
    }
}
