using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using MKT.Website.Data;
using MKT.Website.Models;
using MKT.Infrastructure;
using MKT.Infrastructure.AzureBlobStorage;
using System.Data;
using Microsoft.AspNetCore.Authorization;

namespace MKT.Website.Controllers
{
    [Authorize(Roles = "Admin")]

    public class PersonController : Controller
    {
        private readonly ApplicationDbContext _db;

        public PersonController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Person> objPersonList = _db.Persons.OrderByDescending(x => x.CreatedDateTime);
            return View(objPersonList);
        }

        //Get
        [AllowAnonymous]
        public IActionResult Create()
        {

            return View();
        }

        //POST
        [AllowAnonymous]
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

                return RedirectToAction("DownloadGuide");
            }
            return View(obj);


        }


        //Get
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var PersonFromDb = _db.Persons.Find(id);
            if (PersonFromDb == null)
            {
                return NotFound();
            }

            return View(PersonFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Person obj)
        {
            //Custom Validation
            if (obj.PersonName == obj.PersonEmail)
            {
                ModelState.AddModelError("PersonName", "The Person Name cannot be exactly match the Person email");
            }
            if (ModelState.IsValid) //Server Validation
            {
                _db.Persons.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Person updated successfully.";

                return RedirectToAction("Index");
            }
            return View(obj);


        }

        //Get
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var PersonFromDb = _db.Persons.Find(id);
            if (PersonFromDb == null)
            {
                return NotFound();
            }

            return View(PersonFromDb);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {

            var obj = _db.Persons.Find(id);
            if (obj == null)
            {
                return NotFound();

            }
            _db.Persons.Remove(obj);

            _db.SaveChanges();
            TempData["success"] = "Person removed successfully.";

            return RedirectToAction("Index");


        }

    }
}
