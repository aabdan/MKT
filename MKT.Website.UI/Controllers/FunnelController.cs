using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit.Text;
using MimeKit;
using MKT.Website.Data;
using MKT.Website.Models;
using Microsoft.AspNetCore.Http;

namespace MKT.Website.UI.Controllers
{
    public enum ContactType
    {
        DownloadAppGuid,
        ContactUs,
        StartYourProject
    }
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
        public IActionResult Create(Person obj, ContactType contactType = ContactType.ContactUs)
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
                    _ = SendBuildAppGuideByEmail(obj.PersonEmail, obj.PersonName, contactType);
                }

                return Ok();
            }
            return View(obj);
        }

        #region Email
        public async Task<IActionResult> SendBuildAppGuideByEmail(string ToEmailAddress, string ToName, ContactType ContactType)
        {
            // create email message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_configuration.GetValue<string>("EmailConfiguration:From")));
            email.To.Add(MailboxAddress.Parse(ToEmailAddress));
            email.Bcc.Add(MailboxAddress.Parse(_configuration.GetValue<string>("EmailConfiguration:From")));

            switch (ContactType)
            {
                case ContactType.ContactUs:
                    email.Subject = _configuration.GetValue<string>("ContactUsEmail:Subject");
                    email.Body = new TextPart(TextFormat.Html) { Text = "Dear " + ToName + ",<br/><br/>" + _configuration.GetValue<string>("ContactUsEmail:Body") + "<br/><br/>Best Regards,<br/> Tech Nexus - Abu Dhabi" };
                    break;
                case ContactType.DownloadAppGuid:
                    email.Subject = _configuration.GetValue<string>("BuildAppGuideEmail:Subject");
                    email.Body = new TextPart(TextFormat.Html) { Text = "Dear " + ToName + ",<br/><br/>" + _configuration.GetValue<string>("BuildAppGuideEmail:Body") + "<br/><br/>Best Regards,<br/> Tech Nexus - Abu Dhabi" };
                    break;

                default:
                    email.Subject = _configuration.GetValue<string>("DefaultEmail:Subject");
                    email.Body = new TextPart(TextFormat.Html) { Text = "Dear " + ToName + ",<br/><br/>" + _configuration.GetValue<string>("DefaultEmail:Body") + "<br/><br/>Best Regards,<br/> Tech Nexus - Abu Dhabi" };
                    break;



            }

            // send email
            using (var smtpClient = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    await smtpClient.ConnectAsync(_configuration.GetValue<string>("EmailConfiguration:SmtpServer"), _configuration.GetValue<int>("EmailConfiguration:Port"), SecureSocketOptions.StartTls);
                    await smtpClient.AuthenticateAsync(_configuration.GetValue<string>("EmailConfiguration:Username"), _configuration.GetValue<string>("EmailConfiguration:Password"));
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
