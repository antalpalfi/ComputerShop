using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ComputerShop
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
            //Console.OutputEncoding = Encoding.UTF8;
            cmbUserType.Items.Add("Administrator");
            cmbUserType.Items.Add("Storekeeper");
            cmbUserType.Items.Add("Seller");
        }

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            using (ComputerWareHousProject ctx = new ComputerWareHousProject())
            {
                ctx.Users_Password.Add(new Users_Password
                {
                    UserName = txtUserName.Text.ToString(),
                    Password = txtPassword.Password.ToString(),
                    UserType = cmbUserType.SelectedItem.ToString()
                });
                ctx.SaveChanges();
            }
            //MessageBox.Show($"New user successfully created\n" +
            //    $"{txtUserName.Text}\n" +
            //    $"{txtPassword.Password.ToString()}\n" +
            //    $"{cmbUserType.SelectedItem.ToString()}");
            DialogResult = true;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void txtUserName_GotFocus(object sender, RoutedEventArgs e)
        {
            txtUserName.Clear();
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

        private void txtUserName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtUserName.Text == "")
            {
                txtUserName.Text = "Username";
            }
        }
    }

}
