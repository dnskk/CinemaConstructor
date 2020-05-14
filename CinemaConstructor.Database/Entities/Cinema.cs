using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CinemaConstructor.Database.Entities
{
    public class Cinema
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public Company Company { get; set; }

        public List<Hall> Halls { get; set; }
    }
}
