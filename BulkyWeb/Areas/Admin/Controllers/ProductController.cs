using BulkyBookWeb.Models;
using BulkyBookWeb.Repository.IRepository;
using BulkyBookWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IWebHostEnvironment _WebHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork,IWebHostEnvironment webHostEnvironment)
        {
            _UnitOfWork = unitOfWork;
            _WebHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> objProductList = _UnitOfWork.Product.GetAll(includeProperty:"Category").ToList();
            return View(objProductList);
        }
        public IActionResult Upsert(int? id)
        {
            ProductVM ProductVM = new() {
                CategoryListItem = _UnitOfWork.Category.GetAll().ToList()
                .Select(i => new SelectListItem
                {
                    Value = i.Id.ToString(),
                    Text = i.Name
                }), Product = new Product() };
            if (id == null || id == 0)
            {
                //Create
                return View(ProductVM);
            }
            else
            {
                //Update
                ProductVM.Product = _UnitOfWork.Product.Get(i => i.Id == id);
                return View(ProductVM);
            }
        }
        [HttpPost]
        public IActionResult Upsert(ProductVM? ProductVM,IFormFile? file)
        {
            if (ModelState.IsValid) {
                string wwwRootPath = _WebHostEnvironment.WebRootPath;//to get the path name of wwwRoot
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath,@"images\product");
                    //checking for oldImage if exist it remove and add new image
                    if (!string.IsNullOrEmpty(ProductVM.Product.ImageUrl))
                    {
                        var oldImage = Path.Combine(wwwRootPath, ProductVM.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImage))
                        {
                            System.IO.File.Delete(oldImage);
                        }

                    }  
                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    ProductVM.Product.ImageUrl = @"images\product\" + fileName;
                }
                if (ProductVM.Product.Id == 0)
                {
                    _UnitOfWork.Product.Add(ProductVM.Product);
                }
                else
                {
                    _UnitOfWork.Product.Update(ProductVM.Product);
                }

                _UnitOfWork.Save();
                TempData["success"] = "Product is created successfully";    
                return RedirectToAction("Index");
            }
            else
            {
                ProductVM.CategoryListItem = _UnitOfWork.Category.GetAll().ToList()
                    .Select(i => new SelectListItem
                    {
                        Value = i.Id.ToString(),
                        Text = i.Name
                    });
                return View(ProductVM);
            }
        }
        //public IActionResult Edit(int? Id)
        //{
        //    if (Id == 0 || Id == null)
        //    {
        //        return NotFound();
        //    }
        //    Product EditObj = _UnitOfWork.Product.Get(item => item.Id == Id);
        //    if (EditObj == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(EditObj);
        //}
        //[HttpPost]
        //public IActionResult Edit(Product? EditObj)
        //{
        //    if (EditObj == null || EditObj.Id == 0)
        //    {
        //        return NotFound();
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        _UnitOfWork.Product.Update(EditObj);
        //        _UnitOfWork.Save();
        //        TempData["success"] = "Product is updated successfully";
        //        return RedirectToAction("Index");
        //    }
        //    return View();
            
        //}
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
