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
        public string loginUser = "";
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

            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                var selectUser = ctx.Personeelslids.Where(x => x.Username == txtUsersName.Text && x.Password == txtPassword.Password).Count();
                if (selectUser == 1)
                {
                    loginUser = txtUsersName.Text;
                    txtPassword.Clear();
                    txtUsersName.Clear();
                    WelcomMessageBox.Show(loginUser);
                    MainMenu mainMenu = new MainMenu();
                    mainMenu.txtUserName.Text = $"Hello {loginUser}";
                    this.Close();
                    mainMenu.ShowDialog();
                }
                else
                {
                    MessageBox.Show("This username and password cannot be found");
                }
            }
            
        }

        private void txtUsersName_GotFocus(object sender, RoutedEventArgs e)
        {
            txtUsersName.Clear();
        }

        private void txtPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            txtPassword.Clear();
        }

        private void txtPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtPassword.Password == "")
            {
                txtPassword.Password = "Password";
            }
        }

        private void txtUsersName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtUsersName.Text == "")
            {
                txtUsersName.Text = "Username";
            }
        }
    }
}
