using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop.Implementation;
using Mysqlx.Crud;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller { 

        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;   
        }
        public IActionResult Index()
        {
            List<Category> OjectCategoriesList = _db.Categories.ToList();// the is line of code gets all data from the db. this line does the query "SELECT * FORM categories"
            return View(OjectCategoriesList);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            // custome server validation
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder exatly match the Name.");
            }
            if (ModelState.IsValid)// validate the given constraint
            {
                _db.Categories.Add(obj); // this line insert into the db 
                _db.SaveChanges();
                TempData["success"] = "Category is created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? CatagroyFromDb = _db.Categories.Find(id);
            if (CatagroyFromDb == null)
            {
                return NotFound();
            }
            return View(CatagroyFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {  
            if (ModelState.IsValid)// validate the given constraint
            {
                _db.Categories.Update(obj); // this line insert into the db 
                _db.SaveChanges(); 
                TempData["success"] = "Category is updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if(id==null|| id == 0)
            {
                return NotFound();
            }
            Category? CatagroyFromDb = _db.Categories.Find(id);
            if (CatagroyFromDb == null)
            {
                return NotFound();
            }
            return View(CatagroyFromDb);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePOST(int?id)
        {   
            Category ?obj= _db.Categories.Find(id);
            if (obj == null) return NotFound();
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category is Deleted successfully";
            return RedirectToAction("Index");

        }
    }
}
     