using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AdminPanel.Models.CinemaViewModels;
using AdminPanel.Models.CompanyViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    public class CinemaController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> All(CancellationToken token)
        {
            AddBreadcrumb("Cinemas", "/Cinema/All");

            var viewModel = new CinemaAllViewModel()
            {
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create(CancellationToken token)
        {
            AddBreadcrumb("Cinemas", "/Cinema/All");
            AddBreadcrumb("Create", "/Cinema/Create");

            var viewModel = new CinemaCreateViewModel()
            {
            };

            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Create(CinemaCreateViewModel model, CancellationToken token, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {

                return RedirectToAction(nameof(All), "Cinema");
            }

            return View(model);
        }
    }
}
