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
    /// Interaction logic for ShoppingSupplierUSC.xaml
    /// </summary>
    public partial class ShoppingSupplierUSC : UserControl
    {
        public Personeelslid loggedInUser { get; set; }
        public WindowState WindowState { get; private set; }

        public List<ProductinKar> myProductinKars = new List<ProductinKar>();
        public ProductinKar productinKar = new ProductinKar();
        public ShoppingSupplierUSC(Personeelslid loggedInPerson)
        {
            InitializeComponent();
            shoppingPanel.Visibility = Visibility.Hidden;
            loggedInUser = loggedInPerson;
            updateCmbSupplier();
        }
        public class ProductinKar
        {
            public int Quantity { get; set; }
            public double Price { get; set; }
            public int ProductinKarID { get; set; }
            public string ProductInKarNaam { get; set; }
            public double Margin1 { get; set; }
            public double Btw { get; set; }

            public int SupplierId { get; set; }

            public ProductinKar(int quantity, double price, int productinKarID, string productInKarNaam, int supplierId, double margin, double btw)
            {
                Quantity = quantity;
                Price = price;
                ProductinKarID = productinKarID;
                ProductInKarNaam = productInKarNaam;
                SupplierId = supplierId;
                Margin1 = margin;
                Btw = btw;
            }
            public ProductinKar()
            {

            }
           
        }
        private void updateTotalPrice()
        {
            double totalprice = 0;

            foreach (var item in myProductinKars)
            {
                totalprice += item.Price  * item.Quantity;

            }
            txtTotalPrice.Text = totalprice.ToString() + "€";

        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            int number = 0;
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                if (productListView.SelectedValue != null && Convert.ToInt32(txtQuantity.Text) != 0)
                {
                    var product = ctx.Products.Select(x => x).Where(x => x.ID == (int)productListView.SelectedValue).FirstOrDefault();
                    foreach (var item in myProductinKars)
                    {
                        if (item.ProductinKarID == product.ID)
                        {
                            number++;
                        }
                    }
                    if (number == 1)
                    {
                        MessagBoxInfo.Show("The product is already in the shopping car", MessagBoxInfo.CmessageBoxTitle.Info);
                    }
                    else
                        myProductinKars.Add(new ProductinKar(Convert.ToInt32(txtQuantity.Text), (double)product.Inkoopprijs, product.ID, product.Naam, (int)product.LeverancierID, (double)product.Marge, (double)product.BTW));
                    
                }
                ctx.SaveChanges();
            }
            lbShoppingCart.ItemsSource = null;
            lbShoppingCart.SelectedValuePath = "ProductinKarID";
            lbShoppingCart.ItemsSource = myProductinKars;
            updateTotalPrice();
        }
        private void btnMotherboard_Click(object sender, RoutedEventArgs e)
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                var product = ctx.Products.Select(x => x).Where(x => x.CategorieID == 1);
                productListView.SelectedValuePath = "ID";
                productListView.ItemsSource = product.ToList();
            }
            txtQuantity.Text = 0.ToString();
            txtPrice.Text = string.Empty;
        }

        private void btnProcessor_Click(object sender, RoutedEventArgs e)
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                var product = ctx.Products.Select(x => x).Where(x => x.CategorieID == 2);
                productListView.SelectedValuePath = "ID";
                productListView.ItemsSource = product.ToList();
            }
            txtQuantity.Text = 0.ToString();
            txtPrice.Text = string.Empty;
        }

        private void btnMemory_Click(object sender, RoutedEventArgs e)
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                var product = ctx.Products.Select(x => x).Where(x => x.CategorieID == 3);
                productListView.SelectedValuePath = "ID";
                productListView.ItemsSource = product.ToList();
            }
            txtQuantity.Text = 0.ToString();
            txtPrice.Text = string.Empty;
        }

        private void btnVGA_Click(object sender, RoutedEventArgs e)
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                var product = ctx.Products.Select(x => x).Where(x => x.CategorieID == 4);
                productListView.SelectedValuePath = "ID";
                productListView.ItemsSource = product.ToList();
            }
            txtQuantity.Text = 0.ToString();
            txtPrice.Text = string.Empty;
        }

        private void btnPowerSupply_Click(object sender, RoutedEventArgs e)
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                var product = ctx.Products.Select(x => x).Where(x => x.CategorieID == 5);
                productListView.SelectedValuePath = "ID";
                productListView.ItemsSource = product.ToList();
            }
            txtQuantity.Text = 0.ToString();
            txtPrice.Text = string.Empty;
        }

        private void btnSSDs_Click(object sender, RoutedEventArgs e)
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                var product = ctx.Products.Select(x => x).Where(x => x.CategorieID == 6);
                productListView.SelectedValuePath = "ID";
                productListView.ItemsSource = product.ToList();
            }
            txtQuantity.Text = 0.ToString();
            txtPrice.Text = string.Empty;
        }

        private void btnCooling_Click(object sender, RoutedEventArgs e)
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                var product = ctx.Products.Select(x => x).Where(x => x.CategorieID == 7);
                productListView.SelectedValuePath = "ID";
                productListView.ItemsSource = product.ToList();
            }
            txtQuantity.Text = 0.ToString();
            txtPrice.Text = string.Empty;
        }

        private void btnComputerCase_Click(object sender, RoutedEventArgs e)
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                var product = ctx.Products.Select(x => x).Where(x => x.CategorieID == 8);
                productListView.SelectedValuePath = "ID";
                productListView.ItemsSource = product.ToList();
            }
            txtQuantity.Text = 0.ToString();
            txtPrice.Text = string.Empty;
        }

        private void btnPc_Click(object sender, RoutedEventArgs e)
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                var product = ctx.Products.Select(x => x).Where(x => x.CategorieID == 9);
                productListView.SelectedValuePath = "ID";
                productListView.ItemsSource = product.ToList();
            }
            txtQuantity.Text = 0.ToString();
            txtPrice.Text = string.Empty;
        }

        private void btnLaptop_Click(object sender, RoutedEventArgs e)
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                var product = ctx.Products.Select(x => x).Where(x => x.CategorieID == 10);
                productListView.SelectedValuePath = "ID";
                productListView.ItemsSource = product.ToList();
            }
            txtQuantity.Text = 0.ToString();
            txtPrice.Text = string.Empty;

        }

        private void btnAll_Click(object sender, RoutedEventArgs e)
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                var product = ctx.Products.Select(x => x);
                productListView.SelectedValuePath = "ID";
                productListView.ItemsSource = product.ToList();
            }
            txtQuantity.Text = 0.ToString();
            txtPrice.Text = string.Empty;
        }
        
        private void btnPlus_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in myProductinKars)
            {
                if (lbShoppingCart.SelectedValue != null && (int)lbShoppingCart.SelectedValue == item.ProductinKarID)
                {
                    item.Quantity++;
                }
            }
            lbShoppingCart.ItemsSource = null;
            lbShoppingCart.ItemsSource = myProductinKars;
            updateTotalPrice();

        }
        // ha 0 akkor kitorolje meg kell csinalni
        private void btnMinus_Click(object sender, RoutedEventArgs e)
        {

            foreach (var item in myProductinKars)
            {
                if (lbShoppingCart.SelectedValue != null && (int)lbShoppingCart.SelectedValue == item.ProductinKarID && item.Quantity > 0)
                {
                    item.Quantity--;
                }
                //if (item.Quantity == 0)
                //{
                //    var productRemove = myProductinKars.Single(x => x.ProductinKarID == item.ProductinKarID);
                //    myProductinKars.Remove(productRemove);
                //}
            }

            updateShoppingList();
            updateTotalPrice();
        }
        private void updateShoppingList()
        {
            lbShoppingCart.ItemsSource = null;
            lbShoppingCart.SelectedValuePath = "ProductinKarID";
            lbShoppingCart.ItemsSource = myProductinKars;
        }
        private void btnPlusQuantity_Click(object sender, RoutedEventArgs e)
        {
            if (productListView.SelectedValue != null && txtQuantity.Text != string.Empty)
            {
                int number = Convert.ToInt32(txtQuantity.Text);
                number++;
                txtQuantity.Text = number.ToString();
            }
        }

        private void btnMinuszQuantity_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(txtQuantity.Text) > 0 && productListView.SelectedValue != null)
            {
                int number = Convert.ToInt32(txtQuantity.Text);
                number--;
                txtQuantity.Text = number.ToString();
            }
        }
        private void updateCmbSupplier()
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                var supl = ctx.Leveranciers.Select(x => x);
                cmbSupplier.SelectedValuePath = "ID";
                cmbSupplier.DisplayMemberPath = "Company";
                cmbSupplier.ItemsSource = supl.ToList();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                if (lbShoppingCart.SelectedIndex != -1)
                {
                    var product = ctx.Products.Select(x => x).Where(x => x.ID == (int)lbShoppingCart.SelectedValue).FirstOrDefault();
                    System.Windows.Forms.DialogResult result = MyMessageBox.Show("are you sure you want delete from shopping cart", MyMessageBox.CMessageBoxButton.Yes, MyMessageBox.CMessageBoxButton.No);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        myProductinKars.RemoveAt(lbShoppingCart.SelectedIndex);
                    }
                }
            }
            updateShoppingList();
            updateTotalPrice();
        }
        // le kell frisiteni az osszes listat
        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.DialogResult result = MyMessageBox.Show("Do you confirm the order?", MyMessageBox.CMessageBoxButton.Yes, MyMessageBox.CMessageBoxButton.No);
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                if (cmbSupplier.SelectedValue != null && lbShoppingCart.Items != null)
                {

                    ctx.Bestellings.Add(new Bestelling()
                    {
                        PersoneelslidID = loggedInUser.ID,
                        KlantID = null,
                        LeverancierID = (int)cmbSupplier.SelectedValue,
                        DatumOpgemaakt = DateTime.Now
                    });
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {


                        ctx.SaveChanges();
                        var lastBesttelingId = ctx.Bestellings.Select(x => x.ID).Max();
                        foreach (var item in myProductinKars)
                        {
                            ctx.BestellingProducts.Add(new BestellingProduct()
                            {
                                BestellingID = lastBesttelingId,
                                ProductID = item.ProductinKarID,
                                Pieces = item.Quantity
                            });
                            var product = ctx.Products.Select(x => x).Where(x => x.ID == item.ProductinKarID).FirstOrDefault();
                            product.InStock = product.InStock + item.Quantity;
                            MessageBox.Show(product.InStock.ToString());
                            ctx.SaveChanges();
                        }
                        MessagBoxInfo.Show("Order successful", MessagBoxInfo.CmessageBoxTitle.Info);
                    }
                }
                else
                {
                    MessagBoxInfo.Show("Something missing", MessagBoxInfo.CmessageBoxTitle.Error);
                }
                myProductinKars.Clear();
                txtQuantity.Text = string.Empty;
                txtPrice.Text = string.Empty;
                txtTotalPrice.Text = string.Empty;
                updateShoppingList();
            }
        }

        private void productListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                if (productListView.SelectedValue != null)
                {
                    var product = ctx.Products.Select(x => x).Where(x => x.ID == (int)productListView.SelectedValue).FirstOrDefault();
                    txtPrice.Text = product.Inkoopprijs.ToString() + "€";
                }
            };
        }

        private void btnShoppingCart_Click(object sender, RoutedEventArgs e)
        {
            shoppingPanel.Visibility = Visibility.Visible;
        }

        private void btnEcs_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void btnMini_Click(object sender, RoutedEventArgs e)
        {
            var myWindow = Window.GetWindow(this);
            myWindow.WindowState = WindowState.Minimized;
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            var myWindow = Window.GetWindow(this);
            myWindow.Close();
            mainWindow.ShowDialog();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainMenu mainmenu = new MainMenu(loggedInUser);
            var myWindow = Window.GetWindow(this);
            myWindow.Close();
            mainmenu.ShowDialog();
        }
    }
}
