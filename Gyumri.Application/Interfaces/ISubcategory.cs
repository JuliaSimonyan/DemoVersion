using Gyumri.Common.ViewModel.Subcategory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gyumri.Application.Interfaces
{
    public interface ISubcategory
    {
        Task<bool> AddSubcategory(AddSubcategoryViewModel model);
        Task<bool> EditSubcategory(EditSubcategoryViewModel model);
        Task<bool> DeleteSubcategory(int subcategoryId);
        List<SubcategoryViewModel> GetAllSubcategories();
        Task<SubcategoryViewModel> GetSubcategoryById(int subcategoryId);
    }
}
