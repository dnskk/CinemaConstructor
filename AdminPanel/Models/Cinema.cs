using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Models
{
    public class Cinema
    {
        [Key]
        public int Id { get; set; }

        public CinemaCompany CinemaCompany { get; set; }
    }
}
