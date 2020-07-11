using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace WpfTestMailSender
{
    class Settings
    {
        public string Server { get; set; }
        public int Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public Settings(string server, int port, string user, string password)
        {
            Server = server;
            Port = port;
            User = user;
            Password = password;
        }
        public Settings()
        {
        }        
    }
}
