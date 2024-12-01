using Gyumri.Common.ViewModel.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gyumri.Application.Interfaces
{
    public interface ICategory
    {
        public Task<List<CategoryListViewModel>> CategoryList();
        public Task<bool> Add(AddCategoryViewModel category);
    }
}
