using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminPanel.Models.LandingViewModels;
using AdminPanel.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminPanel.Controllers
{
    [Authorize]
    public class PersonalAreaController : Controller
    {
        private readonly CompanyRepository _companyRepository;

        public PersonalAreaController(CompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(PersonalAreaViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {

                // Clear input.
                ModelState.Clear();
                model.Name = "";
                return View(model);
            }

            return View();
        }
    }
}
