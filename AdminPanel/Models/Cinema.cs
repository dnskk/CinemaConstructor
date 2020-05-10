using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Models
{
    public class Cinema
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public Company Company { get; set; }
    }
}
