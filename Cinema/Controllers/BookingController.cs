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
        private const string AllowedChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static Random _random = new Random();

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

            var bookingId = Guid.NewGuid();
            var confirmationCode = GenerateCode();
            foreach (var place in places)
            {
                var ticket = new Ticket
                {
                    BookingId = bookingId,
                    ConfirmationCode = confirmationCode,
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
                {"companyId", model.Company.Id}, {"bookingId", bookingId}
            };

            return RedirectToAction(nameof(Info), "Booking", routeValuesDictionary);
        }

        [HttpGet]
        public async Task<IActionResult> Info(CancellationToken token)
        {
            if (!Request.Query.ContainsKey("companyId") || !Request.Query.ContainsKey("bookingId"))
            {
                return NotFound();
            }

            Request.Query.TryGetValue("companyId", out var companyId);
            var company = await _companyRepository.FindByIdAsync(long.Parse(companyId), token);

            Request.Query.TryGetValue("bookingId", out var bookingId);
            var tickets = (await _ticketRepository.FindByBookingIdAsync(Guid.Parse(bookingId), token)).ToList();
            var currentFilmSession = await _filmSessionRepository.FindByIdAsync(tickets.First().FilmSession.Id, token);
            var poster = _blobRepository.Get(currentFilmSession.Film.Id);

            var viewModel = new BookingInfoViewModel
            {
                Company = company,
                Film = currentFilmSession.Film,
                FilmSession = currentFilmSession,
                Poster = poster,
                Money = tickets.Count * currentFilmSession.Price,
                Tickets = tickets,
                ConfirmationCode = tickets.First().ConfirmationCode
            };

            return View(viewModel);
        }

        private static string GenerateCode()
        {
            const int stringLength = 5;
            var chars = new char[stringLength];
            var setLength = AllowedChars.Length;

            for (var i = 0; i < stringLength; ++i)
            {
                chars[i] = AllowedChars[_random.Next(setLength)];
            }

            var randomString = new string(chars, 0, stringLength);

            return randomString;
        }
    }
}
