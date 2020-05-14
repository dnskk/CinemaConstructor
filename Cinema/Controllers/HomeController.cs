using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cinema.Models;
using CinemaConstructor.Database.Repositories;

namespace Cinema.Controllers
{
    public class HomeController : Controller
    {
        private readonly CompanyRepository _companyRepository;
        private readonly FilmRepository _filmRepository;

        public HomeController(CompanyRepository companyRepository, FilmRepository filmRepository)
        {
            _companyRepository = companyRepository;
            _filmRepository = filmRepository;
        }

        public async Task<IActionResult> Index(CancellationToken token)
        {
            if (!Request.Query.ContainsKey("companyId"))
            {
                return NotFound();
            }

            Request.Query.TryGetValue("companyId", out var companyId);
            var company = await _companyRepository.FindByIdAsync(long.Parse(companyId), token);
            var viewModel = new HomeViewModel
            {
                Company = company
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
