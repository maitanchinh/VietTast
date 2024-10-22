using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Vietast.Web.Models;
using Vietast.Web.Service;
using Vietast.Web.Service.IService;

namespace Vietast.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            List<CategoryDTO>? list = new();
            ResponseDTO? response = await _categoryService.GetAllCategoriesAsync();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<CategoryDTO>>(response.Result.ToString());
            }
            else
            {
                TempData["error"] = response.Message;
            }
            return View(list);
        }
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateDTO model)
        {
            if (ModelState.IsValid)
            {
                ResponseDTO? response = await _categoryService.CreateCategoryAsync(model);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Category created successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["error"] = response.Message;
                }
            }
            return View(model);
        }
        public async Task<IActionResult> Delete(Guid categoryId)
        {
            ResponseDTO? response = await _categoryService.GetCategoryAsync(categoryId);
            if (response != null && response.IsSuccess)
            {
                CategoryDTO model = JsonConvert.DeserializeObject<CategoryDTO>(response.Result.ToString());
                return View(model);
            }
            else
            {
                TempData["error"] = response.Message;
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(CategoryDTO categoryDTO)
        {
            ResponseDTO? response = await _categoryService.DeleteCategoryAsync(categoryDTO.CategoryId.ToString());
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Category deleted successfully";
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
