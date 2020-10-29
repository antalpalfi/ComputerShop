using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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
                if (chekText(true))
                {
                    do
                    {
                        try
                        {
                            bool uniekPerson = ctx.Personeelslids.Where(x => x.Username == txtUsername.Text).Count() == 0 ? true : false;
                            if (uniekPerson)
                            {
                                ctx.Personeelslids.Add(new Personeelslid()
                                {
                                    Achternaam = txtLastname.Text,
                                    Voornaam = txtFirstname.Text,
                                    Username = txtUsername.Text,
                                    Password = txtPassword.Text,
                                    Usertype = cmbUsertype.SelectedItem.ToString()
                                });

                                MessagBoxInfo.Show($"{txtUsername.Text} successfully created", MessagBoxInfo.CmessageBoxTitle.Info);
                                ctx.SaveChanges();
                                updateTheList();
                                hideEditform();
                            }
                            else MessagBoxInfo.Show("The user already exists", MessagBoxInfo.CmessageBoxTitle.Warning);
                        }
                        catch
                        {
                            MessagBoxInfo.Show("Something wrong", MessagBoxInfo.CmessageBoxTitle.Error);
                        }
                    } while (false);
                }
                else MessagBoxInfo.Show("Something missing", MessagBoxInfo.CmessageBoxTitle.Error);
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
                if (chekText(true))
                {
                    do
                    {
                        try
                        {
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
                                hideEditform();
                            }
                        }
                        catch
                        {
                            MessagBoxInfo.Show("Something wrong", MessagBoxInfo.CmessageBoxTitle.Error);
                        }
                    } while (false);
                }
                else MessagBoxInfo.Show("Something missing", MessagBoxInfo.CmessageBoxTitle.Error);
            }
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
                    cmbUsertype.SelectedItem = select.Usertype;
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
            if (listViewPersonLid.SelectedValue != null)
            {

                using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
                {
                    var bestelingperson = ctx.Bestellings.Select(x => x).ToList();
                    if (bestelingperson.Contains(listViewPersonLid.SelectedValue))
                    {
                        var selectPerson = ctx.Personeelslids.Select(x => x).Where(x => x.ID == (int)listViewPersonLid.SelectedValue).FirstOrDefault();
                        var selectOrde = ctx.Bestellings.Select(x => x).Where(x => x.PersoneelslidID == selectPerson.ID).FirstOrDefault();
                        var selectOrderProduct = ctx.BestellingProducts.Select(x => x).Where(x => x.BestellingID == selectOrde.ID).FirstOrDefault();
                        System.Windows.Forms.DialogResult result = MyMessageBox.Show("Are you sure you want to delete the user and all the datas and orders?", MyMessageBox.CMessageBoxButton.Yes, MyMessageBox.CMessageBoxButton.No);
                        if (result == System.Windows.Forms.DialogResult.Yes)
                        {
                            var ord = ctx.BestellingProducts.RemoveRange(ctx.BestellingProducts.Where(x => x.BestellingID == selectOrderProduct.BestellingID));
                            var order = ctx.Bestellings.RemoveRange(ctx.Bestellings.Where(x => x.PersoneelslidID == selectPerson.ID));
                            var person = ctx.Personeelslids.RemoveRange(ctx.Personeelslids.Where(x => x.ID == (int)listViewPersonLid.SelectedValue)).FirstOrDefault();
                            MessagBoxInfo.Show(selectPerson.Voornaam + " " + selectPerson.Achternaam + " " + "successfully deleted", MessagBoxInfo.CmessageBoxTitle.Info);
                            ctx.SaveChanges();
                        }
                        else
                        {
                            System.Windows.Forms.DialogResult result2 = MyMessageBox.Show("Would you like to delete only the person", MyMessageBox.CMessageBoxButton.Yes, MyMessageBox.CMessageBoxButton.No);
                            if (result == System.Windows.Forms.DialogResult.Yes)
                            {
                                var person = ctx.Personeelslids.RemoveRange(ctx.Personeelslids.Where(x => x.ID == (int)listViewPersonLid.SelectedValue)).FirstOrDefault();
                                ctx.SaveChanges();
                            }
                        }
                    }
                    else
                    {
                        System.Windows.Forms.DialogResult result2 = MyMessageBox.Show("Are you sure you want to delete the user?", MyMessageBox.CMessageBoxButton.Yes, MyMessageBox.CMessageBoxButton.No);
                        if (result2 == System.Windows.Forms.DialogResult.Yes)
                        {
                            var person = ctx.Personeelslids.RemoveRange(ctx.Personeelslids.Where(x => x.ID == (int)listViewPersonLid.SelectedValue)).FirstOrDefault();
                            ctx.SaveChanges();
                        }
                    }
                }
                updateTheList();
                hideEditform();
            }
        }
        private bool chekText(bool noerror)
        {
            if (txtLastname.Text != string.Empty && txtFirstname.Text != string.Empty && txtUsername.Text != string.Empty && txtPassword.Text != string.Empty && cmbUsertype.Text != string.Empty)
            {
                noerror = true;
            }
            else noerror = false;
            return noerror;
        }
    }
}
