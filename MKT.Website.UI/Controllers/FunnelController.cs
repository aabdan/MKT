using Microsoft.AspNetCore.Mvc;
using MKT.Website.Data;
using MKT.Website.Models;

namespace MKT.Website.UI.Controllers
{
    public class FunnelController : Controller
    {

        private readonly ApplicationDbContext _db;

        public FunnelController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult BuildAppGuid()
        {
            return PartialView("BuildAppGuidForm");
        }

        public IActionResult Error()
        {
            return PartialView("BuildAppGuidForm");
        }
        public IActionResult BuildAppGuidConfirmation()
        {
            return PartialView("BuildAppGuidConfirmation");
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Person obj)
        {
            //Custom Validation
            if (obj.PersonName == obj.PersonEmail)
            {
                ModelState.AddModelError("PersonName", "The Person Name cannot be exactly match the Person email");
            }
            if (ModelState.IsValid) //Server Validation
            {
                _db.Persons.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "شكراً لتحميلك دليل بناء منتج تقني";

                return RedirectToAction("BuildAppGuidConfirmation");
            }
            return View(obj);


        }
    }
}
