using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Threading.Tasks;
using Vietast.Web.Models;
using Vietast.Web.Service.IService;

namespace Vietast.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            List<ProductDTO>? list = new();
            ResponseDTO? response = await _productService.GetAllProductsAsync();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDTO>>(response.Result.ToString());
            }
            else
            {
                TempData["error"] = response.Message;
            }
            return View(list);
        }
        public async Task<IActionResult> Create()
        {
            List<SelectListItem> categories = new();
            var catgoriesResponse = await _categoryService.GetAllCategoriesAsync();
            if (catgoriesResponse != null && catgoriesResponse.IsSuccess)
            {
                var list = JsonConvert.DeserializeObject<List<CategoryDTO>>(catgoriesResponse.Result.ToString());
                categories = list.Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.Name
                }).ToList();
            }
            else
            {
                TempData["error"] = catgoriesResponse.Message;
            }
            ViewBag.Categories = categories;
            return View();
        }
       
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateDTO model)
        {
            if (ModelState.IsValid)
            {
                ResponseDTO? response = await _productService.CreateProductAsync(model);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Product created successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["error"] = response.Message;
                }
            }
            return View(model);
        }
        public async Task<IActionResult> Delete(Guid productId)
        {
            ResponseDTO? response = await _productService.GetProductAsync(productId);
            if (response != null && response.IsSuccess)
            {
                ProductDTO model = JsonConvert.DeserializeObject<ProductDTO>(response.Result.ToString());
                return View(model);
            }
            else
            {
                TempData["error"] = response.Message;
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(ProductDTO productDTO)
        {
            ResponseDTO? response = await _productService.DeleteProductAsync(productDTO.ProductId.ToString());
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Product deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = response.Message;
            }
            return NotFound();
        }
    }
}
