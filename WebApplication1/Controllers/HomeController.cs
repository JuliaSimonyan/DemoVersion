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

        public IActionResult Index()
        {
            // Ընտրած լեզուն
            var language = Request.Cookies[CultureCookieName] ?? "en"; // Default 'en'

            // Լեզուն կիրառել
            var cultureInfo = new System.Globalization.CultureInfo(language);
            System.Threading.Thread.CurrentThread.CurrentCulture = cultureInfo;
            System.Threading.Thread.CurrentThread.CurrentUICulture = cultureInfo;

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
