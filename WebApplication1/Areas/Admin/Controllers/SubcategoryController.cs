using Gyumri.Application.Interfaces;
using Gyumri.Common.ViewModel.Subcategory;
using Microsoft.AspNetCore.Mvc;

namespace Gyumri.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SubcategoryController : Controller
    {
        private readonly ISubcategory _subcategoryService;
        private readonly ICategory _categoryService;

        public SubcategoryController(ISubcategory subcategoryService, ICategory categoryService)
        {
            _subcategoryService = subcategoryService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var subcategories = await _subcategoryService.GetAllSubcategories();
            return View(subcategories);
        }

        public async Task<IActionResult> Add()
        {
            ViewBag.Categories = await _categoryService.GetAllCategories();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddSubcategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _categoryService.GetAllCategories();
                return View(model);
            }

            var result = await _subcategoryService.AddSubcategory(model);
            if (result)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Failed to add subcategory.");
            ViewBag.Categories = await _categoryService.GetAllCategories();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var subcategory = await _subcategoryService.GetSubcategoryById(id);

            if (subcategory == null)
            {
                return NotFound();
            }

            var model = new EditSubcategoryViewModel
            {
                SubcategoryId = subcategory.SubcategoryId,
                Name = subcategory.Name,
                CategoryId = subcategory.CategoryId
            };

            ViewBag.Categories = await _categoryService.GetAllCategories();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditSubcategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _categoryService.GetAllCategories();
                return View(model);
            }

            var result = await _subcategoryService.EditSubcategory(model);
            if (result)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Failed to edit subcategory.");
            ViewBag.Categories = await _categoryService.GetAllCategories();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            bool success = await _subcategoryService.DeleteSubcategory(id);
            if (success)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }
    }
}
