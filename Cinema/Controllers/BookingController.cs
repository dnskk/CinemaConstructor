using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cinema.Models;
using CinemaConstructor.Database.Entities;
using CinemaConstructor.Database.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;

namespace Cinema.Controllers
{
    public class BookingController : Controller
    {
        private readonly CompanyRepository _companyRepository;
        private readonly FilmSessionRepository _filmSessionRepository;
        private readonly BlobRepository _blobRepository;
        private readonly TicketRepository _ticketRepository;

        public BookingController(CompanyRepository companyRepository, BlobRepository blobRepository,
            FilmSessionRepository filmSessionRepository, TicketRepository ticketRepository)
        {
            _companyRepository = companyRepository;
            _blobRepository = blobRepository;
            _filmSessionRepository = filmSessionRepository;
            _ticketRepository = ticketRepository;
        }

        [HttpGet]
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

            var tickets = (await _ticketRepository.FindByFilmSessionIdAsync(currentFilmSession.Id, token)).ToArray();
            var unavailableSeats = new long[tickets.Length][];
            for (var i = 0; i < tickets.Length; i++)
            {
                unavailableSeats[i] = new[] { tickets[i].Row, tickets[i].Column };
            }


            var viewModel = new BookingViewModel
            {
                Company = company,
                Film = currentFilmSession.Film,
                FilmSession = currentFilmSession,
                Poster = poster,
                UnavailableSeats = JsonConvert.SerializeObject(unavailableSeats)
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(BookingViewModel model, CancellationToken token)
        {
            var places = JsonConvert.DeserializeObject<long[][]>(model.Places);
            foreach (var place in places)
            {
                var ticket = new Ticket
                {
                    Email = model.Email,
                    Phone = model.Phone,
                    Row = place[0],
                    Column = place[1],
                    FilmSession = await _filmSessionRepository.FindByIdAsync(model.FilmSession.Id, token)
                };
                await _ticketRepository.AddAsync(ticket, token);
            }

            var routeValuesDictionary = new RouteValueDictionary
            {
                {"companyId", model.Company.Id}, {"filmId", model.Film.Id}
            };

            return RedirectToAction(nameof(FilmController.Index), "Film", routeValuesDictionary);
        }
    }
}
