using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        public Personeelslid loggedInUser { get; set; }
        public ProductPage(Personeelslid loggedInPerson)
        {
            InitializeComponent();
            loggedInUser = loggedInPerson;
            if (loggedInUser.Usertype == "Storekeeper")
            {
                btnAdd.Visibility = Visibility.Hidden;
                btnRemove.Visibility = Visibility.Hidden;
                btnEdit.Visibility = Visibility.Hidden;
            }
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
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {

                var company = ctx.Products.Select(x => new
                {
                    Id = x.ID,
                    Name = x.Naam,
                    Purchaseprice = x.Inkoopprijs,
                    Margin = x.Marge,
                    Unit = x.Eenheid,
                    Btw = x.BTW,
                    Leverancie = x.Leverancier.Company,
                    Category = x.Categorie.CategorieNaam,
                    Instock = x.InStock,
                    Foto = x.Foto,
                    Specification = x.Specifications
                });

                productListView.SelectedValuePath = "Id";
                productListView.ItemsSource = company.ToList();
            }
        }
        private void fillComboBox()
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                
                var supList = ctx.Leveranciers.Select(x => x);
                cmbSupplier.SelectedValuePath = "ID";
                cmbSupplier.DisplayMemberPath = "Company";
                cmbSupplier.ItemsSource = supList.ToList();
               

                var catList = ctx.Categories.Select(x => x);
                cmbCategory.SelectedValuePath = "ID";
                cmbCategory.DisplayMemberPath = "CategorieNaam";
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
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                if (chekErrors(true))
                {
                    do
                    {
                        try
                        {
                            ctx.Products.Add(new Product()
                            {
                                Naam = txtName.Text,
                                Inkoopprijs = Convert.ToInt32(txtPurchasePrice.Text),
                                Marge = Convert.ToInt32(txtMargin.Text),
                                Eenheid = txtUnit.Text,
                                BTW = Convert.ToInt32(txtBtw.Text),
                                InStock = Convert.ToInt32(txtInstock.Text),
                                Foto = txtSource.Text,
                                Specifications = txtSpecifications.Text,
                                LeverancierID = (int)cmbSupplier.SelectedValue,
                                CategorieID = (int)cmbCategory.SelectedValue
                            });
                            ctx.SaveChanges();
                            updateListView();
                            MessagBoxInfo.Show("New product successfully added to the warehouse.", MessagBoxInfo.CmessageBoxTitle.Info);
                            hideGrid();
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
            if (chekErrors(true))
            {


                using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
                {
                    var product = ctx.Products.Select(x => x).Where(x => x.ID == (int)productListView.SelectedValue).FirstOrDefault();
                    do
                    {
                        try
                        {
                            product.Naam = txtName.Text;
                            product.Inkoopprijs = Convert.ToInt32(txtPurchasePrice.Text);
                            product.Marge = Convert.ToInt32(txtMargin.Text);
                            product.Eenheid = txtUnit.Text;
                            product.BTW = Convert.ToInt32(txtBtw.Text);
                            product.InStock = Convert.ToInt32(txtInstock.Text);
                            product.Foto = txtSource.Text;
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
                        catch
                        {
                            MessagBoxInfo.Show("Something wrong", MessagBoxInfo.CmessageBoxTitle.Error);
                        }
                    } while (false);
                }
            }
        }
        private void updateEditForm()
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                if (productListView.SelectedValue!=null)
                {
                    var product = ctx.Products.Select(x => x).Where(x => x.ID == (int)productListView.SelectedValue).FirstOrDefault();
                    txtBtw.Text = product.BTW.ToString();
                    txtInstock.Text = product.InStock.ToString();
                    txtMargin.Text = product.Marge.ToString();
                    txtName.Text = product.Naam;
                    txtPurchasePrice.Text = product.Inkoopprijs.ToString();
                    txtSource.Text = product.Foto;
                    txtSpecifications.Text = product.Specifications;
                    txtUnit.Text = product.Eenheid;
                    cmbCategory.SelectedValue = (int)product.CategorieID;
                    cmbSupplier.SelectedValue = (int)product.LeverancierID;
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
            cmbCategory.SelectedIndex = -1;
            cmbSupplier.SelectedIndex = -1;
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                var bestel = ctx.BestellingProducts.Select(x => x).ToList();
                if ( bestel.Contains(productListView.SelectedValue))
                {
                    var bestelingId = ctx.BestellingProducts.Select(x => x).Where(x => x.ProductID == (int)productListView.SelectedValue).FirstOrDefault();
                    ctx.BestellingProducts.RemoveRange(ctx.BestellingProducts.Where(x => x.ProductID == (int)productListView.SelectedValue)).FirstOrDefault();
                    ctx.Bestellings.RemoveRange(ctx.Bestellings.Where(x => x.ID == bestelingId.BestellingID));
                    ctx.Products.RemoveRange(ctx.Products.Where(p => p.ID == (int)productListView.SelectedValue)).FirstOrDefault();
                }
                else
                {
                    ctx.Products.RemoveRange(ctx.Products.Where(p => p.ID == (int)productListView.SelectedValue)).FirstOrDefault();
                }

                System.Windows.Forms.DialogResult result = MyMessageBox.Show("Are you sure want to delet the product?", MyMessageBox.CMessageBoxButton.Yes, MyMessageBox.CMessageBoxButton.No);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    MessagBoxInfo.Show("Product successfully deleted", MessagBoxInfo.CmessageBoxTitle.Info);
                   ctx.SaveChanges();
                }
            }
            updateListView();
        }
        private bool chekErrors( bool noerror)
        {
            if (txtName.Text != string.Empty && txtPurchasePrice.Text != string.Empty && txtMargin.Text != string.Empty && txtUnit.Text != string.Empty
                && txtBtw.Text != string.Empty && txtInstock.Text != string.Empty && txtSource.Text != string.Empty && txtSpecifications.Text != string.Empty
                && cmbSupplier.SelectedValue != null && cmbCategory.SelectedValue != null)
            {
                noerror = true;
            }
            else
            {
                noerror = false;
            }
            return noerror;
        }
    }
}
