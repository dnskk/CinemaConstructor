using System.Threading;
using System.Threading.Tasks;
using AdminPanel.Models.FilmSessionViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    public class FilmSessionController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> All(CancellationToken token)
        {
            AddBreadcrumb("Film session", "/FilmSession/All");

            var viewModel = new FilmSessionAllViewModel
            {
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create(CancellationToken token)
        {
            AddBreadcrumb("Film session", "/FilmSession/All");
            AddBreadcrumb("Create", "/FilmSession/Create");

            var viewModel = new FilmSessionCreateViewModel
            {
            };

            return View(viewModel);
        }
    }
}
