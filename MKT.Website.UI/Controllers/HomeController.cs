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

namespace MKT.Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private LanguageService _localization;
        private string _languageCode;

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
            return Redirect($"/{_languageCode}{returnUrl}");


        }
        #endregion


        public IActionResult About()
        {
            string canonicalUrl = "https://technexus.ae/" + _languageCode + "/Home/About";

            Response.Headers.Add("Canonical", canonicalUrl);
            Response.Headers.Add("Hreflang", _languageCode);


            return View();
        }



        public IActionResult Services()
        {
            string canonicalUrl = "https://technexus.ae/" + _languageCode + "/Home/Services";

            Response.Headers.Add("Canonical", canonicalUrl);
            Response.Headers.Add("Hreflang", _languageCode);

            return View();
        }



        public IActionResult Technologies()
        {
            string canonicalUrl = "https://technexus.ae/" + _languageCode + "/Home/Technologies";

            Response.Headers.Add("Canonical", canonicalUrl);
            Response.Headers.Add("Hreflang", _languageCode);

            return View();
        }




        public IActionResult Clients()
        {
            string canonicalUrl = "https://technexus.ae/" + _languageCode + "/Home/Clients";

            Response.Headers.Add("Canonical", canonicalUrl);
            Response.Headers.Add("Hreflang", _languageCode);

            return View();
        }



        public IActionResult ContactUs()
        {
            string canonicalUrl = "https://technexus.ae/" + _languageCode + "/Home/ContactUs";

            Response.Headers.Add("Canonical", canonicalUrl);
            Response.Headers.Add("Hreflang", _languageCode);

            return View();
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [Route("Default")]
        public IActionResult Default()
        {
            return PartialView();
        }

        public IActionResult Main(string lang)
        {
            var currentCulture = Thread.CurrentThread.CurrentUICulture.Name;
            string canonicalUrl = "https://technexus.ae/";

            Response.Headers.Add("Canonical", canonicalUrl);
            Response.Headers.Add("Hreflang", _languageCode);

            return View();
        }

        public IActionResult StartYourProject()
        {
            return View();
        }
        public IActionResult TesterPage()
        {
            return View();
        }
    }
}