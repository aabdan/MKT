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
    [Route("")]
    [Route("Home")]
    [Route("{lang:regex(^$|ar|en|fr$)}/{controller=Home}/{action=Main}/{id?}")]
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

        public IActionResult ChangeLanguage(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions() { Expires = DateTimeOffset.UtcNow.AddYears(1) });

            return Redirect(Request.Headers["Referer"].ToString() ?? "/");
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
        [Route("ContactUs")]

        public IActionResult ContactUs()
        {
            return PartialView("ContactUs");
        }

        [Route("Default")]
        public IActionResult Default()
        {
            return PartialView();
        }

        [Route("")]
        public IActionResult Main(string lang)
        {

            //throw new Exception("This is a test exception");


            //get culture information
            var currentCulture = Thread.CurrentThread.CurrentUICulture.Name;

            ////For example --> browserLang = 'en-US'
            // currentCulture = Request.Headers["Accept-Language"].ToString().Split(";").FirstOrDefault()?.Split(",").FirstOrDefault();

            return PartialView();
        }


        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //[Route("Error")]

        //public IActionResult Error()
        //{
        //    return PartialView(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}