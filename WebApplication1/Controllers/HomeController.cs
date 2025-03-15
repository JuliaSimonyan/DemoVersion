using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Gyumri.Controllers
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
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Content/index.html");

            if (System.IO.File.Exists(filePath))
            {
                return PhysicalFile(filePath, "text/html");
            }
            else
            {
                return NotFound(); // If the file does not exist
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
