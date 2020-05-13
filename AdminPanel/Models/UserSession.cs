using System;
using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Models
{
    public class UserSession
    {
        [Key]
        public Guid UserId { get; set; }

        public long CurrentCompanyId { get; set; }

        public long CurrentCinemaId { get; set; }
    }
}
