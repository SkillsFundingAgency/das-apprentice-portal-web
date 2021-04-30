using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SFA.DAS.ApprenticeService.Web.Models;
using SFA.DAS.ApprenticeService.Web.Configuration;
using System.Diagnostics;

namespace SFA.DAS.ApprenticeService.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly ApprenticeCommitementsUrlConfiguration _configuration;

        public HomeController(
            ILogger<HomeController> logger
            //,ApprenticeCommitementsUrlConfiguration configuration)
            )
        {
            _logger = logger;
            //_configuration = configuration;
        }

        public IActionResult Index()
        {
            //return View(new HomeModel
            //    {
            //        ApprenticeCommitmentsUrl = $"{_configuration.ApprenticeCommitmentsBaseUrl}"
            //    });

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
