using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfTestMailSender.VVMLogWindows
{
    class VMLogWindows : INotifyPropertyChanged
    {
        private string log;
        public string Log { get; set; }
        public event EventHandler CloseLogWindows;
        public VMLogWindows(string log)
        {
            Log = log;
            LogWindow logWindow = new LogWindow();
            CloseLogWindows += (s, e) => logWindow.Close();
            logWindow.DataContext = this;
            logWindow.Show();
        }
        private MyCommands close;
        public MyCommands Close
        {
            get
            {
                return close ?? (close = new MyCommands(CloseWindow));
            }
        }
        public void CloseWindow(object obj)
        {
            CloseLogWindows(this, new EventArgs());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
