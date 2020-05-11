using System.Threading;
using System.Threading.Tasks;
using AdminPanel.Models.HallViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    public class HallController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> All(CancellationToken token)
        {
            AddBreadcrumb("Halls", "/Hall/All");

            var viewModel = new HallAllViewModel
            {
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create(CancellationToken token)
        {
            AddBreadcrumb("Halls", "/Hall/All");
            AddBreadcrumb("Create", "/Hall/Create");

            var viewModel = new HallCreateViewModel();

            return View(viewModel);
        }
    }
}
