using System.Linq;
using CinemaConstructor.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaConstructor.Controllers
{
    [Authorize(Roles = "Administrators")]
    public class UserLogsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserLogsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public ViewResult Index()
        {
            return View(_context.UserAuditEvents.ToList());
        }
    }
}
