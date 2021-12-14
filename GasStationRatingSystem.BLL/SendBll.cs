using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace GasStationRatingSystem.BLL
{
   public class SendBll
    {
        public bool SendMail(string subject,string name, string body,string email)
        {
            var fromAddress = new MailAddress("gasrating0@gmail.com", "Gas Station Rating");
            var toAddress = new MailAddress(email, name);
            const string fromPassword = "gomtx@123";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
                try
                {
                    smtp.Send(message);
                }
                catch
                {
                    return false;
                }
                return true;
        }
    }
}
