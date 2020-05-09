using System;
using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Models
{
    public class UserSession
    {
        [Key]
        public Guid UserId { get; set; }

        public Guid CurrentCompanyId { get; set; }
    }
}
