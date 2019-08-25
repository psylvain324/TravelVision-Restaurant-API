using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using YorkHarborService.Models;

namespace YorkHarborService.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "CMS for York Harbor Applications.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Contact us here.";

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
