using Microsoft.AspNetCore.Mvc;
using MKT.Website.Data;
using MKT.Website.Models;

namespace MKT.Website.UI.Controllers
{
    public class ServiceController : Controller
    {

        private readonly ApplicationDbContext _db;

        public ServiceController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult WebDevelopment()
        {
            return View();
        }

        public IActionResult MobileDevelopment()
        {
            return View();
        }

        public IActionResult ITConsultation()
        {
            return View();
        }

        public IActionResult CyberSecurity()
        {
            return View();
        }

        public IActionResult DigitalMarketing()
        {
            return View();
        }

        public IActionResult ECommerce()
        {
            return View();
        }



    }
}
