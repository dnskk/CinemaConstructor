using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CinemaConstructor.Database.Entities;
using CinemaConstructor.Database.Repositories;
using CinemaConstructor.Models.LandingViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CinemaConstructor.Controllers
{
    [Authorize]
    public class PersonalAreaController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly CompanyRepository _companyRepository;
        private readonly CompanyMemberRepository _companyMemberRepository;
        private readonly UserSessionRepository _userSessionRepository;
        private readonly ILogger _logger;

        public PersonalAreaController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, CompanyRepository companyRepository,
            CompanyMemberRepository companyMemberRepository, UserSessionRepository userSessionRepository,
            ILoggerFactory loggerFactory)
        {
            _companyRepository = companyRepository;
            _userManager = userManager;
            _companyMemberRepository = companyMemberRepository;
            _userSessionRepository = userSessionRepository;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<PersonalAreaController>();
        }

        public async Task<IActionResult> Index()
        {
            var model = new PersonalAreaViewModel
            {
                Companies = await GetCompanies(CancellationToken.None)
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(PersonalAreaViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                await CreateCompany(model.Name);

                model.Companies = await GetCompanies(CancellationToken.None);
                // Clear input.
                ModelState.Clear();
                model.Name = "";
                return View(model);
            }

            model.Companies = await GetCompanies(CancellationToken.None);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> SelectCompany(PersonalAreaViewModel model)
        {
            if (!Request.Query.ContainsKey("companyId"))
            {
                return await Index();
            }

            Request.Query.TryGetValue("companyId", out var companyId);

            var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User)) as IdentityUser;
            var userSession = new UserSession
            {
                UserId = Guid.Parse(user.Id),
                CurrentCompanyId = long.Parse(companyId)
            };

            await _userSessionRepository.UpdateAsync(userSession, CancellationToken.None);

            return RedirectToAction(nameof(CompanyController.Index), "Company");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation(4, "User logged out.");
            return RedirectToAction(nameof(LandingController.Index), "Landing");
        }

        private async Task CreateCompany(string name)
        {
            var company = new Company
            {
                Name = name
            };

            company = await _companyRepository.AddAsync(company, CancellationToken.None);
            var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User)) as IdentityUser;

            var companyMember = new CompanyMember
            {
                Company = company,
                UserId = user.Id,
                Role = MemberRole.Administrator
            };

            await _companyMemberRepository.AddAsync(companyMember, CancellationToken.None);
        }

        private async Task<List<Company>> GetCompanies(CancellationToken token)
        {
            var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User)) as IdentityUser;

            var companyMembers = await _companyMemberRepository.FindByUserIdAsync(user.Id, token);
            return companyMembers
                .Where(p => p.Role == MemberRole.Administrator)
                .Select(p => p.Company).ToList();
        }
    }
}
