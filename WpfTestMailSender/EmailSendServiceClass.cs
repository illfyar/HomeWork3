using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace WpfTestMailSender
{
    static class EmailSendServiceClass
    {
        public static void Send(ObservableCollection<Letter> letters, Settings settings)
        {
            foreach (Letter letter in letters)
            {
                MailMessage mailMessage = GetMailMessage(letter);
                mailMessage.IsBodyHtml = false;
                SmtpClient smtpClient = GetSmtpClient(settings);
                //smtpClient.Send(mailMessage); отправка не работает, порты у провайдера закрыты
                }
        }
        private static MailMessage GetMailMessage(Letter letter)
        {
            return new MailMessage(letter.SenderEmail.Adress, letter.RecipientEmail.Adress, letter.Message.subject, letter.Message.text);
        }
        private static SmtpClient GetSmtpClient(Settings settings)
        {
            SmtpClient smtpClient = new SmtpClient(settings.Server, settings.Port);
            smtpClient.EnableSsl = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(settings.User, settings.Password);
            return smtpClient;
        }
    }
}
