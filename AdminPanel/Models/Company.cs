using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Models
{
    public class Company
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string InstagramLink { get; set; }

        public string FacebookLink { get; set; }

        public List<Cinema> Cinemas { get; set; }
    }
}
