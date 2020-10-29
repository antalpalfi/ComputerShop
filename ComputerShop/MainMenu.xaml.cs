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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ComputerShop
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public Personeelslid loggedInPerson { get; set; }
        public MainMenu(Personeelslid selectUser)
        {

            InitializeComponent();
            loggedInPerson = selectUser;
            DispatcherTimer timeNow = new DispatcherTimer();
            timeNow.Tick += new EventHandler(UpdateTimer_Tick);
            timeNow.Interval = new TimeSpan(0, 0, 1);
            timeNow.Start();
            chekUserType();
        }
       

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            txtDateTime.Text = DateTime.Now.ToString("g");
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.ShowDialog();
        }

        private void btnEcs_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void btnDatamanegement_Click(object sender, RoutedEventArgs e)
        {
            DataManagement myDataManagement = new DataManagement(loggedInPerson);
            this.Close();
            myDataManagement.ShowDialog();
        }
        private void chekUserType()
        {
            if (loggedInPerson.Usertype == "Seller")
            {
                btnDatamanegement.IsEnabled = false;
            }
        }

        private void btnOrder_Click_1(object sender, RoutedEventArgs e)
        {
           Shopping myshopping = new Shopping(loggedInPerson);
           this.Close();
           myshopping.ShowDialog();
        }

        private void btnOverwiew_Click(object sender, RoutedEventArgs e)
        {
            Overwiew myOverwiew = new Overwiew(loggedInPerson);
            this.Close();
            myOverwiew.ShowDialog();
        }
    }
}
