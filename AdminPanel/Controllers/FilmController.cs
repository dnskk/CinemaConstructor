using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AdminPanel.Models.CinemaViewModels;
using AdminPanel.Models.FilmViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    public class FilmController : BaseController
    {

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

            var viewModel = new FilmCreateViewModel();

            return View(viewModel);
        }
    }
}
