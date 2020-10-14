using MaterialDesignThemes.Wpf;
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
using System.Windows.Threading;

namespace ComputerShop
{
    /// <summary>
    /// Interaction logic for Supplier.xaml
    /// </summary>
    public partial class Supplier : UserControl
    {

        public Supplier()
        {
            InitializeComponent();

            using (IndividueelProjectEntities1 ctx = new IndividueelProjectEntities1())
            {
                var supl = ctx.Leveranciers.Select(l => l);
                lbSupplierName.SelectedValuePath = "LeverancierID";
                //lbSupplierName.DisplayMemberPath = "Company";
                lbSupplierName.ItemsSource = supl.ToList();
            }
        }


        private void lbSupplierName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (IndividueelProjectEntities1 ctx = new IndividueelProjectEntities1())
            {
                var supl = ctx.Leveranciers.Select(l => l).Where(b => b.LeverancierID ==(int)lbSupplierName.SelectedValue).FirstOrDefault();
                txtContactPerson.Text =$"Contact person: {supl.Contactpersoon}";
                txtTelefon.Text =$"Telefon: {supl.Telefoonnummer}";
                txtEmail.Text =$"Email: {supl.Emailadres}";
                txtStreet.Text = $"Street: {supl.Straatnaam}";
                txtHousNumber.Text = $"House number: {supl.Huisnummer}";
                txtBox.Text = $"Box: {supl.Bus}";
                txtZipCode.Text = $"Zipcode: {supl.Postcode}";
                txtTown.Text = $"Town: {supl.Gemeente}";
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NewSuplier suplier = new NewSuplier();
            suplier.ShowDialog();
        }
    }
}
