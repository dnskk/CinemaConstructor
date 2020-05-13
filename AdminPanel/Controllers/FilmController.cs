using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AdminPanel.Models;
using AdminPanel.Models.FilmViewModels;
using AdminPanel.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    public class FilmController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly CompanyRepository _companyRepository;
        private readonly UserSessionRepository _userSessionRepository;
        private readonly FilmRepository _filmRepository;

        public FilmController(FilmRepository filmRepository, UserManager<ApplicationUser> userManager,
            CompanyRepository companyRepository, UserSessionRepository userSessionRepository)
        {
            _filmRepository = filmRepository;
            _userManager = userManager;
            _companyRepository = companyRepository;
            _userSessionRepository = userSessionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> All(CancellationToken token)
        {
            AddBreadcrumb("Films", "/Film/All");

            var company = await GetCompany(token);
            var films = await _filmRepository.FindByCompanyIdAsync(company.Id, token);
            var viewModel = new FilmAllViewModel
            {
                Films = films.ToList()
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create(CancellationToken token)
        {
            AddBreadcrumb("Films", "/Film/All");
            AddBreadcrumb("Create", "/Film/Create");

            var viewModel = new FilmCreateViewModel
            {
                ReleaseDate = DateTime.Now.ToString("MM\\/dd\\/yyyy")
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(FilmCreateViewModel model, CancellationToken token, string returnUrl = null)
        {
            AddBreadcrumb("Films", "/Film/All");
            AddBreadcrumb("Create", "/Film/Create");
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                var film = new Film
                {
                    Title = model.Title,
                    Description = model.Description,
                    Genre = model.Genre,
                    ReleaseDate = DateTime.ParseExact(model.ReleaseDate, "MM\\/dd\\/yyyy", CultureInfo.InvariantCulture),
                    TrailerUrl = model.TrailerUrl,
                    Company = await GetCompany(token)
                };

                await _filmRepository.AddAsync(film, token);

                return RedirectToAction(nameof(All), "Film");
            }

            return View(model);
        }

        private async Task<Company> GetCompany(CancellationToken token)
        {
            var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User)) as IdentityUser;
            var userSession = await _userSessionRepository.FindByUserIdAsync(Guid.Parse(user.Id), token);
            return await _companyRepository.FindByIdAsync(userSession.CurrentCompanyId, token);
        }
    }
}
