using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeClient
{
    public class SendEmail
    {
        private string sendFromLogin;
        private string sendFromPass;
        private string sendTo;
        private string theme;
        private string msg;
        public SendEmail(string sendTo, string theme, string msg)
        {
            this.sendTo = sendTo;
            this.theme = theme;
            this.msg = msg;
            sendFromLogin = "andreymakas@inbox.ru";
            sendFromPass = "Liona32014";
            Task t = SendEmailAsync(sendTo, theme, msg);
        }

        public async Task SendEmailAsync(string mailTo, string subject, string body)
        {
            string result = await Task.Run(() =>
            {
                SmtpClient Smtp = new SmtpClient("smtp.mail.ru", 25);
                Smtp.Credentials = new NetworkCredential(sendFromLogin, sendFromPass);
                Smtp.EnableSsl = true;

                MailMessage message = new MailMessage();
                message.From = new MailAddress(sendFromLogin);
                message.To.Add(new MailAddress(mailTo));

                message.Subject = subject;
                message.Body = body;
                try
                {
                    Smtp.Send(message);
                    result = "Письмо отправлено";
                }
                catch (SmtpException ex)
                {
                    result = ex.Message;
                }
                catch (Exception ex)
                {
                    result = ex.Message;
                }

                return result;
            });
        }


    }
}
