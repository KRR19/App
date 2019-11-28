using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;

namespace App.BussinesLogicLayer.Helper
{
    public class EmailHelper
    {
        public IConfiguration Configuration { get; }

        public EmailHelper(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public string SendEmail(string inputEmail, string subject, string body)
        {
            string returnString = string.Empty;
            string smtp = Configuration.GetSection("Email")["smtp"];
            int port = Convert.ToInt32(Configuration.GetSection("Email")["port"]);
            string user = Configuration.GetSection("Email")["user"];
            string psw = Configuration.GetSection("Email")["password"];

            try
            {
                using (var client = new SmtpClient(smtp, port))
                {
                    client.Credentials = new NetworkCredential(user, psw);
                    client.EnableSsl = true;
                    client.Send(user, inputEmail, subject, body);
                };

            }
            catch (Exception ex)
            {
                returnString = "Error: " + ex.ToString();
            }
            return returnString;
        }
    }
}
