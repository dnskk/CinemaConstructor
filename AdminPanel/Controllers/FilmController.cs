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

            var viewModel = new FilmCreateViewModel
            {
                ReleaseDate = DateTime.Now.ToString("MM\\/dd\\/yyyy")
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(FilmCreateViewModel model, CancellationToken token, string returnUrl = null)
        {
            AddBreadcrumb("Films", "/Film/All");
            AddBreadcrumb("Create", "/Film/Create");
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(All), "Film");
            }

            return View(model);
        }
    }
}
