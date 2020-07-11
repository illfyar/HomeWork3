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
            EmailsDataContext emails = new EmailsDataContext();
            IQueryable<Email> emailsQuery = from c in emails.Email select c;
            return new ObservableCollection<Email>(emailsQuery.ToList());
        }
        public void InsertToMass_Send(ObservableCollection<Mass_Send> mass_SendList, Message message)
        {
            EmailsDataContext emails = new EmailsDataContext();
            InsertToMessage(message);
            var groupSends = mass_SendList.GroupBy(send => send.Date_Send);
            foreach (var mass_SendGroupItem in groupSends)
            {
                int idGroup = LastIdMass_Send();
                idGroup = idGroup == 0 ? 1 : idGroup + 1;
                List<Mass_Send> mass_SendsGroupList = mass_SendGroupItem.ToList<Mass_Send>();
                mass_SendsGroupList.ForEach(x => { x.id = idGroup; x.Message = message; });
                emails.Mass_Send.InsertAllOnSubmit(mass_SendsGroupList);
                emails.SubmitChanges();
            }                           
        }
        public void InsertToMessage(Message message)
        {
            EmailsDataContext emails = new EmailsDataContext();
            emails.Message.InsertOnSubmit(message);
            emails.SubmitChanges();
        }
        public int LastIdMass_Send()
        {
            EmailsDataContext emails = new EmailsDataContext();
            IQueryable<int> mass_Send = from c in emails.Mass_Send select c.id;
            return mass_Send.Max();
        }
    }
}
