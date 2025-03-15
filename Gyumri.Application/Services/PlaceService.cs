using Gyumri.Application.Interfaces;
using Gyumri.Common.ViewModel.Place;
using Gyumri.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace Gyumri.Application.Services
{
    public class PlaceService : IPlace
    {
        private readonly ApplicationContext _context;

        public PlaceService(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<PlacesViewModel> GetAllPlaces()
        {
            return _context.Places
                .Select(p => new PlacesViewModel
                {
                    Id = p.PlaceId,
                    PlaceName = p.PlaceName,
                    Description = p.Description,
                    Photo = p.Photo,
                    SubcategoryId = p.SubcategoryId
                })
                .ToList();
        }

        public PlacesViewModel GetPlaceById(int id)
        {
            var place = _context.Places.FirstOrDefault(p => p.PlaceId == id);
            if (place == null)
                return null;

            return new PlacesViewModel
            {
                Id = place.PlaceId,
                PlaceName = place.PlaceName,
                Description = place.Description,
                Photo = place.Photo,
                SubcategoryId = place.SubcategoryId
            };
        }

        public bool AddPlace(AddEditPlaceViewModel model)
        {
            var place = new Place
            {
                PlaceName = model.PlaceName,
                Description = model.Description,
                Photo = model.Photo,
                SubcategoryId = model.SubcategoryId
            };

            _context.Places.Add(place);
            _context.SaveChanges();
            return true;
        }

        public bool EditPlace(AddEditPlaceViewModel model)
        {
            var place = _context.Places.FirstOrDefault(p => p.PlaceId == model.Id);
            if (place == null) return false;

            place.PlaceName = model.PlaceName;
            place.Description = model.Description;
            place.Photo = model.Photo;
            place.SubcategoryId = model.SubcategoryId;

            _context.Places.Update(place);
            _context.SaveChanges();
            return true;
        }

        public bool DeletePlace(int id)
        {
            var place = _context.Places.FirstOrDefault(p => p.PlaceId == id);
            if (place == null) return false;

            _context.Places.Remove(place);
            _context.SaveChanges();
            return true;
        }
    }
}
