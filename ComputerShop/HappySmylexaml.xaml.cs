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

namespace ComputerShop
{
    /// <summary>
    /// Interaction logic for HappySmylexaml.xaml
    /// </summary>
    public partial class HappySmylexaml : Window
    {
        public Personeelslid loggedInUser { get; set; }
        public HappySmylexaml(Personeelslid loggedInPerson)
        {
            InitializeComponent();
            loggedInUser = loggedInPerson;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessagBoxInfo.Show($"Thank {loggedInUser.Voornaam + " " + loggedInUser.Achternaam} for watching the application", MessagBoxInfo.CmessageBoxTitle.Info);
            Shopping myshopping = new Shopping(loggedInUser);
            this.Close();
            myshopping.ShowDialog();

        }
    }
}
