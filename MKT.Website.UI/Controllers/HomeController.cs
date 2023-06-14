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
                                 .Replace("/Ar", "").Replace("/Fr", "");
            if (culture.ToLower().Contains("en"))
            {
                return Redirect(returnUrl);
            }

            string languageCode = culture.Substring(0, 2);
            languageCode = languageCode.Substring(0, 1).ToUpper() + languageCode.Substring(1);
            return Redirect($"/{languageCode}{returnUrl}");
        }
        #endregion


        #region Email
        [Route("IndexAsync")]
        public async Task<IActionResult> IndexAsync()
        {
            // create email message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("info@technexus.ae"));
            email.To.Add(MailboxAddress.Parse("ahmadabdan@hotmail.com"));
            email.Subject = "MKT TechNexus Email";
            email.Body = new TextPart(TextFormat.Plain) { Text = "Email from MKT TechNexus Email" };

            // send email
            using (var smtpClient = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    await smtpClient.ConnectAsync("smtp.office365.com", 587, SecureSocketOptions.StartTls);
                    await smtpClient.AuthenticateAsync("info@technexus.ae", "");
                    await smtpClient.SendAsync(email);
                }

                catch (Exception)
                {
                    //log an error message or throw an exception, or both.
                    throw;
                }
                finally
                {
                    await smtpClient.DisconnectAsync(true);
                    smtpClient.Dispose();
                }
            }

            return View();
        }
        #endregion

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
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("ar-AE");

            return PartialView();
        }
    }
}