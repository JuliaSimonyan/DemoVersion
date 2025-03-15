using Gyumri.Application.Interfaces;
using Gyumri.Common.ViewModel.Category;
using Gyumri.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

        public IActionResult Index()
        {
            var categories = _categoryService.CategoryList().Result;
            return View(categories);
        }

        public IActionResult Add()
        {
            return View(new AddCategoryViewModel());
        }

        [HttpPost]
        public IActionResult Add(AddCategoryViewModel category)
        {
            if (ModelState.IsValid)
            {
                bool success = _categoryService.Add(category).Result;
                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Error adding category.");
            }
            return View(category);
        }

        public IActionResult Edit(int id)
        {
            var category = _context.Categories.Find(id);
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
        public IActionResult Edit(EditCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.CategoryId == 0)
            {
                return NotFound();
            }

            var category = _context.Categories.Find(model.CategoryId);
            if (category == null)
            {
                return NotFound();
            }

            category.Name = model.Name;
            _context.Entry(category).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            bool success = _categoryService.Delete(id).Result;
            if (success)
            {
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }
    }
}
