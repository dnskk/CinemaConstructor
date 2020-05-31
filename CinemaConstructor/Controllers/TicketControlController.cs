using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CinemaConstructor.Database.Repositories;
using CinemaConstructor.Models.TicketControlViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaConstructor.Controllers
{
    [Authorize]
    public class TicketControlController : BaseController
    {
        private readonly TicketRepository _ticketRepository;
        private readonly FilmSessionRepository _filmSessionRepository;

        public TicketControlController(TicketRepository ticketRepository, FilmSessionRepository filmSessionRepository)
        {
            _ticketRepository = ticketRepository;
            _filmSessionRepository = filmSessionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken token)
        {
            AddBreadcrumb("Ticket control", "/TicketControl");

            var viewModel = new TicketControlViewModel
            {
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(TicketControlViewModel model, CancellationToken token, string returnUrl = null)
        {
            AddBreadcrumb("Ticket control", "/TicketControl");
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                var tickets = (await _ticketRepository.FindByConfirmationCodeAsync(model.ConfirmationCode, token)).ToList();

                if (tickets.Count != 0)
                {
                    model.FilmSession = await _filmSessionRepository.FindByIdAsync(tickets.First().FilmSession.Id, token);
                    model.Tickets = tickets;
                }

                model.IsChecked = true;
                return View(model);
            }

            return View(model);
        }
    }
}
