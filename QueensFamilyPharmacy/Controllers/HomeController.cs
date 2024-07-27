using Microsoft.AspNetCore.Mvc;
using QueensFamilyPharmacy.Models;
using System.Diagnostics;

namespace QueensFamilyPharmacy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailSender _emailSender;

        public HomeController(ILogger<HomeController> logger, IEmailSender emailSender)
        {
            _logger = logger;
            _emailSender = emailSender;
        }

        public IActionResult Index()
        {            
            //_emailSender.SendEmail("dhruvpatel1093@gmail.com", "User Regitered");            
            return View();
        }
        public IActionResult QuickRefill()
        {
            return View();
        }
        public IActionResult TransferPrescription()
        {
            return View();
        }
        public IActionResult ContactUs() { 

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
