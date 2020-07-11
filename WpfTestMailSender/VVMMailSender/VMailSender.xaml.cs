using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfTestMailSender
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        VMMailSender VMMailSender { get; set; } = new VMMailSender();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = VMMailSender;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((ComboBox)sender).SelectedItem is Email)
            {
                VMMailSender.SelectedMass_Send.Email = (Email)((ComboBox)sender).SelectedItem;
            }            
        }

        private void CBAdressMassSend_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
