using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ComputerShop
{
    /// <summary>
    /// Interaction logic for CustomerUserControl.xaml
    /// </summary>
    public partial class CustomerUserControl : UserControl
    {
        public Personeelslid loggedInUser { get; set; }
        public CustomerUserControl(Personeelslid loggedInPerson)
        {
            InitializeComponent();
            loggedInUser = loggedInPerson;
            UpdateListView();
            if (loggedInUser.Usertype == "Storekeeper")
            {
                gridCustomer.Visibility = Visibility.Hidden;
            }
            listViewCustomer.IsEnabled = true;
            editCus.Height = new GridLength(0, GridUnitType.Star);
            viewList.Height = new GridLength(543, GridUnitType.Star);
            editCustomer.Visibility = Visibility.Hidden;
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                if (listViewCustomer.SelectedValue != null)
                {
                    var selectedKlant= ctx.Klants.Select(x=>x).Where(x => x.ID == (int)listViewCustomer.SelectedValue).FirstOrDefault();
                    var besteling = ctx.Bestellings.Select(x => x).ToList();
                    if (besteling.Contains(listViewCustomer.SelectedValue))
                    {
                        var besteling2 = ctx.Bestellings.Select(x => x).Where(x => x.KlantID == selectedKlant.ID).FirstOrDefault();
                        System.Windows.Forms.DialogResult result = MyMessageBox.Show("Are you sure you want to delete and all data with it?", MyMessageBox.CMessageBoxButton.Yes, MyMessageBox.CMessageBoxButton.No);
                        if (result == System.Windows.Forms.DialogResult.Yes)
                        {
                            var bestelingProd = ctx.BestellingProducts.RemoveRange(ctx.BestellingProducts.Where(x => x.BestellingID == besteling2.ID));
                            var bestelinRemove = ctx.Bestellings.RemoveRange(ctx.Bestellings.Where(x => x.KlantID == selectedKlant.ID));
                            var klant = ctx.Klants.RemoveRange(ctx.Klants.Where(x => x.ID == (int)listViewCustomer.SelectedValue)).FirstOrDefault();
                            ctx.SaveChanges();
                            UpdateListView();
                            MessagBoxInfo.Show("Customer successfully deleted", MessagBoxInfo.CmessageBoxTitle.Info);
                        }
                    }
                    else
                    {
                        System.Windows.Forms.DialogResult result = MyMessageBox.Show("Are you sure you want to delete?", MyMessageBox.CMessageBoxButton.Yes, MyMessageBox.CMessageBoxButton.No);
                        if (result == System.Windows.Forms.DialogResult.Yes)
                        {
                            var klant2 = ctx.Klants.RemoveRange(ctx.Klants.Where(x => x.ID == (int)listViewCustomer.SelectedValue)).FirstOrDefault();
                            ctx.SaveChanges();
                            UpdateListView();
                            MessagBoxInfo.Show("Customer successfully deleted", MessagBoxInfo.CmessageBoxTitle.Info);
                        }
                    }
                }
            }
        }
        private void UpdateListView()
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                var cusList = ctx.Klants.Select(x => x);
                listViewCustomer.SelectedValuePath = "ID";
                listViewCustomer.ItemsSource = cusList.ToList();

            }
        }

        private void btnEsc_Click(object sender, RoutedEventArgs e)
        {
            listViewCustomer.IsEnabled = true;
            editCus.Height = new GridLength(0, GridUnitType.Star);
            viewList.Height = new GridLength(543, GridUnitType.Star);
            editCustomer.Visibility = Visibility.Hidden;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            listViewCustomer.IsEnabled = true;
            btnSave.Visibility = Visibility.Visible;
            btnAddSave.Visibility = Visibility.Hidden;
            editCus.Height = new GridLength(271, GridUnitType.Star);
            viewList.Height = new GridLength(277, GridUnitType.Star);
            editCustomer.Visibility = Visibility.Visible;
            fillEditForm();
        }
        private void fillEditForm()
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                if (listViewCustomer.SelectedValue != null)
                {
                    var selectKlant = ctx.Klants.Select(y => y).Where(x => x.ID == (int)listViewCustomer.SelectedValue).FirstOrDefault();
                    txtFirstName.Text = selectKlant.Voornaam.ToString();
                    txtLastName.Text = selectKlant.Achternaam.ToString();
                    txtEmail.Text = selectKlant.Emailadres.ToString();
                    txtHouseNumber.Text = selectKlant.Huisnummer.ToString();
                    txtMailbox.Text = selectKlant.Bus.ToString();
                    txtPhone.Text = selectKlant.Telefoonnummer.ToString();
                    txtStreet.Text = selectKlant.Straatnaam.ToString();
                    txtTown.Text = selectKlant.Gemeente.ToString();
                    txtZipcode.Text = selectKlant.Postcode.ToString();
                    txtComment.Text = selectKlant.Opmerking.ToString();
                    DateTimePicker.SelectedDate = selectKlant.AangemaaktOp;
                }
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            listViewCustomer.IsEnabled = false;
            btnAddSave.Visibility = Visibility.Visible;
            btnSave.Visibility = Visibility.Hidden;
            editCus.Height = new GridLength(271, GridUnitType.Star);
            viewList.Height = new GridLength(277, GridUnitType.Star);
            editCustomer.Visibility = Visibility.Visible;
            clearTxtfield();
        }
        private void clearTxtfield()
        {
            txtFirstName.Text = String.Empty;
            txtLastName.Text = String.Empty;
            txtEmail.Text = String.Empty;
            txtHouseNumber.Text = String.Empty;
            txtMailbox.Text = String.Empty;
            txtPhone.Text = String.Empty;
            txtStreet.Text = String.Empty;
            txtTown.Text = String.Empty;
            txtZipcode.Text = String.Empty;
            txtComment.Text = String.Empty;
        }

        private void listViewCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            fillEditForm();
        }

        private void btnAddSave_Click(object sender, RoutedEventArgs e)
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                if (chekErrorsText(true))
                {
                    do
                    {
                        try
                        {
                            ctx.Klants.Add(new Klant()
                            {
                                Voornaam = txtFirstName.Text,
                                Achternaam = txtLastName.Text,
                                Straatnaam = txtStreet.Text,
                                Huisnummer = Convert.ToInt32(txtHouseNumber.Text),
                                Bus = Convert.ToInt32(txtMailbox.Text),
                                Postcode = Convert.ToInt32(txtZipcode.Text),
                                Gemeente = txtTown.Text,
                                Telefoonnummer = txtPhone.Text,
                                Emailadres = txtEmail.Text,
                                AangemaaktOp = Convert.ToDateTime(DateTimePicker.Text),
                                Opmerking = txtComment.Text
                            });
                            var klant = ctx.Klants.Select(x => x).Where(y => y.Voornaam == txtFirstName.Text && y.Achternaam == txtLastName.Text).Count();
                            if (klant == 0)
                            {
                                MessagBoxInfo.Show("New Customer", MessagBoxInfo.CmessageBoxTitle.Info);
                                ctx.SaveChanges();
                                UpdateListView();
                                hidePanel();
                            }
                            else MessagBoxInfo.Show("De customer alredy exist", MessagBoxInfo.CmessageBoxTitle.Error);
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

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (chekErrorsText(true))
            {
                do
                {
                    try
                    {
                        using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
                        {
                            var klant = ctx.Klants.Select(x => x).Where(x => x.ID == (int)listViewCustomer.SelectedValue).FirstOrDefault();

                            klant.Voornaam = txtFirstName.Text;
                            klant.Achternaam = txtLastName.Text;
                            klant.Straatnaam = txtStreet.Text;
                            klant.Huisnummer = Convert.ToInt32(txtHouseNumber.Text);
                            klant.Bus = Convert.ToInt32(txtMailbox.Text);
                            klant.Postcode = Convert.ToInt32(txtZipcode.Text);
                            klant.Gemeente = txtTown.Text;
                            klant.Telefoonnummer = txtPhone.Text;
                            klant.Emailadres = txtEmail.Text;
                            klant.AangemaaktOp = Convert.ToDateTime(DateTimePicker.Text);
                            klant.Opmerking = txtComment.Text;
                            System.Windows.Forms.DialogResult result = MyMessageBox.Show("Are you sure you want to change it?", MyMessageBox.CMessageBoxButton.Yes, MyMessageBox.CMessageBoxButton.No);
                            if (result == System.Windows.Forms.DialogResult.Yes)
                            {
                                ctx.SaveChanges();
                                MessagBoxInfo.Show("Succesful", MessagBoxInfo.CmessageBoxTitle.Info);
                            }
                            UpdateListView();
                            hidePanel();
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

        private void listViewCustomer_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            fillEditForm();
        }
        private bool chekErrorsText(bool noerror)
        {
            if (txtFirstName.Text != string.Empty && txtLastName.Text != string.Empty && txtStreet.Text != string.Empty &&
                txtHouseNumber.Text != string.Empty && txtMailbox.Text != string.Empty && txtZipcode.Text != string.Empty &&
                txtTown.Text != string.Empty && txtPhone.Text != string.Empty && txtEmail.Text != string.Empty &&
                txtComment.Text != string.Empty && DateTimePicker.Text != string.Empty)
            {
                noerror = true;
            }
            else noerror = false;
            return noerror;
        }
        private void hidePanel()
        {
            listViewCustomer.IsEnabled = true;
            editCus.Height = new GridLength(0, GridUnitType.Star);
            viewList.Height = new GridLength(543, GridUnitType.Star);
            editCustomer.Visibility = Visibility.Hidden;
        }
    }
}
