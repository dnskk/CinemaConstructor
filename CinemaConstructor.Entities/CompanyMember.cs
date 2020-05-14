using System.ComponentModel.DataAnnotations;

namespace CinemaConstructor.Entities
{
    public class CompanyMember
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public Company Company { get; set; }

        [Required]
        public MemberRole Role { get; set; }
    }
}
