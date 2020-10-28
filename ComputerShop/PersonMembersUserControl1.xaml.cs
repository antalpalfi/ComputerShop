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
        public Personeelslid loggedInUser { get; set; }
        public PersonMembersUserControl1(Personeelslid loggedInPerson)
        {
            InitializeComponent();
            loggedInUser = loggedInPerson;
            if (loggedInUser.Usertype == "Storekeeper")
            {
                btnAdd.Visibility = Visibility.Hidden;
                btnRemove.Visibility = Visibility.Hidden;
                btnEdit.Visibility = Visibility.Hidden;
            }
            updateTheList();
            hideEditform();
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
            clearTextBox();
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

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            listViewUser.Height = new GridLength(350, GridUnitType.Star);
            editRow.Height = new GridLength(200, GridUnitType.Star);
            stackEdit.Visibility = Visibility.Visible;
            listViewPersonLid.IsEnabled = true;
            btnSave.Visibility = Visibility.Visible;
            btnAddNew.Visibility = Visibility.Hidden;
            fillEditForm();

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                var select = ctx.Personeelslids.Select(x => x).Where(x => x.ID == (int)listViewPersonLid.SelectedValue).FirstOrDefault();
                select.Voornaam = txtFirstname.Text;
                select.Achternaam = txtLastname.Text;
                select.Username = txtUsername.Text;
                select.Password = txtPassword.Text;
                select.Usertype = cmbUsertype.SelectedItem.ToString();

                System.Windows.Forms.DialogResult result = MyMessageBox.Show("Are you sure you want to change it? ", MyMessageBox.CMessageBoxButton.Yes, MyMessageBox.CMessageBoxButton.No);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    MessagBoxInfo.Show("Succesful", MessagBoxInfo.CmessageBoxTitle.Info);
                    ctx.SaveChanges();
                }
            }
            hideEditform();
        }
        private void fillEditForm()
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                if (listViewPersonLid.SelectedValue != null)
                {
                    var select = ctx.Personeelslids.Select(x => x).Where(x => x.ID == (int)listViewPersonLid.SelectedValue).FirstOrDefault();
                    txtFirstname.Text = select.Voornaam;
                    txtLastname.Text = select.Achternaam;
                    txtPassword.Text = select.Password;
                    txtUsername.Text = select.Username;
                }
            }

        }
        private void clearTextBox()
        {
            txtFirstname.Text = String.Empty;
            txtLastname.Text = String.Empty;
            txtPassword.Text = String.Empty;
            txtUsername.Text = String.Empty;
            cmbUsertype.SelectedIndex = -1;
        }
        private void listViewPersonLid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            fillEditForm();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                
            }
        }
    }
}
