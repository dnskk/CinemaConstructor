using System.Collections.Generic;
using CinemaConstructor.Database.Entities;
using Microsoft.AspNetCore.Identity;

namespace CinemaConstructor.Models.RoleViewModels
{
    public class EditRoleVm
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<ApplicationUser> Members { get; set; }
        public IEnumerable<ApplicationUser> NonMembers { get; set; }
    }
}
