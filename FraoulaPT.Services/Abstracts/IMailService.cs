using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraoulaPT.Services.Abstracts
{
    public interface IMailService
    {
        /// <summary>
        /// Sends an email asynchronously.
        /// </summary>
        /// <param name="toEmail">Recipient's email address.</param>
        /// <param name="subject">Subject of the email.</param>
        /// <param name="body">Body content of the email.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task SendAsync(string toEmail, string subject, string body);

    }
}
