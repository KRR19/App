using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace App.BussinesLogicLayer.Helper
{
    public class EmailHelper
    {


        public async Task<bool> SendEmail(string toAdress, string subject, string body)
        {
            MailAddress from = new MailAddress("testappdel123@gmail.com");
            MailAddress to = new MailAddress(toAdress);
            MailMessage m = new MailMessage(from, to);
            m.Subject = subject;
            m.Body = body;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential(from.Address, "123testappdel123");
            smtp.EnableSsl = true;
            await smtp.SendMailAsync(m);
            return true;
        }

    }
}
