using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CinemaConstructor.Database.Entities;
using CinemaConstructor.Database.Repositories;
using CinemaConstructor.Models.FilmSessionViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CinemaConstructor.Controllers
{
    public class FilmSessionController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly FilmSessionRepository _filmSessionRepository;
        private readonly FilmRepository _filmRepository;
        private readonly HallRepository _hallRepository;
        private readonly CinemaRepository _cinemaRepository;
        private readonly CompanyRepository _companyRepository;
        private readonly UserSessionRepository _userSessionRepository;

        public FilmSessionController(UserManager<ApplicationUser> userManager, FilmRepository filmRepository,
            HallRepository hallRepository, CinemaRepository cinemaRepository, CompanyRepository companyRepository,
            UserSessionRepository userSessionRepository, FilmSessionRepository filmSessionRepository)
        {
            _userManager = userManager;
            _filmRepository = filmRepository;
            _hallRepository = hallRepository;
            _cinemaRepository = cinemaRepository;
            _companyRepository = companyRepository;
            _userSessionRepository = userSessionRepository;
            _filmSessionRepository = filmSessionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> All(CancellationToken token)
        {
            AddBreadcrumb("Film session", "/FilmSession/All");

            var company = await GetCompany(token);
            var filmSessions = (await _filmSessionRepository.FindByCompanyIdAsync(company.Id, token)).ToList();

            var upcomingSession = filmSessions.Where(p => p.EndTime >= DateTime.Now).OrderByDescending(p => p.StartTime).ToList();
            var pastSession = filmSessions.Where(p => p.EndTime < DateTime.Now).OrderByDescending(p => p.StartTime).ToList();

            var viewModel = new FilmSessionAllViewModel
            {
                UpcomingSessions = upcomingSession,
                PastSessions = pastSession
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create(CancellationToken token)
        {
            AddBreadcrumb("Film session", "/FilmSession/All");
            AddBreadcrumb("Create", "/FilmSession/Create");

            var company = await GetCompany(token);
            var films = await _filmRepository.FindByCompanyIdAsync(company.Id, token);
            var halls = await _hallRepository.FindByCompanyIdAsync(company.Id, token);
            var groupedHalls = halls.GroupBy(p => p.Cinema.Name).OrderBy(p => p.Key);

            var viewModel = new FilmSessionCreateViewModel
            {
                Films = films.Where(p => p.IsActive).ToList(),
                GroupedHalls = groupedHalls.ToList(),
                CompanyId = company.Id
            };

            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Create(FilmSessionCreateViewModel model, CancellationToken token, string returnUrl = null)
        {
            AddBreadcrumb("Film session", "/FilmSession/All");
            AddBreadcrumb("Create", "/FilmSession/Create");
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                var time = TimeSpan.FromSeconds(long.Parse(model.Time));
                var date = DateTime.ParseExact(model.Date, "MM\\/dd\\/yyyy", CultureInfo.InvariantCulture);
                var film = await _filmRepository.FindByIdAsync(long.Parse(model.SelectedFilm), token);
                var hall = await _hallRepository.FindByIdAsync(long.Parse(model.SelectedHall), token);

                var startTime = date.AddHours(time.Hours).AddMinutes(time.Minutes);
                var endTime = startTime.AddHours(film.Duration.Hours).AddMinutes(film.Duration.Minutes);

                var filmSession = new FilmSession
                {
                    Film = film,
                    Hall = hall,
                    StartTime = startTime,
                    EndTime = endTime,
                    Price = long.Parse(model.Price)
                };

                await _filmSessionRepository.AddAsync(filmSession, token);

                return RedirectToAction(nameof(All), "FilmSession");
            }

            var halls = await _hallRepository.FindByCompanyIdAsync(model.CompanyId, token);
            model.GroupedHalls = halls.GroupBy(p => p.Cinema.Name).OrderBy(p => p.Key).ToList();

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
