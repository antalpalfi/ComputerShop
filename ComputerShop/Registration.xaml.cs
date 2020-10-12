using System;
using System.Collections.Generic;
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

        private static  string newPassword()
        {
            string password = null;
            bool pas = true;

            do
            {
                if (!pas)
                {
                    if (!password.Any(char.IsUpper))
                        MessageBox.Show("It does not contain capital letters.");
                    if (!password.Any(char.IsLower))
                        MessageBox.Show("It does not contain lowercase letters.");
                    if (!password.Any(char.IsDigit))
                        MessageBox.Show("It does not contain numbers.");
                    if (password.Length < 8)
                        MessageBox.Show("It's too short");
                    if (password.Length > 20)
                        MessageBox.Show("It's too long");
                    if (Regex.IsMatch(password, @"^[a-zA-Z0-9]+$"))
                        MessageBox.Show("It does not contain a strange sign");
                }
                pas = false;
            } while (!password.Any(char.IsUpper)
            || !password.Any(char.IsLower)
            || !password.Any(char.IsDigit)
            || password.Length < 8
            || password.Length > 20
            || Regex.IsMatch(password, @"^[a-zA-Z0-9]+$"));

            return password;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
    
}
