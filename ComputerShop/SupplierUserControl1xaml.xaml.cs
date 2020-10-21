using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
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
    /// Interaction logic for SupplierUserControl1xaml.xaml
    /// </summary>
    public partial class SupplierUserControl1xaml : UserControl
    {
       
        public SupplierUserControl1xaml()
        {
            InitializeComponent();
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
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                string company = "";
                var name = ctx.Leveranciers.Where(x => x.ID == (int)supportList.SelectedValue);
                foreach (var item in name)
                {
                    company+= item.Company.ToString();
                }
                var selec = ctx.Leveranciers.RemoveRange(ctx.Leveranciers.Where(x => x.ID == (int)supportList.SelectedValue)).FirstOrDefault();
                System.Windows.Forms.DialogResult result = MyMessageBox.Show("Are you sure you want to delete" + " " + company + "\n" + "and all data with it?", MyMessageBox.CMessageBoxButton.Yes, MyMessageBox.CMessageBoxButton.No);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    ctx.SaveChanges();
                    updateTheList();
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
                if (supportList.SelectedValue!=null)
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
        private void updateSupplierEditform()
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
                ctx.SaveChanges();
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
            updateSupplierEditform();
            updateTheList();
            editSup.Height = new GridLength(0, GridUnitType.Star);
            viewList.Height = new GridLength(543, GridUnitType.Star);
            editSupplier.Visibility = Visibility.Hidden;
            supportList.IsEnabled = true;
        }

        private void btnAddSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtCompany.Text != "" && txtContactPerson.Text != "" && txtEmail.Text != "" && txtHouseNumber.Text != "" && txtMailbox.Text != "" && txtPhone.Text != "" && txtStreet.Text != "" && txtTown.Text != "" && txtZipcode.Text != "")
            {
                using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
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
                        }
                    }
                    else
                    {
                        MessagBoxInfo.Show("Company is already exist", MessagBoxInfo.CmessageBoxTitle.Error);
                    }
                }
            }
            else MessagBoxInfo.Show("Lookup again", MessagBoxInfo.CmessageBoxTitle.Warning);

            supportList.IsEnabled = true;
            editSup.Height = new GridLength(0, GridUnitType.Star);
            viewList.Height = new GridLength(543, GridUnitType.Star);
            editSupplier.Visibility = Visibility.Hidden;
            updateTheList();
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
    }
}
