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
            //_emailSender.SendEmail("demonsrontheway@gmail.com", "User Regitered");            
            return View();
        }
        [HttpPost]
        public IActionResult QuickRefill(QuickRefill quickRefill)
        {
            string Msg = string.Empty;
            try
            {
				_emailSender.SendEmail(quickRefill.QEmail, "Queen's Quick Refill", quickRefill);
				Msg = "Thank You, Raising Request For Quick Refill";
			}
            catch (Exception)
            {
                Msg = "We are currently experiencing technical difficulties. please try again later";
			}            
            return View(Msg);
        }
        public IActionResult TransferPrescription(QuickRefill quickRefill)
        {
			string Msg = string.Empty;
            try
            {
				_emailSender.SendEmail(quickRefill.QEmail, "Request to Transfer Prescription into Queen's Family Pharmacy", quickRefill);
				Msg = "Thank You, Raising Request For Transfer Prescription";
			}
            catch (Exception)
            {
				Msg = "We are currently experiencing technical difficulties. please try again later";
			}			
            return View(Msg);
        }
        public IActionResult ContactUs(QuickRefill quickRefill)
        {
			string Msg = string.Empty;
			try
            {
				_emailSender.SendEmail(quickRefill.QEmail, "Contact With Us", quickRefill);
				Msg = "Thank You, Contact With Us";
			}
            catch (Exception)
            {
				Msg = "We are currently experiencing technical difficulties. please try again later";
			}            
            return View(Msg);
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
