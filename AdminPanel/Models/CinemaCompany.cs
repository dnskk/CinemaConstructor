using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Models
{
    public class CinemaCompany
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Cinema> Cinemas { get; set; }
    }
}
