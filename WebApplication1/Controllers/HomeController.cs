using Gyumri.Application.Interfaces;
using Gyumri.Application.Services;
using Gyumri.Common.ViewModel.Category;
using Gyumri.Data.Models;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Web;

namespace Gyumri.Controllers
{
    public class HomeController : Controller
    {
        private const string CultureCookieName = "UserCulture";
        private readonly ApplicationContext _context;
        private readonly ICategory _categoryService;
        private readonly ISubcategory _subcategoryService;
        public HomeController(ICategory categoryService, ApplicationContext context, ISubcategory subcategoryService)
        {
            _categoryService = categoryService;
            _context = context;
            _subcategoryService = subcategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var language = Request.Cookies[CultureCookieName] ?? "en"; // Default 'en'
            var cultureInfo = new System.Globalization.CultureInfo(language);
            System.Threading.Thread.CurrentThread.CurrentCulture = cultureInfo;
            System.Threading.Thread.CurrentThread.CurrentUICulture = cultureInfo;

            ViewBag.Categories = await _categoryService.GetAllCategories();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Category(int categoryId)
        {
            Category category = await _categoryService.GetCategoryById(categoryId);
            var subcategories = await _subcategoryService.GetSubcategoriesByCategoryId(categoryId);
            ViewBag.Subcategories = subcategories;
            ViewBag.Category = category;
            ViewBag.Categories = await _categoryService.GetAllCategories();
            return View();
        }

        [HttpPost]
        public IActionResult ChangeLanguage(string lang)
        {
            // Պահել լեզուն cookie-ում
            Response.Cookies.Append(CultureCookieName, lang, new Microsoft.AspNetCore.Http.CookieOptions
            {
                Expires = DateTime.UtcNow.AddYears(1),  // Լեզուն պահպանվում է 1 տարի
                IsEssential = true, // Անհրաժեշտ է կարգավորումների համար
                SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Lax // Համակարգչի միջավայրում
            });

            // Արտահանենք լեզվով փոխված էջը
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
