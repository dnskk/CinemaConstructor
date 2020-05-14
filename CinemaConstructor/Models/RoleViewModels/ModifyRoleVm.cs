using System.ComponentModel.DataAnnotations;

namespace CinemaConstructor.Models.RoleViewModels
{
    public class ModifyRoleVm
    {
        [Required]
        public string RoleName { get; set; }

        public string RoleId { get; set; }
        public string[] IdsToAdd { get; set; }
        public string[] IdsToRemove { get; set; }

    }
}
