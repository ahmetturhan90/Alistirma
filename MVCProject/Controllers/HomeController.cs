using Alistirma.Data;
using Microsoft.AspNetCore.Mvc;
using MVCProject.Models;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace MVCProject.Controllers
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
            User user = new User();
            user.Password = "qsdsad";
            user.Email = "asdsadsadas";
            user.Name = "asdasdasda";
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
