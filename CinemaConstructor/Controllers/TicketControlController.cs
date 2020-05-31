using System.Threading;
using System.Threading.Tasks;
using CinemaConstructor.Models.TicketControlViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaConstructor.Controllers
{
    [Authorize]
    public class TicketControlController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken token)
        {
            AddBreadcrumb("Ticket control", "/TicketControl");

            var viewModel = new TicketControlViewModel
            {
            };

            return View(viewModel);
        }
    }
}
