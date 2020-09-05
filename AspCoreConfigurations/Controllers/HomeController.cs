using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AspCoreConfigurations.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace AspCoreConfigurations.Controllers
{
    public class HomeController : Controller
    {
        private readonly WelcomeConfig _config;

        public HomeController(IOptions<WelcomeConfig> options)
        {
            _config = options.Value;
        }

        public IActionResult Index()
        { 
            return View(_config);
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
