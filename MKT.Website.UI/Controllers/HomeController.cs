using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit.Text;
using MimeKit;
using MKT.Website.Models;
using System.Diagnostics;
using System.Net.Mail;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Mvc.Localization;
using MKT.Website.Services;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace MKT.Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private LanguageService _localization;
        private string _languageCode = "";

        public HomeController(ILogger<HomeController> logger, LanguageService localization)
        {
            _logger = logger;
            _localization = localization;

        }


        #region Localization
        [Route("ChangeLanguage")]
        public IActionResult ChangeLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions() { Expires = DateTimeOffset.UtcNow.AddYears(1) });

            returnUrl = returnUrl.Replace("/en-US", "").Replace("/ar-AE", "").Replace("/fr-FR", "")
                                 .Replace("/En", "").Replace("/Ar", "").Replace("/Fr", "")
                                  .Replace("/en", "").Replace("/ar", "").Replace("/fr", "");


            _languageCode = culture.Substring(0, 2);
            _languageCode = _languageCode.Substring(0, 1).ToLower() + _languageCode.Substring(1);

            //HttpContext.Session.SetString("languageCode", _languageCode);
            
            return Redirect($"/{_languageCode}{returnUrl}");
        }
        #endregion


        public IActionResult About()
        {            
            string languageCode = CultureInfo.CurrentCulture.ToString();

            languageCode = languageCode == "en-US" ? "en" : languageCode == "ar-AE" ? "ar" : "fr";
            string canonicalUrl = "https://technexus.ae/" + languageCode + "/Home/About";

            ViewBag.CanonicalUrl = canonicalUrl;
            ViewBag.Hreflang = languageCode;

            return View();
        }



        public IActionResult Services()
        {
            string languageCode = CultureInfo.CurrentCulture.ToString();

            languageCode = languageCode == "en-US" ? "en" : languageCode == "ar-AE" ? "ar" : "fr";
            string canonicalUrl = "https://technexus.ae/" + languageCode + "/Home/Services";

            ViewBag.CanonicalUrl = canonicalUrl;
            ViewBag.Hreflang = languageCode;

            return View();
        }



        public IActionResult Technologies()
        {
            string languageCode = CultureInfo.CurrentCulture.ToString();

            languageCode = languageCode == "en-US" ? "en" : languageCode == "ar-AE" ? "ar" : "fr";
            string canonicalUrl = "https://technexus.ae/" + languageCode + "/Home/Technologies";

            ViewBag.CanonicalUrl = canonicalUrl;
            ViewBag.Hreflang = languageCode;

            return View();
        }




        public IActionResult Clients()
        {
            string languageCode = CultureInfo.CurrentCulture.ToString();

            languageCode = languageCode == "en-US" ? "en" : languageCode == "ar-AE" ? "ar" : "fr";
            string canonicalUrl = "https://technexus.ae/" + languageCode + "/Home/Clients";

            ViewBag.CanonicalUrl = canonicalUrl;
            ViewBag.Hreflang = languageCode;

            return View();
        }



        public IActionResult ContactUs()
        {
            string languageCode = CultureInfo.CurrentCulture.ToString();

            languageCode = languageCode == "en-US" ? "en" : languageCode == "ar-AE" ? "ar" : "fr";

            string canonicalUrl = "https://technexus.ae/" + languageCode + "/Home/ContactUs";

            ViewBag.CanonicalUrl = canonicalUrl;
            ViewBag.Hreflang = languageCode;

            return View();
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [Route("Default")]
        public IActionResult Default()
        {
            string languageCode = CultureInfo.CurrentCulture.ToString();

            languageCode = languageCode == "en-US" ? "en" : languageCode == "ar-AE" ? "ar" : "fr";

            ViewBag.CanonicalUrl = "https://technexus.ae/" + languageCode + "/";
            ViewBag.Hreflang = languageCode + "-default";

            return PartialView();
        }

        public IActionResult Main(string lang)
        {
            string languageCode = CultureInfo.CurrentCulture.ToString();

            languageCode = languageCode == "en-US" ? "en" : languageCode == "ar-AE" ? "ar" : "fr";

            ViewBag.CanonicalUrl = "https://technexus.ae/" + languageCode + "/";
            ViewBag.Hreflang = languageCode + "-default";

            return View();
        }

        public IActionResult StartYourProject()
        {
            string languageCode = CultureInfo.CurrentCulture.ToString();

            languageCode = languageCode == "en-US" ? "en" : languageCode == "ar-AE" ? "ar" : "fr";

            string canonicalUrl = "https://technexus.ae/" + languageCode + "/Home/StartYourProject";

            ViewBag.CanonicalUrl = canonicalUrl;
            ViewBag.Hreflang = languageCode;

            return View();
        }
        public IActionResult TesterPage()
        {
            return View();
        }
    }
}