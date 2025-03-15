using Gyumri.Application.Interfaces;
using Gyumri.Application.Services;
using Gyumri.Common.ViewModel.Place;
using Microsoft.AspNetCore.Mvc;

namespace Gyumri.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PlaceController : Controller
    {
        private readonly IPlace _placeService;
        private readonly ISubcategory _subcategoryService;



        public PlaceController(IPlace placeService, ISubcategory subcategoryService)
        {
            _placeService = placeService;
            _subcategoryService = subcategoryService;
        }

        public IActionResult Index()
        {
            var places = _placeService.GetAllPlaces();
            return View(places);
        }

        public IActionResult Add()
        {
            var subcategories = _subcategoryService.GetAllSubcategories();
            ViewBag.Subcategories = subcategories;


            return View();
        }

        [HttpPost]
        public IActionResult Add(AddEditPlaceViewModel model, IFormFile photo)
        {
            Console.WriteLine("POST Add action hit.");

            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState is not valid.");

                var subcategories = _subcategoryService.GetAllSubcategories();
                ViewBag.Subcategories = subcategories;
                return View(model);
            }

            if (photo != null && photo.Length > 0)
            {
                Console.WriteLine("Photo provided, processing the file.");

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
                var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                var filePath = Path.Combine(directoryPath, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    photo.CopyTo(fileStream);
                }

                model.Photo = "/images/" + fileName;
            }
            else
            {
                model.Photo = "default.jpg";
            }

            Console.WriteLine("Calling AddPlace method.");

            bool result = _placeService.AddPlace(model); 
            if (result) 
            {
                Console.WriteLine("Place added successfully.");
                return RedirectToAction("Index");
            }

            Console.WriteLine("Failed to add place.");
            ModelState.AddModelError("", "Failed to add place.");

            var subcategoriesAgain = _subcategoryService.GetAllSubcategories();
            ViewBag.Subcategories = subcategoriesAgain;

            return View(model);
        }

    }
}
