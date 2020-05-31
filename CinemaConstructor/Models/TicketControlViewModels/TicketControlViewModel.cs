using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CinemaConstructor.Database.Entities;

namespace CinemaConstructor.Models.TicketControlViewModels
{
    public class TicketControlViewModel
    {
        [Required]
        [Display(Name = "Confirmation code")]
        public string ConfirmationCode { get; set; }

        public List<Ticket> Tickets { get; set; }

        public FilmSession FilmSession { get; set; }

        public bool IsChecked { get; set; }
    }
}
