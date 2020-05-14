using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CinemaConstructor.Common;
using CinemaConstructor.Models;
using CinemaConstructor.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CinemaConstructor.ViewComponents
{
    public class SidebarViewComponent : ViewComponent
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly CompanyRepository _companyRepository;
        private readonly UserSessionRepository _userSessionRepository;

        public SidebarViewComponent(UserManager<ApplicationUser> userManager, CompanyRepository companyRepository,
            UserSessionRepository userSessionRepository)
        {
            _userManager = userManager;
            _companyRepository = companyRepository;
            _userSessionRepository = userSessionRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //you can do the access rights checking here by using session, user, and/or filter parameter
            var sidebars = new List<SidebarMenu>();

            //if (((ClaimsPrincipal)User).GetUserProperty("AccessProfile").Contains("VES_008, Payroll"))
            //{
            //}

            var user = await _userManager.FindByIdAsync(_userManager.GetUserId(UserClaimsPrincipal)) as IdentityUser;
            var userSession = await _userSessionRepository.FindByUserIdAsync(Guid.Parse(user.Id), CancellationToken.None);
            var company = await _companyRepository.FindByIdAsync(userSession.CurrentCompanyId, CancellationToken.None);

            sidebars.Add(ModuleHelper.AddTree(company.Name));
            sidebars.Last().TreeChild = new List<SidebarMenu>
            {
                ModuleHelper.AddModule(ModuleHelper.Module.Info),
                ModuleHelper.AddModule(ModuleHelper.Module.EditInfo),
                ModuleHelper.AddModule(ModuleHelper.Module.EditDesign)
            };

            sidebars.Add(ModuleHelper.AddTree("Cinemas"));
            sidebars.Last().TreeChild = new List<SidebarMenu>
            {
                ModuleHelper.AddModule(ModuleHelper.Module.CinemasManagement),
                ModuleHelper.AddModule(ModuleHelper.Module.CinemaCreate)
            };

            sidebars.Add(ModuleHelper.AddTree("Halls"));
            sidebars.Last().TreeChild = new List<SidebarMenu>
            {
                ModuleHelper.AddModule(ModuleHelper.Module.HallsManagement),
                ModuleHelper.AddModule(ModuleHelper.Module.HallCreate)
            };

            sidebars.Add(ModuleHelper.AddTree("Films"));
            sidebars.Last().TreeChild = new List<SidebarMenu>
            {
                ModuleHelper.AddModule(ModuleHelper.Module.FilmsManagement),
                ModuleHelper.AddModule(ModuleHelper.Module.FilmCreate)
            };

            sidebars.Add(ModuleHelper.AddTree("Film sessions"));
            sidebars.Last().TreeChild = new List<SidebarMenu>
            {
                ModuleHelper.AddModule(ModuleHelper.Module.FilmSessionsManagement),
                ModuleHelper.AddModule(ModuleHelper.Module.FilmSessionCreate)
            };

            sidebars.Add(ModuleHelper.AddModule(ModuleHelper.Module.About, Tuple.Create(1, 0, 0)));
            sidebars.Add(ModuleHelper.AddTree("Account"));
            sidebars.Last().TreeChild = new List<SidebarMenu>
            {
                ModuleHelper.AddModule(ModuleHelper.Module.Register, Tuple.Create(1, 1, 1)),
            };

            if (User.IsInRole("SuperAdmins"))
            {
                sidebars.Add(ModuleHelper.AddTree("Administration"));
                sidebars.Last().TreeChild = new List<SidebarMenu>
                {
                    ModuleHelper.AddModule(ModuleHelper.Module.SuperAdmin),
                    ModuleHelper.AddModule(ModuleHelper.Module.Role),
                };
                sidebars.Add(ModuleHelper.AddModule(ModuleHelper.Module.UserLogs));
            }

            return View(sidebars);
        }
    }
}
