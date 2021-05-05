using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SFA.DAS.ApprenticeService.Web.Models;
using System.Diagnostics;
using SFA.DAS.ApprenticeService.Web.Startup;

namespace SFA.DAS.ApprenticeService.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationConfiguration _configuration;

        public HomeController(
            ILogger<HomeController> logger
            , ApplicationConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View(new HomeModel
            {
                ApprenticeCommitmentsBaseUrl = _configuration.ApprenticeCommitmentsBaseUrl
            });
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
