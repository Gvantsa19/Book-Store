using Microsoft.Extensions.Options;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using ZBS.Shared.EmailHelpers;

namespace ZBS.Application.Email
{
    public class EmailSend
    {
        private EmailSettings _emailSettings;

        public EmailSend(EmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
        }
        public void send(string mail, string subject, string body)
        {
            
            MailMessage message = new MailMessage();


            message.From = new MailAddress(_emailSettings.Email);
            message.Subject = subject;
            message.Body = body;
            message.To.Add(mail);

            using SmtpClient smtpClient = new SmtpClient();

            smtpClient.Port = _emailSettings.Port;
            smtpClient.Host = _emailSettings.Host;
            smtpClient.EnableSsl = _emailSettings.UseSSL;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = _emailSettings.UseDefaultCredentials;


            smtpClient.Credentials = new NetworkCredential(_emailSettings.Email, _emailSettings.Password);


            smtpClient.Send(message);

            smtpClient.Dispose();
        }
            
    
    } 
    
}
