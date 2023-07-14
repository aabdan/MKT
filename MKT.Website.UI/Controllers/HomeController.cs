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


            string languageCode = culture.Substring(0, 2);
            languageCode = languageCode.Substring(0, 1).ToLower() + languageCode.Substring(1);
            return Redirect($"/{languageCode}{returnUrl}");


        }
        #endregion


        public IActionResult About()
        {
            return View();
        }



        public IActionResult Services()
        {
            return View();
        }



        public IActionResult Technologies()
        {
            return View();
        }




        public IActionResult Clients()
        {
            return View();
        }



        public IActionResult ContactUs()
        {
            return View();
        }


        [Route("Privacy")]

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