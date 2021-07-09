using Microsoft.AspNetCore.Mvc;
using SFA.DAS.ApprenticePortal.Web.Models;
using SFA.DAS.ApprenticePortal.Web.Startup;
using SFA.DAS.ApprenticePortal.Web.Views;
using System.Diagnostics;

namespace SFA.DAS.ApprenticePortal.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationConfiguration _configuration;

        public HomeController(ApplicationConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View(new HomeModel
            {
                ApprenticeCommitmentsBaseUrl = _configuration.ApprenticeCommitmentsBaseUrl
            });
        }

        public IActionResult Homepage()
        {
            return View(new HomepageModel
            {
                ApprenticeCommitmentsBaseUrl = _configuration.ApprenticeCommitmentsBaseUrl
            });
            //return View();
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