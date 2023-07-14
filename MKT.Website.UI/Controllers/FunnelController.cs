using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit.Text;
using MimeKit;
using MKT.Website.Data;
using MKT.Website.Models;

namespace MKT.Website.UI.Controllers
{
    public class FunnelController : Controller
    {

        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _configuration;

        public FunnelController(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        public IActionResult BuildAppGuide()
        {
            return PartialView("BuildAppGuideForm");
        }

        public IActionResult Error()
        {
            return PartialView("BuildAppGuideForm");
        }
        public IActionResult BuildAppGuideConfirmation()
        {
            return PartialView("BuildAppGuideConfirmation");
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Person obj)
        {
            //Custom Validation
            if (obj.PersonName == obj.PersonEmail)
            {
                ModelState.AddModelError("PersonName", "The Person Name cannot be exactly match the Person email");
            }
            if (ModelState.IsValid) //Server Validation
            {
                _db.Persons.Add(obj);
                _db.SaveChanges();
                if (obj.PersonEmail is not null && obj.PersonName is not null)
                {
                    _ = SendBuildAppGuideByEmail(obj.PersonEmail, obj.PersonName);
                }

                return Ok();
            }
            return View(obj);
        }

        #region Email
        public async Task<IActionResult> SendBuildAppGuideByEmail(string ToEmailAddress, string ToName)
        {
            // create email message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_configuration.GetValue<string>("EmailConfiguration:From")));
            email.To.Add(MailboxAddress.Parse(ToEmailAddress));
            email.Bcc.Add(MailboxAddress.Parse(_configuration.GetValue<string>("EmailConfiguration:From")));

            email.Subject = _configuration.GetValue<string>("BuildAppGuideEmail:Subject");
            email.Body = new TextPart(TextFormat.Plain) { Text = _configuration.GetValue<string>("BuildAppGuideEmail:Body") };

            // send email
            using (var smtpClient = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    await smtpClient.ConnectAsync(_configuration.GetValue<string>("EmailConfiguration:SmtpServer"), _configuration.GetValue<int>("EmailConfiguration:Port"), SecureSocketOptions.StartTls);
                    await smtpClient.AuthenticateAsync(_configuration.GetValue<string>("EmailConfiguration:From"), _configuration.GetValue<string>("EmailConfiguration:Password"));
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

    }
}
