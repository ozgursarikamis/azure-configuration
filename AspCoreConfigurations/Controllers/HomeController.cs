﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AspCoreConfigurations.Models;
using Microsoft.Extensions.Configuration;

namespace AspCoreConfigurations.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            //_logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var message = _configuration["Welcome:WelcomeMessage"];
            
            ViewData["WelcomeMessage"] = message;
            ViewData["TodaysNumber"] = _configuration["Welcome:TodaysNumber"];
            ViewData["SystemUp"] = _configuration["Welcome:SystemUp"];

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
