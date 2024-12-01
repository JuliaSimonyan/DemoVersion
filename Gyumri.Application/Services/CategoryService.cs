using Gyumri.Application.Interfaces;
using Gyumri.Common.ViewModel.Category;
using Gyumri.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gyumri.Application.Services
{
    public class CategoryService : ICategory
    {
        private readonly ApplicationContext _context;

        public CategoryService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(AddCategoryViewModel category)
        {
            if (_context.Categories.Select(i => i.Name).Contains(category.Name) || category == null) return false;
            Category newCategory = new Category
            {
                Name = category.Name
            };
            await _context.Categories.AddAsync(newCategory);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<CategoryListViewModel>> CategoryList()
        {
            List<CategoryListViewModel> categories = _context.Categories.Select(
                v => new CategoryListViewModel
                {
                    CategoryId = v.CategoryId,
                    Name = v.Name,
                }
            ).ToList();
            return categories;
        }
    }
}
