using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace AplicacionWeb.Helpers
{
    public class MailHelper
    {
        private static string smtpServer = "";
        private static int smtpPort = 25;
        private static bool useSsl = false;

        private static string smtpUser = "";
        private static string smtpPassword = "";

        private static string senderAddress = "";

        public static void SendEmail(string recipient, string subject, string htmlBody)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress(senderAddress);
                message.To.Add(recipient);
                message.Subject = subject;
                message.Body = htmlBody;
                message.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient(smtpServer, smtpPort);
                smtp.EnableSsl = useSsl;
                smtp.Credentials = new NetworkCredential(smtpUser, smtpPassword);

                smtp.Send(message);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

    }
}