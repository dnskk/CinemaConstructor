using System.ComponentModel.DataAnnotations;

namespace CinemaConstructor.Models.CinemaViewModels
{
    public class CinemaCreateViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Phone number")]
        public string Phone { get; set; }
    }
}
