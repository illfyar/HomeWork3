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
    }
}
