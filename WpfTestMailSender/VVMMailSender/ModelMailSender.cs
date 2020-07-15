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
            using (EmailsDataContext emails = new EmailsDataContext())
            {
                IQueryable<Email> emailsQuery = from c in emails.Email select c;
                return new ObservableCollection<Email>(emailsQuery.ToList());
            }
        }
        public void InsertToLetter(ObservableCollection<Letter> mass_SendList, Message message, Email senderEmail)
        {
            InsertToMessage(message);
            Email senderEmailNew = new Email
            {//при помещении данных в БД если не создать новый объект Email то ругается
                Id = senderEmail.Id,
                Adress = senderEmail.Adress,
                Name = senderEmail.Name
            };
            using (EmailsDataContext emails = new EmailsDataContext()) { 
                var groupSends = mass_SendList.GroupBy(send => send.Date_Send);
                foreach (var mass_SendGroupItem in groupSends)
                {
                    int idGroup = LastIdMass_Send();
                    idGroup = idGroup == 0 ? 1 : idGroup + 1;
                    List<Letter> mass_SendsGroupList = mass_SendGroupItem.ToList<Letter>();
                    mass_SendsGroupList.ForEach(x =>
                        {
                            x.id = idGroup;
                            x.Message = message;
                            x.SenderEmail = senderEmailNew;
                        });
                    emails.Letter.InsertAllOnSubmit(mass_SendsGroupList);
                    emails.SubmitChanges();
                }
            }                           
        }
        public void InsertToMessage(Message message)
        {
            using (EmailsDataContext emails = new EmailsDataContext()) 
            { 
                emails.Message.InsertOnSubmit(message);
                emails.SubmitChanges();
            }
        }
        public int LastIdMass_Send()
        {
            using (EmailsDataContext emails = new EmailsDataContext())
            {
                IQueryable<int> mass_Send = from c in emails.Letter select c.id;
                return mass_Send.Count() == 0 ? 0 : mass_Send.Max();
            }
        }
    }
}
