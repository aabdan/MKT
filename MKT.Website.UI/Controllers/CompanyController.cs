using Microsoft.AspNetCore.Mvc;
using MKT.Website.Data;
using MKT.Website.Models;
using System.ComponentModel;

namespace MKT.Website.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CompanyController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Company> objCompanyList = _db.Companies;
            return View(objCompanyList);
        }
        //Get
        public IActionResult Create()
        {
       
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Company obj)
        {
            //Custom Validation
            if (obj.CompanyName == obj.CompanyDescription)
            {
                ModelState.AddModelError("CompanyName", "The Company Name cannot be exactly match the company description");
            }
            if(ModelState.IsValid) //Server Validation
            {
                _db.Companies.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Company created successfully.";
                return RedirectToAction("Index");
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
            var companyFromDb = _db.Companies.Find(id);
            if (companyFromDb == null)
            {
                return NotFound();
            }

            return View(companyFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Company obj)
        {
            //Custom Validation
            if (obj.CompanyName == obj.CompanyDescription)
            {
                ModelState.AddModelError("CompanyName", "The Company Name cannot be exactly match the company description");
            }
            if (ModelState.IsValid) //Server Validation
            {
                _db.Companies.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Company updated successfully.";

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
            var companyFromDb = _db.Companies.Find(id);
            if (companyFromDb == null)
            {
                return NotFound();
            }

            return View(companyFromDb);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
         
            var obj = _db.Companies.Find(id);
            if (obj == null)
            {
                return NotFound();

            }
            _db.Companies.Remove(obj);
            
                _db.SaveChanges();
            TempData["success"] = "Company removed successfully.";

            return RedirectToAction("Index");


        }

    }
}
