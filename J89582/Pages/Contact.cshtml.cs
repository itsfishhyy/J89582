using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using J89582.Model;

namespace J89582.Pages
{
    public class ContactModel : PageModel
    {
        public EmailModel Input { get; set; }
        public string Message { get; set; }

        private IConfiguration Configuration;
        public ContactModel(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            //Read SMTP settings from AppSettings.json.
            string host = this.Configuration.GetValue<string>("Smtp:Server");
            int port = this.Configuration.GetValue<int>("Smtp:Port");
            string fromAddress = this.Configuration.GetValue<string>("Smtp:FromAddress");
            string userName = this.Configuration.GetValue<string>("Smtp:UserName");
            string password = this.Configuration.GetValue<string>("Smtp:Password");

            using (MailMessage mm = new MailMessage(fromAddress, "burgerorzo@gmail.com"))
            {
                mm.Subject = Input.Subject;
                mm.Body = "Name: " + Input.Name + "<br /><br />Email: " + Input.Email + "<br />" + Input.Message;
                mm.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = host;
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential(userName, password);
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = port;
                    smtp.Send(mm);
                    this.Message = "Email sent sucessfully.";
                }
            }
            return Page();
        }
        
    }
}
