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
    /// Interaction logic for ProductPage.xaml
    /// </summary>
    public partial class ProductPage : UserControl
    {

        public ProductPage()
        {
            InitializeComponent();
            updateListView();
            fillComboBox();
            hideGrid();
        }

        private void productListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateEditForm();
        }
        private void updateListView()
        {
            using (ComputerWareHousProject ctx = new ComputerWareHousProject())
            {
                var joinLeverancie = ctx.Products.Join(
                    ctx.Leveranciers,
                    p => p.LeverancierID,
                    l => l.LeverancierID,
                    (p, l) => new { p, l});

                //supplierHeader.DisplayMemberBinding = Supname;

                var allProd = ctx.Products.Select(p => p);
                productListView.SelectedValuePath = "ProductID";
                productListView.ItemsSource = allProd.ToList();
            }
        }
        private void fillComboBox()
        {
            using (ComputerWareHousProject ctx = new ComputerWareHousProject())
            {
                var supList = ctx.Leveranciers.Select(x => x.Company);
                cmbSupplier.SelectedValuePath = "LeverancierID";
                cmbSupplier.ItemsSource = supList.ToList();

                var catList = ctx.Categories.Select(x => x.CategorieNaam);
                cmbCategory.SelectedValuePath = "CategorieID";
                cmbCategory.ItemsSource = catList.ToList();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            clearTextBox();
            productListView.IsEnabled = false;
            editProduct.Visibility = Visibility.Visible;
            editProductRow.Height = new GridLength(380, GridUnitType.Star);
            viewList.Height = new GridLength(169, GridUnitType.Star);
            btnAddSave.Visibility = Visibility.Visible;
            btnSave.Visibility = Visibility.Hidden;
        }

        private void btnAddSave_Click(object sender, RoutedEventArgs e)
        {
            using (ComputerWareHousProject ctx = new ComputerWareHousProject())
            {
                ctx.Products.Add(new Product()
                {
                    Naam = txtName.Text,
                    Inkoopprijs = Convert.ToInt32(txtPurchasePrice.Text),
                    Marge = Convert.ToInt32(txtMargin.Text),
                    Eenheid = txtUnit.Text,
                    BTW = Convert.ToInt32(txtBtw.Text),
                    InStock = Convert.ToInt32(txtInstock.Text),
                    Source = txtSource.Text,
                    Specifications = txtSpecifications.Text,
                    LeverancierID = (int)cmbSupplier.SelectedValue,
                    CategorieID = (int)cmbCategory.SelectedValue

                });
                ctx.SaveChanges();
                updateListView();
                MessagBoxInfo.Show("New product successfully added to the warehouse.", MessagBoxInfo.CmessageBoxTitle.Info);
                hideGrid();

            }
        }
        private void hideGrid()
        {
            productListView.IsEnabled = true;
            editProduct.Visibility = Visibility.Hidden;
            editProductRow.Height = new GridLength(0, GridUnitType.Star);
            viewList.Height = new GridLength(549, GridUnitType.Star);
        }

        private void btnEsc_Click(object sender, RoutedEventArgs e)
        {
            hideGrid();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            productListView.IsEnabled = true;
            editProduct.Visibility = Visibility.Visible;
            editProductRow.Height = new GridLength(380, GridUnitType.Star);
            viewList.Height = new GridLength(169, GridUnitType.Star);
            btnAddSave.Visibility = Visibility.Hidden;
            btnSave.Visibility = Visibility.Visible;
            updateEditForm();


        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            using (ComputerWareHousProject ctx = new ComputerWareHousProject())
            {
                var product = ctx.Products.Select(x => x).Where(x => x.ProductID == (int)productListView.SelectedValue).FirstOrDefault();
                product.Naam = txtName.Text;
                product.Inkoopprijs = Convert.ToInt32(txtPurchasePrice.Text);
                product.Marge = Convert.ToInt32(txtMargin.Text);
                product.Eenheid = txtUnit.Text;
                product.BTW = Convert.ToInt32(txtBtw.Text);
                product.InStock = Convert.ToInt32(txtInstock.Text);
                product.Source = txtSource.Text;
                product.Specifications = txtSpecifications.Text;
                product.LeverancierID = (int)cmbSupplier.SelectedValue;
                product.CategorieID = (int)cmbCategory.SelectedValue;
                System.Windows.Forms.DialogResult result = MyMessageBox.Show("Are you sure you want to change it?", MyMessageBox.CMessageBoxButton.Yes, MyMessageBox.CMessageBoxButton.No);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    ctx.SaveChanges();
                    MessagBoxInfo.Show("Succesful", MessagBoxInfo.CmessageBoxTitle.Info);
                }
                updateListView();
                hideGrid();
            }
        }
        private void updateEditForm()
        {
            using (ComputerWareHousProject ctx = new ComputerWareHousProject())
            {
                if (productListView.SelectedValue!=null)
                {
                    var product = ctx.Products.Select(x => x).Where(x => x.ProductID == (int)productListView.SelectedValue).FirstOrDefault();
                    txtBtw.Text = product.BTW.ToString();
                    txtInstock.Text = product.InStock.ToString();
                    txtMargin.Text = product.Marge.ToString();
                    txtName.Text = product.Naam;
                    txtPurchasePrice.Text = product.Inkoopprijs.ToString();
                    txtSource.Text = product.Source;
                    txtSpecifications.Text = product.Specifications;
                    txtUnit.Text = product.Eenheid;
                    //cmbCategory
                    //cmbSupplier
                }
            }
        }
        private void clearTextBox()
        {
            txtBtw.Text = String.Empty;
            txtInstock.Text = String.Empty;
            txtMargin.Text = String.Empty;
            txtName.Text = String.Empty;
            txtPurchasePrice.Text = String.Empty;
            txtSource.Text = String.Empty;
            txtSpecifications.Text = String.Empty;
            txtUnit.Text = String.Empty;
        }
    }
}
