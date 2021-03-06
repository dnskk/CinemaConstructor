﻿using Cinema.Models;
using CinemaConstructor.Database.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cinema.Controllers
{
    public class FilmController : Controller
    {
        private readonly CompanyRepository _companyRepository;
        private readonly FilmRepository _filmRepository;
        private readonly FilmSessionRepository _filmSessionRepository;
        private readonly BlobRepository _blobRepository;

        public FilmController(CompanyRepository companyRepository, FilmRepository filmRepository,
            BlobRepository blobRepository, FilmSessionRepository filmSessionRepository)
        {
            _companyRepository = companyRepository;
            _filmRepository = filmRepository;
            _blobRepository = blobRepository;
            _filmSessionRepository = filmSessionRepository;
        }

        public async Task<IActionResult> Index(CancellationToken token)
        {
            if (!Request.Query.ContainsKey("companyId") || !Request.Query.ContainsKey("filmId"))
            {
                return NotFound();
            }

            Request.Query.TryGetValue("companyId", out var companyId);
            var company = await _companyRepository.FindByIdAsync(long.Parse(companyId), token);

            Request.Query.TryGetValue("filmId", out var filmId);
            var currentFilm = await _filmRepository.FindByIdAsync(long.Parse(filmId), token);
            var poster = _blobRepository.Get(currentFilm.Id);

            var posters = new Dictionary<long, string>();
            var upcomingFilms = (await _filmRepository.FindByCompanyIdAsync(company.Id, token))
                .Where(p => p.IsActive && p.ReleaseDate > DateTime.Now).ToList();
            foreach (var film in upcomingFilms)
            {
                posters[film.Id] = _blobRepository.Get(film.Id);
            }

            var filmSessions = (await _filmSessionRepository.FindByFilmIdAsync(currentFilm.Id, token))
                .Where(p => p.StartTime > DateTime.Now);

            var groupedSessions = filmSessions.GroupBy(p => p.Hall.Cinema.Name);

            var viewModel = new FilmViewModel
            {
                Company = company,
                Film = currentFilm,
                Poster = poster,
                GroupedSessions = groupedSessions.ToList(),
                UpcomingFilms = upcomingFilms,
                Posters = posters
            };

            return View(viewModel);
        }
    }
}
