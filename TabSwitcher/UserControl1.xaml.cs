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

namespace TabSwitcher
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class TabSwitcherControl : UserControl
    {
        public event RoutedEventHandler btnNextClick;
        public event RoutedEventHandler btnPreviousClick;
        public TabSwitcherControl()
        {
            InitializeComponent();
        }
        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            btnNextClick?.Invoke(sender, e);
        }
        private void BtnPrev_Click(object sender, RoutedEventArgs e)
        {
            btnPreviousClick?.Invoke(sender, e);
        }
        public static readonly DependencyProperty PrevTextProperty =
            DependencyProperty.Register("PrevText", typeof(string),
                typeof(TabSwitcherControl), new PropertyMetadata("", new PropertyChangedCallback(PrevTextChanged)));

        public static readonly DependencyProperty NextTextProperty =
    DependencyProperty.Register("NextText", typeof(string),
        typeof(TabSwitcherControl), new PropertyMetadata("", new PropertyChangedCallback(NextTextChanged)));

        private static void PrevTextChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            TextBlock textBlock = (TextBlock)(sender as UserControl).FindName("TBPrevText");
            if (textBlock != null)
            {
                textBlock.Text = e.NewValue.ToString();
            }
        }
        private static void NextTextChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            TextBlock textBlock = (TextBlock)(sender as UserControl).FindName("TBNextText");
            if (textBlock != null)
            {
                textBlock.Text = e.NewValue.ToString();
            }
        }
        public string PrevText
        {
            get
            {
                return (string)GetValue(PrevTextProperty);
            }
            set
            {
                SetValue(PrevTextProperty, value);
            }
        }
        public string NextText
        {
            get
            {
                return (string)GetValue(NextTextProperty);
            }
            set
            {
                SetValue(NextTextProperty, value);
            }
        }
    }
}
