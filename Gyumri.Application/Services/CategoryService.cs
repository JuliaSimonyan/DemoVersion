using Gyumri.Application.Interfaces;
using Gyumri.Common.ViewModel.Category;
using Gyumri.Data.Models;
using Microsoft.EntityFrameworkCore;
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
        public async Task<Category> GetCategoryById(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<bool> Edit(EditCategoryViewModel model)
        {
            try
            {
                var category = await _context.Categories.FindAsync(model.CategoryId);
                if (category == null)
                {
                    return false;
                }

                category.Name = model.Name;

                _context.Entry(category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                int rowsAffected = await _context.SaveChangesAsync();
                if (rowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<bool> Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return false;

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<CategoryListViewModel>> GetAllCategories()
        {
            return await _context.Categories
                            .Select(c => new CategoryListViewModel
                            {
                                CategoryId = c.CategoryId,
                                Name = c.Name
                            })
                            .ToListAsync();
        }
    }
}
