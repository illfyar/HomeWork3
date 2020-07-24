using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTestMailSender.VVMMailSender
{
    class ModelMailSender
    {
        
        public ObservableCollection<Email> GetEmails()
        {
            using (MailsAndSendersModel context = new MailsAndSendersModel())
            {
                var Emails = context.Email;
                return new ObservableCollection<Email>(Emails.ToList());
            }
        }
        public void InsertToLetter(ObservableCollection<Letter> LetterList, Message message, Email senderEmail)
        {
            InsertToMessage(message);
            using (MailsAndSendersModel context = new MailsAndSendersModel())
            {
                var groupLetter = LetterList.GroupBy(send => send.Date_Send);
                foreach (var LetterGroupItem in groupLetter)
                {
                    int idGroup = LastIdLetter();
                    idGroup = idGroup == 0 ? 1 : idGroup + 1;
                    List<Letter> LetterGroupList = LetterGroupItem.ToList<Letter>();
                    LetterGroupList.ForEach(x =>
                        {
                            x.id = idGroup;
                            x.Message = message;
                            //x.id_Message = message.id;
                            x.SenderEmail = senderEmail;
                            //x.id_RecipientEmail = x.RecipientEmail.Id;
                        });
                    context.Letter.AddRange(LetterGroupList);
                    context.SaveChanges();
                }
            }
        }
        public void InsertToMessage(Message message)
        {
            using (MailsAndSendersModel context = new MailsAndSendersModel())
            {
                context.Message.Add(message);
                context.SaveChanges();
            }
        }
        public int LastIdLetter()
        {
            using (MailsAndSendersModel context = new MailsAndSendersModel())
            {
                return context.Letter.Select(c => c.id).Max();
            }
        }
    }
}
