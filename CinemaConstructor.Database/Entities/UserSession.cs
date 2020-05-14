using System;
using System.ComponentModel.DataAnnotations;

namespace CinemaConstructor.Database.Entities
{
    public class UserSession
    {
        [Key]
        public Guid UserId { get; set; }

        public long CurrentCompanyId { get; set; }

        public long CurrentCinemaId { get; set; }
    }
}
