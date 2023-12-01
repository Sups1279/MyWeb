using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
namespace MyCommon
{
    public class MailHelper
    {
        public void sendMail(string toEmailAddress,string subject, string content)
        {
            var fromEmailAddress = ConfigurationManager.AppSettings["FromEmailAddress"].ToString();
            var fromEmailDisplayName = ConfigurationManager.AppSettings["FromEmailDisplayName"].ToString();
            var fromEmailPassword = ConfigurationManager.AppSettings["FromEmailPassword"].ToString();
            var stmpHost = ConfigurationManager.AppSettings["SMTPHost"].ToString() ;
            var stmpPost = ConfigurationManager.AppSettings["SMTPPost"].ToString();
            bool enableSs1 = bool.Parse(ConfigurationManager.AppSettings["EnableSSL"].ToString());

            string body = content;
            MailMessage message= new MailMessage(new MailAddress(fromEmailAddress,fromEmailDisplayName),new MailAddress(toEmailAddress));
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;
            var client = new SmtpClient();
            client.Credentials = new NetworkCredential(fromEmailAddress,fromEmailPassword);
            client.Host = stmpHost;
            client.EnableSsl = enableSs1;
            client.Port = !string.IsNullOrEmpty(stmpPost)?Convert.ToInt32(stmpPost) : 0;
            client.Send(message);
        }
    }
}
