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
        public List<ProductinKar> myProductinKars = new List<ProductinKar>();
        public Personeelslid loggedInUser { get; set; }
        public Shopping(Personeelslid loggedInPerson)
        {
            InitializeComponent();
            loggedInUser = loggedInPerson;
            lbShoppingCart.ItemsSource = null;
            lbShoppingCart.SelectedValuePath = "ProductinKarID";
            lbShoppingCart.ItemsSource = myProductinKars;
            updateCmbKlant();
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
                        myProductinKars.Add(new ProductinKar(Convert.ToDouble(txtQuantity.Text), (product as Product).sellPrice(), product.ID, product.Naam, (int)product.LeverancierID));
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
                    //txtPrice.Text = (product as Product).nettoPrice().ToString();
                    txtTaxPrice.Text = (product as Product).sellPrice().ToString() + "€";
                    txtInfo.Text = product.Specifications;
                }
            };
            
        }
        public class ProductinKar
        {
            public double Quantity { get; set; }
            public double Price { get; set; }
            public int ProductinKarID { get; set; }
            public string ProductInKarNaam { get; set; }

            public int SupplierId { get; set; }

            public ProductinKar(double quantity, double price, int productinKarID, string productInKarNaam, int supplierId)
            {
                Quantity = quantity;
                Price = price;
                ProductinKarID = productinKarID;
                ProductInKarNaam = productInKarNaam;
                SupplierId = supplierId;
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

            updateShoppingList();
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
            if (lbProduct.SelectedValue != null)
            {
                int number = Convert.ToInt32(txtQuantity.Text);
                number++;
                txtQuantity.Text = number.ToString();
            }
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
        private void updateCmbKlant()
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                var klant = ctx.Klants.Select(x => new { Id = x.ID, Name = x.Voornaam + " " + x.Achternaam });
                cmbKlant.SelectedValuePath = "Id";
                cmbKlant.DisplayMemberPath = "Name";
                cmbKlant.ItemsSource = klant.ToList();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lbShoppingCart.SelectedIndex != -1)
            {
                myProductinKars.RemoveAt(lbShoppingCart.SelectedIndex);
                updateShoppingList();
            }
            updateShoppingList();
        }
        private void updateShoppingList()
        {
            lbShoppingCart.ItemsSource = null;
            lbShoppingCart.SelectedValuePath = "ProductinKarID";
            lbShoppingCart.ItemsSource = myProductinKars;
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
               
                    ctx.Bestellings.Add(new Bestelling() { PersoneelslidID = loggedInUser.ID, 
                        KlantID = (int)cmbKlant.SelectedValue,
                        LeverancierID = null,
                        DatumOpgemaakt = DateTime.Now});

                ctx.SaveChanges();
                var lastBesttelingId = ctx.Bestellings.Select(x => x.ID).Max();
                foreach (var item in myProductinKars)
                {
                    ctx.BestellingProducts.Add(new BestellingProduct()
                    {
                        BestellingID = lastBesttelingId,
                        ProductID = item.ProductinKarID,
                        Pieces = (int)item.Quantity
                    });

                    var product = ctx.Products.Select(x => x).Where(x => x.ID == item.ProductinKarID).FirstOrDefault();
                    product.InStock = Convert.ToInt32(product.InStock - item.Quantity);
                }
                ctx.SaveChanges();
                myProductinKars.Clear();
                txtInfo.Text = string.Empty;
                txtQuantity.Text = string.Empty;
                txtTaxPrice.Text = string.Empty;
                txtTotalPrice.Text = string.Empty;
                txtTotalPriceWithTax.Text = string.Empty;
                updateShoppingList();
            }
        }
    }
}
