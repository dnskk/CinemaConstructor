using System.ComponentModel.DataAnnotations;

namespace CinemaConstructor.Models.CompanyViewModels
{
    public class EditViewModel
    {
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Instagram link")]
        public string InstagramLink { get; set; }

        [Display(Name = "Facebook link")]
        public string FacebookLink { get; set; }

        [Display(Name = "Yandex wallet")]
        public string YandexWallet { get; set; }
    }
}
