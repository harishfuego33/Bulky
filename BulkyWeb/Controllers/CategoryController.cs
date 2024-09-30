using BulkyBookWeb.Models;
using BulkyBookWeb.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller { 

      
        private readonly IUnitOfWork _UnitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
             _UnitOfWork = unitOfWork;   
        }
        public IActionResult Index()
        {
            List<Category> OjectCategoriesList =  _UnitOfWork.Category.GetAll().ToList();// the is line of code gets all data from the db. this line does the query "SELECT * FORM categories"
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
                 _UnitOfWork.Category.Add(obj); // this line insert into the db 
                 _UnitOfWork.Save();
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
            Category? CatagroyFromDb =  _UnitOfWork.Category.Get(item => item.Id == id);
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
                 _UnitOfWork.Category.Update(obj); // this line insert into the db 
                 _UnitOfWork.Save(); 
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
            Category? CatagroyFromDb =  _UnitOfWork.Category.Get(item=>item.Id==id);
            if (CatagroyFromDb == null)
            {
                return NotFound();
            }
            return View(CatagroyFromDb);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePOST(int?id)
        {   
            Category ?obj=  _UnitOfWork.Category.Get(item=>item.Id==id);
            if (obj == null) return NotFound();
             _UnitOfWork.Category.Remove(obj);
             _UnitOfWork.Save();
            TempData["success"] = "Category is Deleted successfully";
            return RedirectToAction("Index");

        }
    }
}
     