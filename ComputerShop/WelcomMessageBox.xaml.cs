using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ComputerShop
{
    /// <summary>
    /// Interaction logic for WelcomMessageBox.xaml
    /// </summary>
    public partial class WelcomMessageBox : Window
    {
        public WelcomMessageBox()
        {
            InitializeComponent();
            MainWindow mainwindow = new MainWindow();
            txtWelcome.Text = mainwindow.loginUser;
        }
        static WelcomMessageBox welcomMessageBox;
        static DialogResult result = System.Windows.Forms.DialogResult.No;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DispatcherTimer timeNow = new DispatcherTimer();
            timeNow.Tick += new EventHandler(MyTimer_Tick);
            timeNow.Interval = new TimeSpan(0, 0, 2);
            timeNow.Start();
        }
        private void MyTimer_Tick(object sender, EventArgs e)
        {
            this.Close();
        }
        DoubleAnimation anim;
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Closing -= Window_Closing;
            e.Cancel = true;
            anim = new DoubleAnimation(0, duration: (Duration)TimeSpan.FromSeconds(0.3));
            anim.Completed += (s, _) => this.Close();
            this.BeginAnimation(UIElement.OpacityProperty, anim);
        }
        public static DialogResult Show(string message)
        {
            welcomMessageBox = new WelcomMessageBox();
            welcomMessageBox.txtWelcome.Text = message;

            
            welcomMessageBox.ShowDialog();
            return result;

        }
    }
}
