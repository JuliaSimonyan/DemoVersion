using Gyumri.Application.Interfaces;
using Gyumri.Common.ViewModel.Category;
using Gyumri.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Gyumri.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ApplicationContext _context;

        private readonly ICategory _categoryService;

        public CategoryController(ICategory categoryService, ApplicationContext context)
        {
            _categoryService = categoryService;
            _context = context;

        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.CategoryList();
            return View(categories);
        }

        public IActionResult Add()
        {
            return View(new AddCategoryViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCategoryViewModel category)
        {
            if (ModelState.IsValid)
            {
                bool success = await _categoryService.Add(category);
                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Error adding category.");
            }
            return View(category);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound(); 
            }

            var model = new EditCategoryViewModel
            {
                CategoryId = category.CategoryId,
                Name = category.Name
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); 
            }

            if (model.CategoryId == 0)
            {
                return NotFound(); 
            }

            var category = await _context.Categories.FindAsync(model.CategoryId);
            if (category == null)
            {
                return NotFound(); 
            }

            category.Name = model.Name;
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            bool success = await _categoryService.Delete(id);
            if (success)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }
    }
}
