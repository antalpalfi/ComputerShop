using System;
using System.Collections.Generic;
using System.Data;
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

namespace ComputerShop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            Registration newReg = new Registration();
            newReg.ShowDialog();
        }

        private void btnEsc_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {

            using (IndividueelProjectEntities1 ctx = new IndividueelProjectEntities1())
            {
                var selectUser = ctx.Users_Password.Where(x => x.UserName == txtUsersName.Text && x.Password == txtPassword.Password).Count();
                if (selectUser == 1)
                {
                    MessageBox.Show("Welkom");
                    MainMenu mainMenu = new MainMenu();
                    mainMenu.ShowDialog();
                }
                else
                {
                    MessageBox.Show("This username and password cannot be found");
                }
            };
        }
    }
}
