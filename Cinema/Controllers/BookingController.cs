using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cinema.Models;
using CinemaConstructor.Database.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers
{
    public class BookingController : Controller
    {
        private readonly CompanyRepository _companyRepository;
        private readonly FilmSessionRepository _filmSessionRepository;
        private readonly BlobRepository _blobRepository;

        public BookingController(CompanyRepository companyRepository, BlobRepository blobRepository,
            FilmSessionRepository filmSessionRepository)
        {
            _companyRepository = companyRepository;
            _blobRepository = blobRepository;
            _filmSessionRepository = filmSessionRepository;
        }
        public async Task<IActionResult> Index(CancellationToken token)
        {
            if (!Request.Query.ContainsKey("companyId") || !Request.Query.ContainsKey("filmSessionId"))
            {
                return NotFound();
            }

            Request.Query.TryGetValue("companyId", out var companyId);
            var company = await _companyRepository.FindByIdAsync(long.Parse(companyId), token);

            Request.Query.TryGetValue("filmSessionId", out var filmSessionId);
            var currentFilmSession = await _filmSessionRepository.FindByIdAsync(long.Parse(filmSessionId), token);
            var poster = _blobRepository.Get(currentFilmSession.Film.Id);

            var posters = new Dictionary<long, string>();

            var viewModel = new BookingViewModel
            {
                Company = company,
                Film = currentFilmSession.Film,
                FilmSession = currentFilmSession,
                Poster = poster,
            };

            return View(viewModel);
        }
    }
}
