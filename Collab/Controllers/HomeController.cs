using Collab.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Collab.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Check if the user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                // Show a welcome message with the username
                ViewData["Message"] = "Welcome back, " + User.Identity.Name;
            }
            else
            {
                // Show a generic welcome message
                ViewData["Message"] = "Welcome to Collab! Please log in or register to get started.";
            }

            return View();
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
