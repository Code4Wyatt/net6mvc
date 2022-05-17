using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _db;

        public CategoryController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.Categories;
            return View(objCategoryList);
        }

        //GET 
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            // Custom Validation [Name and DisplayOrder cannot match]
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("displayorder", "The DisplayOrder cannot exactly match the name");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj); // Adds object user inputs
                _db.SaveChanges(); // Saves changes to the database
                TempData["success"] = "Category added successfully";
                return RedirectToAction("Index");
            }
           return View(obj);
        }
        //GET 
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var catergoryFromDb = _db.Categories.Find(id);

            if (catergoryFromDb == null)
            {
                return NotFound();
            }
            return View(catergoryFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            // Custom Validation [Name and DisplayOrder cannot match]
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("displayorder", "The DisplayOrder cannot exactly match the name");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj); // Adds object user inputs
                _db.SaveChanges(); // Saves changes to the database
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        //GET 
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var catergoryFromDb = _db.Categories.Find(id);

            if (catergoryFromDb == null)
            {
                return NotFound();
            }
            return View(catergoryFromDb);
        }

        //POST
        [HttpPost, ActionName("Delete")] // ActionName gives DeletePOST an alias incase a Delete action is submitted
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            
                _db.Categories.Remove(obj); // Remove category object
                _db.SaveChanges(); // Saves changes to the database
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        
        }
    }
}
