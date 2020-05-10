using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Models.CinemaViewModels
{
    public class CinemaEditViewModel
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
