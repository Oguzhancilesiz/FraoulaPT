using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FraoulaPT.Services.Abstracts;

namespace FraoulaPT.Services.Concrete
{
    public class MailService : IMailService
    {
        private readonly SmtpClient _smtpClient;
        private readonly string _fromEmail;
        private readonly string _fromName;

        public MailService(IConfiguration config)
        {
            var smtpSection = config.GetSection("SmtpSettings");
            _fromEmail = smtpSection["FromEmail"];
            _fromName = smtpSection["FromName"];

            _smtpClient = new SmtpClient
            {
                Host = smtpSection["Host"],
                Port = int.Parse(smtpSection["Port"]),
                EnableSsl = bool.Parse(smtpSection["EnableSsl"]),
                Credentials = new NetworkCredential(
                        smtpSection["Username"],
                        smtpSection["Password"]
                )
            };
        }

        public async Task SendAsync(string toEmail, string subject, string body)
        {
            var message = new MailMessage();
            message.From = new MailAddress(_fromEmail, _fromName);
            message.To.Add(new MailAddress(toEmail));
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            await _smtpClient.SendMailAsync(message);
        }
    }
}
