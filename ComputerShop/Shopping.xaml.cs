using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ComputerShop
{
    /// <summary>
    /// Interaction logic for Shopping.xaml
    /// </summary>
    public partial class Shopping : Window
    {
        //public Dictionary<double, double> shoppingKart = new Dictionary<double, double>();
        public List<ProductinKar> myProductinKars = new List<ProductinKar>();
        public Shopping()
        {
            InitializeComponent();

            lbShoppingCart.ItemsSource = null;
            lbShoppingCart.SelectedValuePath = "ProductinKarID";
            lbShoppingCart.ItemsSource = myProductinKars;
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                if (lbProduct.SelectedValue != null)
                {
                    var product = ctx.Products.Select(x => x).Where(x => x.ID == (int)lbProduct.SelectedValue).FirstOrDefault();
                    if (product.InStock >= Convert.ToInt32(txtQuantity.Text))
                    {
                        myProductinKars.Add(new ProductinKar(Convert.ToDouble(txtQuantity.Text), (product as Product).sellPrice(), product.ID, product.Naam));
                    }
                }
               
            }
            lbShoppingCart.ItemsSource = null;
            lbShoppingCart.SelectedValuePath = "ProductinKarID";
            lbShoppingCart.ItemsSource = myProductinKars;
            updateTotalPrice();
        }

        private void lbProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                if (lbProduct.SelectedValue!=null)
                {
                    var product = ctx.Products.Select(x => x).Where(x => x.ID == (int)lbProduct.SelectedValue).FirstOrDefault();
                    txtPrice.Text = (product as Product).nettoPrice().ToString();
                    txtTaxPrice.Text = (product as Product).sellPrice().ToString();
                }
            };
        }
        public class ProductinKar
        {
            public double Quantity { get; set; }
            public double Price { get; set; }
            public int ProductinKarID { get; set; }
            public string ProductInKarNaam { get; set; }
                
            public ProductinKar(double quantity, double price, int productinKarID, string productInKarNaam)
            {
                Quantity = quantity;
                Price = price;
                ProductinKarID = productinKarID;
                ProductInKarNaam = productInKarNaam;
            }
            public ProductinKar()
            {

            }
        }

        private void btnPlus_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in myProductinKars)
            {
                if (lbShoppingCart.SelectedValue != null && (int)lbShoppingCart.SelectedValue == item.ProductinKarID)
                {
                    item.Quantity ++;
                }
            }
            lbShoppingCart.ItemsSource = null;
            lbShoppingCart.ItemsSource = myProductinKars;
            updateTotalPrice();
        }

        private void btnMinus_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in myProductinKars)
            {
                if (lbShoppingCart.SelectedValue != null && (int)lbShoppingCart.SelectedValue == item.ProductinKarID)
                {
                    item.Quantity--;
                }
            }
            lbShoppingCart.ItemsSource = null;
            lbShoppingCart.ItemsSource = myProductinKars;
            updateTotalPrice();
        }
        private void updateTotalPrice()
        {
            double totalprice = 0;
            foreach (var item in myProductinKars)
            {
                totalprice += item.Price * item.Quantity;

            }
            txtTotalPrice.Text = totalprice.ToString();

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainMenu mymenu = new MainMenu();
            this.Close();
            mymenu.ShowDialog();
        }

        private void btnMotherboard_Click(object sender, RoutedEventArgs e)
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                var product = ctx.Products.Select(x => x).Where(x=>x.CategorieID == 1);
                lbProduct.DisplayMemberPath = "Naam";
                lbProduct.SelectedValuePath = "ID";
                lbProduct.ItemsSource = product.ToList();
            }
        }

        private void btnProcessor_Click(object sender, RoutedEventArgs e)
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                var product = ctx.Products.Select(x => x).Where(x => x.CategorieID == 2);
                lbProduct.DisplayMemberPath = "Naam";
                lbProduct.SelectedValuePath = "ID";
                lbProduct.ItemsSource = product.ToList();
            }
        }

        private void btnMemory_Click(object sender, RoutedEventArgs e)
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                var product = ctx.Products.Select(x => x).Where(x => x.CategorieID == 3);
                lbProduct.DisplayMemberPath = "Naam";
                lbProduct.SelectedValuePath = "ID";
                lbProduct.ItemsSource = product.ToList();
            }
        }

        private void btnVGA_Click(object sender, RoutedEventArgs e)
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                var product = ctx.Products.Select(x => x).Where(x => x.CategorieID == 4);
                lbProduct.DisplayMemberPath = "Naam";
                lbProduct.SelectedValuePath = "ID";
                lbProduct.ItemsSource = product.ToList();
            }
        }

        private void btnPowerSupply_Click(object sender, RoutedEventArgs e)
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                var product = ctx.Products.Select(x => x).Where(x => x.CategorieID == 5);
                lbProduct.DisplayMemberPath = "Naam";
                lbProduct.SelectedValuePath = "ID";
                lbProduct.ItemsSource = product.ToList();
            }
        }

        private void btnSSDs_Click(object sender, RoutedEventArgs e)
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                var product = ctx.Products.Select(x => x).Where(x => x.CategorieID == 6);
                lbProduct.DisplayMemberPath = "Naam";
                lbProduct.SelectedValuePath = "ID";
                lbProduct.ItemsSource = product.ToList();
            }
        }

        private void btnCooling_Click(object sender, RoutedEventArgs e)
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                var product = ctx.Products.Select(x => x).Where(x => x.CategorieID == 7);
                lbProduct.DisplayMemberPath = "Naam";
                lbProduct.SelectedValuePath = "ID";
                lbProduct.ItemsSource = product.ToList();
            }
        }

        private void btnComputerCase_Click(object sender, RoutedEventArgs e)
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                var product = ctx.Products.Select(x => x).Where(x => x.CategorieID == 8);
                lbProduct.DisplayMemberPath = "Naam";
                lbProduct.SelectedValuePath = "ID";
                lbProduct.ItemsSource = product.ToList();
            }
        }

        private void btnPc_Click(object sender, RoutedEventArgs e)
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                var product = ctx.Products.Select(x => x).Where(x => x.CategorieID == 9);
                lbProduct.DisplayMemberPath = "Naam";
                lbProduct.SelectedValuePath = "ID";
                lbProduct.ItemsSource = product.ToList();
            }
        }

        private void btnLaptop_Click(object sender, RoutedEventArgs e)
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                var product = ctx.Products.Select(x => x).Where(x => x.CategorieID == 10);
                lbProduct.DisplayMemberPath = "Naam";
                lbProduct.SelectedValuePath = "ID";
                lbProduct.ItemsSource = product.ToList();
            }
        }

        private void btnPlusQuantity_Click(object sender, RoutedEventArgs e)
        {
            int number = Convert.ToInt32(txtQuantity.Text);
            number++;
            txtQuantity.Text = number.ToString();
        }

        private void btnMinuszQuantity_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(txtQuantity.Text) > 0)
            {
                int number = Convert.ToInt32(txtQuantity.Text);
                number--;
                txtQuantity.Text = number.ToString();
            }
        }
    }
}
