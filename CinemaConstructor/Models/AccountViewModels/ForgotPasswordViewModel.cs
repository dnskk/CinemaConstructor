using System.ComponentModel.DataAnnotations;

namespace CinemaConstructor.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
