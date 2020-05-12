using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AdminPanel.Models;
using AdminPanel.Models.CinemaViewModels;
using AdminPanel.Models.FilmViewModels;
using AdminPanel.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    public class FilmController : BaseController
    {
        private readonly FilmRepository _filmRepository;

        public FilmController(FilmRepository filmRepository)
        {
            _filmRepository = filmRepository;
        }

        [HttpGet]
        public async Task<IActionResult> All(CancellationToken token)
        {
            AddBreadcrumb("Films", "/Film/All");

            var viewModel = new FilmAllViewModel
            {
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
                    TrailerUrl = model.TrailerUrl
                };

                await _filmRepository.AddAsync(film, token);

                return RedirectToAction(nameof(All), "Film");
            }

            return View(model);
        }
    }
}
