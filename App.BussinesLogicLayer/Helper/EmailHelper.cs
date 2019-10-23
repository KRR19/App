using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using SmtpClient = System.Net.Mail.SmtpClient;

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
            string returnString = "";
            try
            {
                using (var client = new SmtpClient(Configuration.GetSection("Email")["smtp"], Convert.ToInt32(Configuration.GetSection("Email")["port"])))
                {
                    client.Credentials = new NetworkCredential(Configuration.GetSection("Email")["user"], Configuration.GetSection("Email")["password"]);
                    client.EnableSsl = true;
                    client.Send(Configuration.GetSection("Email")["user"], inputEmail, subject, body);
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
