using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AdminPanel.Models;
using AdminPanel.Models.HallViewModels;
using AdminPanel.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    public class HallController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly CinemaRepository _cinemaRepository;
        private readonly CompanyRepository _companyRepository;
        private readonly UserSessionRepository _userSessionRepository;

        public HallController(UserManager<ApplicationUser> userManager, CinemaRepository cinemaRepository, CompanyRepository companyRepository, UserSessionRepository userSessionRepository)
        {
            _userManager = userManager;
            _cinemaRepository = cinemaRepository;
            _companyRepository = companyRepository;
            _userSessionRepository = userSessionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> All(CancellationToken token)
        {
            AddBreadcrumb("Halls", "/Hall/All");

            var viewModel = new HallAllViewModel
            {
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create(CancellationToken token)
        {
            AddBreadcrumb("Halls", "/Hall/All");
            AddBreadcrumb("Create", "/Hall/Create");

            var cinemas = await GetCinemas(token);
            var viewModel = new HallCreateViewModel
            {
                Cinemas = cinemas
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFirstStep(HallCreateViewModel model, CancellationToken token, string returnUrl = null)
        {
            AddBreadcrumb("Halls", "/Hall/All");
            AddBreadcrumb("Create", "/Hall/Create");

            model.ActiveTab = 1;
            if (ModelState.IsValid)
            {
                model.ActiveTab = 2;
                return View("Create", model);
            }

            return View("Create", model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSecondStep(HallCreateViewModel model, CancellationToken token, string returnUrl = null)
        {
            AddBreadcrumb("Halls", "/Hall/All");
            AddBreadcrumb("Create", "/Hall/Create");

            model.ActiveTab = 2;

            if (ModelState.IsValid)
            {
                return View("Create", model);
            }

            return View("Create", model);
        }

        private async Task<Company> GetCompany(CancellationToken token)
        {
            var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User)) as IdentityUser;
            var userSession = await _userSessionRepository.FindByUserIdAsync(Guid.Parse(user.Id), token);
            return await _companyRepository.FindByIdAsync(userSession.CurrentCompanyId, token);
        }

        private async Task<List<Cinema>> GetCinemas(CancellationToken token)
        {
            var company = await GetCompany(token);

            var cinemas = await _cinemaRepository.FindByCompanyIdAsync(company.Id, token);
            return cinemas.ToList();
        }
    }
}
