using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CinemaConstructor.Database.Entities;
using CinemaConstructor.Database.Repositories;
using CinemaConstructor.Models.FilmViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CinemaConstructor.Controllers
{
    public class FilmController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly CompanyRepository _companyRepository;
        private readonly UserSessionRepository _userSessionRepository;
        private readonly FilmRepository _filmRepository;
        private readonly BlobRepository _blobRepository;

        public FilmController(FilmRepository filmRepository, UserManager<ApplicationUser> userManager,
            CompanyRepository companyRepository, UserSessionRepository userSessionRepository, BlobRepository blobRepository)
        {
            _filmRepository = filmRepository;
            _userManager = userManager;
            _companyRepository = companyRepository;
            _userSessionRepository = userSessionRepository;
            _blobRepository = blobRepository;
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
                    Duration = TimeSpan.FromSeconds(long.Parse(model.Duration)),
                    ReleaseDate = DateTime.ParseExact(model.ReleaseDate, "MM\\/dd\\/yyyy", CultureInfo.InvariantCulture),
                    TrailerUrl = model.TrailerUrl,
                    IsActive = true,
                    Company = await GetCompany(token)
                };

                var addedFilm = await _filmRepository.AddAsync(film, token);

                await _blobRepository.Upload(addedFilm.Id, model.PosterImage);

                return RedirectToAction(nameof(All), "Film");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> SelectFilm(CancellationToken token)
        {
            if (!Request.Query.ContainsKey("filmId"))
            {
                return await All(token);
            }

            Request.Query.TryGetValue("filmId", out var filmId);

            var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User)) as IdentityUser;
            var userSession = await _userSessionRepository.FindByUserIdAsync(Guid.Parse(user.Id), token);
            userSession.CurrentFilmId = long.Parse(filmId);
            await _userSessionRepository.UpdateAsync(userSession, CancellationToken.None);

            return RedirectToAction(nameof(Edit), "Film");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(CancellationToken token)
        {
            AddBreadcrumb("Films", "/Film/All");
            AddBreadcrumb("Edit", "/Film/Edit");

            var film = await GetFilm(token);

            var viewModel = new FilmEditViewModel
            {
                Title = film.Title,
                Description = film.Description,
                ReleaseDate = film.ReleaseDate.ToString("MM\\/dd\\/yyyy"),
                Duration = ((long)film.Duration.TotalSeconds).ToString(),
                Genre = film.Genre,
                TrailerUrl = film.TrailerUrl,
                IsActive = film.IsActive
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(FilmEditViewModel model, CancellationToken token, string returnUrl = null)
        {
            AddBreadcrumb("Films", "/Film/All");
            AddBreadcrumb("Edit", "/Film/Edit");
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                var film = await GetFilm(token);
                film.Title = model.Title;
                film.Description = model.Description;
                film.Genre = model.Genre;
                film.Duration = TimeSpan.FromSeconds(long.Parse(model.Duration));
                film.ReleaseDate = DateTime.ParseExact(model.ReleaseDate, "MM\\/dd\\/yyyy", CultureInfo.InvariantCulture);
                film.TrailerUrl = model.TrailerUrl;
                film.IsActive = model.IsActive;

                await _filmRepository.UpdateAsync(film, token);

                return RedirectToAction(nameof(All), "Film");
            }

            return View(model);
        }

        private async Task<Film> GetFilm(CancellationToken token)
        {
            var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User)) as IdentityUser;
            var userSession = await _userSessionRepository.FindByUserIdAsync(Guid.Parse(user.Id), token);
            return await _filmRepository.FindByIdAsync(userSession.CurrentCompanyId, token);
        }

        private async Task<Company> GetCompany(CancellationToken token)
        {
            var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User)) as IdentityUser;
            var userSession = await _userSessionRepository.FindByUserIdAsync(Guid.Parse(user.Id), token);
            return await _companyRepository.FindByIdAsync(userSession.CurrentCompanyId, token);
        }
    }
}
