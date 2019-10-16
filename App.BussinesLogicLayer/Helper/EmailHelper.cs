using System;
using System.Net;
using SmtpClient = System.Net.Mail.SmtpClient;

namespace App.BussinesLogicLayer.Helper
{
    public class EmailHelper
    {
       
        public string SendEmail(string inputEmail, string subject, string body)
        {
            string returnString = "";
            try
            {
                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("appanuitextest@gmail.com", "AppAnuitexTest123!"),
                    EnableSsl = true
                };
                client.Send("appanuitextest@gmail.com",inputEmail, subject, body);
            }
            catch (Exception ex)
            {
                returnString = "Error: " + ex.ToString();
            }
            return returnString;
        }
    }
}
