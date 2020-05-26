using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LevelTest.Logic;
using LevelTest.Logic.DAL;
using Microsoft.AspNetCore.Mvc;
using LevelTest.Models;
using LevelTest.Models.Question;
using Microsoft.Extensions.Configuration;

namespace LevelTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly TestConfigurator _testConfigurator;

        public HomeController(IConfiguration configuration)
        {
            string connectionString = configuration["ConnectionStrings:LevelTest"];
            DataAccess dataAccess = new DataAccess(connectionString);
            TestConfiguration testConfiguration = TestConfiguration.GetConfigurations(configuration);

            _testConfigurator = new TestConfigurator(testConfiguration, dataAccess);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Test()
        {
            IEnumerable<Test> tests = _testConfigurator.GetTests();

            return View(tests);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

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
