using log_reg.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace log_reg.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UsersContext _context;
        public HomeController(ILogger<HomeController> logger, UsersContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Register()
        {
            try
            {
                _context.Database.OpenConnection();
                return View();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Database connection error");
                return View("DbStatus");
            }
        }

        public IActionResult DisplayUser()
        {
            return View();
        }

        public IActionResult DbStatus()
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