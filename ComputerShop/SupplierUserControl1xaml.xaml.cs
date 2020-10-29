using System;
using System.Linq;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ComputerShop
{
    /// <summary>
    /// Interaction logic for SupplierUserControl1xaml.xaml
    /// </summary>
    public partial class SupplierUserControl1xaml : UserControl
    {
        public Personeelslid loggedInUser { get; set; }
        public SupplierUserControl1xaml(Personeelslid loggedInPerson)
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
        }
        private void updateTheList()
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                var supl = ctx.Leveranciers.Select(l => l);
                supportList.SelectedValuePath = "ID";
                supportList.ItemsSource = supl.ToList();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            clearTxtfield();
            supportList.IsEnabled = false;
            btnAddSave.Visibility = Visibility.Visible;
            btnSave.Visibility = Visibility.Hidden;
            editSup.Height = new GridLength(201, GridUnitType.Star);
            viewList.Height = new GridLength(344, GridUnitType.Star);
            editSupplier.Visibility = Visibility.Visible;
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (supportList.SelectedValue != null)
            {
                using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
                {
                    var productSup = ctx.Products.Select(x => x).Where(x=>x.LeverancierID == (int)supportList.SelectedValue).FirstOrDefault();
                    var bestelSup = ctx.Bestellings.Select(x => x).ToList();
                    var bestelingPro = ctx.BestellingProducts.Select(x => x).ToList();
                    if (bestelSup.Contains(supportList.SelectedValue))
                    {
                        var selectBest = ctx.Bestellings.Select(x => x).Where(x => x.LeverancierID == (int)supportList.SelectedValue).FirstOrDefault();

                        System.Windows.Forms.DialogResult result1 = MyMessageBox.Show("Are you sure you want to delete and all data with it?", MyMessageBox.CMessageBoxButton.Yes, MyMessageBox.CMessageBoxButton.No);
                        if (result1 == System.Windows.Forms.DialogResult.Yes)
                        {
                            var bestProremove = ctx.BestellingProducts.RemoveRange(ctx.BestellingProducts.Where(x => x.BestellingID == selectBest.ID));
                            var bestRemove = ctx.Bestellings.RemoveRange(ctx.Bestellings.Where(x => x.LeverancierID == (int)supportList.SelectedValue));
                            // select product
                            //if (bestelingPro.Contains(productSup))
                            //{

                            //}
                            var selectSup = ctx.Leveranciers.RemoveRange(ctx.Leveranciers.Where(x => x.ID == (int)supportList.SelectedValue)).FirstOrDefault();
                            ctx.SaveChanges();
                            updateTheList();
                        }
                    }
                    var selec = ctx.Leveranciers.RemoveRange(ctx.Leveranciers.Where(x => x.ID == (int)supportList.SelectedValue)).FirstOrDefault();
                    System.Windows.Forms.DialogResult result = MyMessageBox.Show("Are you sure you want to delete and all data with it?", MyMessageBox.CMessageBoxButton.Yes, MyMessageBox.CMessageBoxButton.No);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        ctx.SaveChanges();
                        updateTheList();
                    }
                }
            }

        }
        private void btnEdit_Click_1(object sender, RoutedEventArgs e)
        {
            supportList.IsEnabled = true;
            btnSave.Visibility = Visibility.Visible;
            btnAddSave.Visibility = Visibility.Hidden;
            editSup.Height = new GridLength(201, GridUnitType.Star);
            viewList.Height = new GridLength(344, GridUnitType.Star);
            editSupplier.Visibility = Visibility.Visible;
            fillEditForm();
        }

        private void supportList_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show(supportList.SelectedValue.ToString());
        }
        private void fillEditForm()
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                if (supportList.SelectedValue != null)
                {
                    var selectSup = ctx.Leveranciers.Select(y => y).Where(x => x.ID == (int)supportList.SelectedValue).FirstOrDefault();
                    txtCompany.Text = selectSup.Company.ToString();
                    txtContactPerson.Text = selectSup.Contactpersoon.ToString();
                    txtEmail.Text = selectSup.Emailadres.ToString();
                    txtHouseNumber.Text = selectSup.Huisnummer.ToString();
                    txtMailbox.Text = selectSup.Bus.ToString();
                    txtPhone.Text = selectSup.Telefoonnummer.ToString();
                    txtStreet.Text = selectSup.Straatnaam.ToString();
                    txtTown.Text = selectSup.Gemeente.ToString();
                    txtZipcode.Text = selectSup.Postcode.ToString();
                }
            }
        }

        private void supportList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            fillEditForm();
        }

        private void btnEsc_Click(object sender, RoutedEventArgs e)
        {
            supportList.IsEnabled = true;
            editSup.Height = new GridLength(0, GridUnitType.Star);
            viewList.Height = new GridLength(543, GridUnitType.Star);
            editSupplier.Visibility = Visibility.Hidden;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (chekText(true))
            {
                do
                {
                    try
                    {
                        using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
                        {
                            var sup = ctx.Leveranciers.Select(x => x).Where(y => y.ID == (int)supportList.SelectedValue).FirstOrDefault();
                            sup.Contactpersoon = txtContactPerson.Text;
                            sup.Company = txtCompany.Text;
                            sup.Telefoonnummer = txtPhone.Text;
                            sup.Emailadres = txtEmail.Text;
                            sup.Straatnaam = txtStreet.Text;
                            sup.Huisnummer = Convert.ToInt32(txtHouseNumber.Text);
                            sup.Bus = Convert.ToInt32(txtMailbox.Text);
                            sup.Postcode = Convert.ToInt32(txtZipcode.Text);
                            sup.Gemeente = txtTown.Text;
                            System.Windows.Forms.DialogResult result = MessagBoxInfo.Show("Are you sure you want to change it?", MessagBoxInfo.CmessageBoxTitle.Info);
                            if (result == System.Windows.Forms.DialogResult.OK)
                            {
                                MessagBoxInfo.Show("Succesful", MessagBoxInfo.CmessageBoxTitle.Info);
                                ctx.SaveChanges();
                                updateTheList();
                                hidePanel();
                            }
                        }
                    }
                    catch (Exception)
                    {
                        MessagBoxInfo.Show("Something wrong", MessagBoxInfo.CmessageBoxTitle.Warning);
                    }
                } while (false);
            }
            else MessagBoxInfo.Show("Something missing", MessagBoxInfo.CmessageBoxTitle.Warning);
        }

        private void btnAddSave_Click(object sender, RoutedEventArgs e)
        {
            if (chekText(true))
            {
                do
                {
                    using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
                    {
                        try
                        {
                            var sup = ctx.Leveranciers.Where(x => x.Company == txtCompany.Text && x.Contactpersoon == txtContactPerson.Text).Count();

                            if (sup == 0)
                            {
                                ctx.Leveranciers.Add(new Leverancier()
                                {
                                    Contactpersoon = txtContactPerson.Text,
                                    Company = txtCompany.Text,
                                    Telefoonnummer = txtPhone.Text,
                                    Emailadres = txtEmail.Text,
                                    Straatnaam = txtStreet.Text,
                                    Huisnummer = Convert.ToInt32(txtHouseNumber.Text),
                                    Bus = Convert.ToInt32(txtMailbox.Text),
                                    Postcode = Convert.ToInt32(txtZipcode.Text),
                                    Gemeente = txtTown.Text
                                });
                                string newSuppplier = $"{txtCompany.Text}\n" +
                                    $"{txtContactPerson.Text}\n" +
                                    $"{txtPhone.Text}\n" +
                                    $"{txtEmail.Text}\n" +
                                    $"{txtStreet.Text} {txtHouseNumber.Text} {txtMailbox.Text}\n" +
                                    $"{txtZipcode.Text}\n" +
                                    $"{txtTown.Text}";
                                System.Windows.Forms.DialogResult result = MessagBoxInfo.Show(newSuppplier, MessagBoxInfo.CmessageBoxTitle.Info);
                                if (result == System.Windows.Forms.DialogResult.OK)
                                {
                                    ctx.SaveChanges();
                                    updateTheList();
                                    hidePanel();
                                }
                            }
                            else
                            {
                                MessagBoxInfo.Show("Company is already exist", MessagBoxInfo.CmessageBoxTitle.Error);
                            }
                        }
                        catch (Exception)
                        {
                            MessagBoxInfo.Show("Something wrong", MessagBoxInfo.CmessageBoxTitle.Warning);
                        }
                    }
                } while (false);
            }
            else MessagBoxInfo.Show("Something missing", MessagBoxInfo.CmessageBoxTitle.Warning);
        }
        private void clearTxtfield()
        {
            txtCompany.Text = String.Empty;
            txtContactPerson.Text = String.Empty;
            txtEmail.Text = String.Empty;
            txtHouseNumber.Text = String.Empty;
            txtMailbox.Text = String.Empty;
            txtPhone.Text = String.Empty;
            txtStreet.Text = String.Empty;
            txtTown.Text = String.Empty;
            txtZipcode.Text = String.Empty;
        }
        private bool chekText(bool noerror)
        {
            if (txtContactPerson.Text != string.Empty && txtCompany.Text != string.Empty && txtPhone.Text != string.Empty &&
                txtEmail.Text != string.Empty && txtStreet.Text != string.Empty && txtHouseNumber.Text != string.Empty &&
                txtMailbox.Text != string.Empty && txtZipcode.Text != string.Empty && txtTown.Text != string.Empty)
            {
                noerror = true;
            }
            else noerror = false;
            return noerror;
        }
        private void hidePanel()
        {
            supportList.IsEnabled = true;
            editSup.Height = new GridLength(0, GridUnitType.Star);
            viewList.Height = new GridLength(543, GridUnitType.Star);
            editSupplier.Visibility = Visibility.Hidden;
        }
    }
}
