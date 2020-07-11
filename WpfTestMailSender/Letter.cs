using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace WpfTestMailSender
{
    class Letter
    {
        public string RecipientAdress { get; set; }
        public List<String> RecipientAdressList { get; set; }
        public string SenderAdress { get; set; }
        public string Subject { get; set; }
        public string BodyMessage { get; set; }
        public bool MassSend { get; set; } = false;
    }
}
