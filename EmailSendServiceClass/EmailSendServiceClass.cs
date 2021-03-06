﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace WpfTestMailSender
{
    static class EmailSendServiceClass
    {
        public static void Send(Letter letter, Settings settings)
        {
            if (letter.MassSend)
            {
                foreach (string Recipient in letter.RecipientAdressList)
                {
                SendLetter(Recipient);
                }
            }
            else
            {
                SendLetter(letter.RecipientAdress);
            }
            void SendLetter(string Recipient)
            {
                MailMessage mailMessage = GetMailMessage(letter, Recipient);
                mailMessage.IsBodyHtml = false;
                SmtpClient smtpClient = GetSmtpClient(settings);
                smtpClient.Send(mailMessage);
            }
        }
        private static MailMessage GetMailMessage(Letter letter,string recipient)
        {
            return new MailMessage(letter.SenderAdress, recipient, letter.Subject, letter.BodyMessage);
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
