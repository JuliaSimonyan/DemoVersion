using Gyumri.Common.ViewModel.Place;
using System.Collections.Generic;

namespace Gyumri.Application.Interfaces
{
    public interface IPlace
    {
        IEnumerable<PlacesViewModel> GetAllPlaces();
        PlacesViewModel GetPlaceById(int id);
        bool AddPlace(AddEditPlaceViewModel model);
        bool EditPlace(AddEditPlaceViewModel model);
        bool DeletePlace(int id);
    }
}
