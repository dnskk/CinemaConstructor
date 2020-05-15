using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cinema.Models;
using CinemaConstructor.Database.Repositories;

namespace Cinema.Controllers
{
    public class HomeController : Controller
    {
        private readonly CompanyRepository _companyRepository;
        private readonly FilmRepository _filmRepository;
        private readonly BlobRepository _blobRepository;

        public HomeController(CompanyRepository companyRepository, FilmRepository filmRepository, BlobRepository blobRepository)
        {
            _companyRepository = companyRepository;
            _filmRepository = filmRepository;
            _blobRepository = blobRepository;
        }

        public async Task<IActionResult> Index(CancellationToken token)
        {
            if (!Request.Query.ContainsKey("companyId"))
            {
                return NotFound();
            }

            Request.Query.TryGetValue("companyId", out var companyId);
            var company = await _companyRepository.FindByIdAsync(long.Parse(companyId), token);

            var posters = new Dictionary<long, string>();

            var currentFilms = (await _filmRepository.FindByCompanyIdAsync(company.Id, token))
                .Where(p => p.IsActive && p.ReleaseDate < DateTime.Now).ToList();
            foreach (var film in currentFilms)
            {
                posters[film.Id] = _blobRepository.Get(film.Id);
            }

            var upcomingFilms = (await _filmRepository.FindByCompanyIdAsync(company.Id, token))
                .Where(p => p.IsActive && p.ReleaseDate > DateTime.Now).ToList();
            foreach (var film in upcomingFilms)
            {
                posters[film.Id] = _blobRepository.Get(film.Id);
            }

            var viewModel = new HomeViewModel
            {
                Company = company,
                CurrentFilms = currentFilms,
                UpcomingFilms = upcomingFilms,
                Posters = posters
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
