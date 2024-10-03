using BulkyBookWeb.Models;
using BulkyBookWeb.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Product> obj = _UnitOfWork.Product.GetAll().ToList();
            return View(obj);
        }
        public IActionResult create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product obj)
        {
            if (ModelState.IsValid) {
                _UnitOfWork.Product.Add(obj);
                _UnitOfWork.Save();
                TempData["success"] = "Product is created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int? Id)
        {
            if (Id == 0 || Id == null)
            {
                return NotFound();
            }
            Product EditObj = _UnitOfWork.Product.Get(item => item.Id == Id);
            if (EditObj == null)
            {
                return NotFound();
            }
            return View(EditObj);
        }
        [HttpPost]
        public IActionResult Edit(Product? EditObj)
        {
            if (EditObj == null || EditObj.Id == 0)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _UnitOfWork.Product.Update(EditObj);
                _UnitOfWork.Save();
                TempData["success"] = "Product is updated successfully";
                return RedirectToAction("Index");
            }
            return View();
            
        }
        public IActionResult Delete(int? Id)
        {
            if (Id == 0 || Id == null)
            {
                return NotFound();
            }
            Product DeleteObj = _UnitOfWork.Product.Get(item => item.Id == Id);
            if (DeleteObj == null)
            {
                return NotFound();
            }
            return View(DeleteObj);

        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? Id)
        {
            if (Id == 0 || Id == null)
            {
                return NotFound();
            }
            Product DeleteObj = _UnitOfWork.Product.Get(item=>item.Id==Id);
            if (DeleteObj == null)
            {
                return NotFound();
            }
            _UnitOfWork.Product.Remove(DeleteObj);
            _UnitOfWork.Save();
            TempData["success"] = "Product is deleted successfully";
            return RedirectToAction("Index");
        }
    }
    
}
