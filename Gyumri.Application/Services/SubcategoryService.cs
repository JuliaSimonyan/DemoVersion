using Gyumri.Application.Interfaces;
using Gyumri.Common.ViewModel.Subcategory;
using Gyumri.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gyumri.Application.Services
{
    public class SubcategoryService : ISubcategory
    {
        private readonly ApplicationContext _context;

        public SubcategoryService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<bool> AddSubcategory(AddSubcategoryViewModel model)
        {
            if(model == null) return false;
            Subcategory subcategory = new()
            {
                Name = model.Name,
                CategoryId = model.CategoryId,
            };
            await _context.AddAsync(subcategory);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteSubcategory(EditSubcategoryViewModel subcategory)//or subcategoryId
        {
            var entity = await _context.Subcategories
                .FirstOrDefaultAsync(p => p.SubcategoryId == subcategory.SubcategoryId);
            if (entity is null)
            {
                return false;
            }
            _context.Subcategories.Remove(entity);
            _context.SaveChangesAsync();
            return true;
        }
    }
}
