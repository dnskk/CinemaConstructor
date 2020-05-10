using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AdminPanel.Models;
using AdminPanel.Models.CinemaViewModels;
using AdminPanel.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    public class CinemaController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly CinemaRepository _cinemaRepository;
        private readonly CompanyRepository _companyRepository;
        private readonly UserSessionRepository _userSessionRepository;

        public CinemaController(UserManager<ApplicationUser> userManager, CinemaRepository cinemaRepository, CompanyRepository companyRepository, UserSessionRepository userSessionRepository)
        {
            _userManager = userManager;
            _cinemaRepository = cinemaRepository;
            _companyRepository = companyRepository;
            _userSessionRepository = userSessionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> All(CancellationToken token)
        {
            AddBreadcrumb("Cinemas", "/Cinema/All");

            var cinemas = await GetCinemas(token);


            var viewModel = new CinemaAllViewModel
            {
                Cinemas = cinemas.OrderBy(p => p.Name).ToList()
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create(CancellationToken token)
        {
            AddBreadcrumb("Cinemas", "/Cinema/All");
            AddBreadcrumb("Create", "/Cinema/Create");

            var viewModel = new CinemaCreateViewModel();

            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Create(CinemaCreateViewModel model, CancellationToken token, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var company = await GetCompany(token);
                var cinema = new Cinema
                {
                    Name = model.Name,
                    Address = model.Address,
                    Phone = model.Phone,
                    Company = company
                };

                await _cinemaRepository.AddAsync(cinema, token);

                return RedirectToAction(nameof(All), "Cinema");
            }

            return View(model);
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
