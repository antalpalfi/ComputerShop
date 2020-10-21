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

namespace ComputerShop
{
    /// <summary>
    /// Interaction logic for PersonMembersUserControl1.xaml
    /// </summary>
    public partial class PersonMembersUserControl1 : UserControl
    {
        public PersonMembersUserControl1()
        {
            InitializeComponent();
            updateTheList();
            cmbUsertype.Items.Add("Administrator");
            cmbUsertype.Items.Add("Storekeeper");
            cmbUsertype.Items.Add("Seller");
        }
        private void updateTheList()
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                var personLid = ctx.Personeelslids.Select(x => x);
                listViewPersonLid.SelectedValuePath = "ID";
                listViewPersonLid.ItemsSource = personLid.ToList();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            listViewUser.Height = new GridLength(350, GridUnitType.Star);
            editRow.Height = new GridLength(200, GridUnitType.Star);
            stackEdit.Visibility = Visibility.Visible;
            listViewPersonLid.IsEnabled = false;
            btnSave.Visibility = Visibility.Hidden;
            btnAddNew.Visibility = Visibility.Visible;
        }

        private void btnAddNew_Click(object sender, RoutedEventArgs e)
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                ctx.Personeelslids.Add(new Personeelslid()
                {
                    Achternaam = txtLastname.Text,
                    Voornaam = txtFirstname.Text,
                    Username = txtUsername.Text,
                    Password = txtPassword.Text,
                    Usertype = cmbUsertype.SelectedItem.ToString()
                });
                ctx.SaveChanges();
                updateTheList();
                hideEditform();
            }
        }
        private void hideEditform()
        {
            listViewUser.Height = new GridLength(550, GridUnitType.Star);
            editRow.Height = new GridLength(0, GridUnitType.Star);
            stackEdit.Visibility = Visibility.Hidden;
            listViewPersonLid.IsEnabled = true;
        }

        private void btnEsc_Click(object sender, RoutedEventArgs e)
        {
            hideEditform();
        }
    }
}
