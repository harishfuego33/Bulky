using BulkyBookWeb.Models;
using System.Collections;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BulkyBookWeb.ViewModels
{
    public class ProductVM
    {
        public Product? Product { set; get; }
        [ValidateNever]
        public IEnumerable<SelectListItem>? CategoryListItem { set; get; }
    }
}
