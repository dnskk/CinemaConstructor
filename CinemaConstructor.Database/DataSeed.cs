using System;
using System.Security.Claims;
using System.Threading.Tasks;
using CinemaConstructor.Common;
using CinemaConstructor.Database.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CinemaConstructor.Database
{
    public static class DataSeed
    {
        public static string Administrators = "Administrators";
        public static string Cashiers = "Cashiers";
        public static string TicketControllers = "TicketControllers";

        public static async Task Seed(IServiceProvider serviceProvider)
        {
            IServiceScopeFactory scopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();

            using (IServiceScope scope = scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                context.Database.Migrate();

                UserManager<ApplicationUser> _userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                // Seed database code goes here

                // User Info
                //string userName = "SuperAdmin";
                string firstName = "Super";
                string lastName = "Admin";
                string email = "superadmin@admin.com";
                string password = "Qwaszx123$";

                if (await _userManager.FindByNameAsync(email) == null)
                {
                    // Create SuperAdmins role if it doesn't exist
                    if (await roleManager.FindByNameAsync(Administrators) == null)
                    {
                        await roleManager.CreateAsync(new IdentityRole(Administrators));
                    }
                    if (await roleManager.FindByNameAsync(Cashiers) == null)
                    {
                        await roleManager.CreateAsync(new IdentityRole(Cashiers));
                    }
                    if (await roleManager.FindByNameAsync(TicketControllers) == null)
                    {
                        await roleManager.CreateAsync(new IdentityRole(TicketControllers));
                    }

                    // Create user account if it doesn't exist
                    ApplicationUser user = new ApplicationUser
                    {
                        UserName = email,
                        Email = email,
                        //extended properties
                        FirstName = firstName,
                        LastName = lastName,
                        AvatarURL = "/images/user.png",
                        DateRegistered = DateTime.UtcNow.ToString(),
                        Position = "",
                        NickName = "",
                    };

                    IdentityResult result = await _userManager.CreateAsync(user, password);

                    // Assign role to the user
                    if (result.Succeeded)
                    {
                        await _userManager.AddClaimAsync(user, new Claim(CustomClaimTypes.GivenName, user.FirstName));
                        await _userManager.AddClaimAsync(user, new Claim(CustomClaimTypes.Surname, user.LastName));
                        await _userManager.AddClaimAsync(user, new Claim(CustomClaimTypes.AvatarURL, user.AvatarURL));

                        //SignInManager<ApplicationUser> _signInManager = serviceProvider.GetRequiredService<SignInManager<ApplicationUser>>();
                        //await _signInManager.SignInAsync(user, isPersistent: false);

                        await _userManager.AddToRoleAsync(user, Administrators);
                        await _userManager.AddToRoleAsync(user, Cashiers);
                        await _userManager.AddToRoleAsync(user, TicketControllers);
                    }
                }
            }
        }
    }
}
