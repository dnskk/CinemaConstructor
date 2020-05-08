using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Models
{
    public class Cinema
    {
        [Key]
        public int Id { get; set; }

        public Company Company { get; set; }
    }
}
