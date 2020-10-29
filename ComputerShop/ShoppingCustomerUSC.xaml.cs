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
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace ComputerShop
{
    /// <summary>
    /// Interaction logic for ShoppingCustomerUSC.xaml
    /// </summary>
    public partial class ShoppingCustomerUSC : UserControl
    {
        public List<ProductinKar> myProductinKars = new List<ProductinKar>();
        public Personeelslid loggedInUser { get; set; }
        public ProductinKar productinKar = new ProductinKar();
        public ShoppingCustomerUSC(Personeelslid loggedInPerson)
        {
            InitializeComponent();
            shoppingPanel.Visibility = Visibility.Hidden;
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
                    if (product.InStock >= Convert.ToInt32(txtQuantity.Text) && Convert.ToInt32(txtQuantity.Text) != 0)
                    {
                        myProductinKars.Add(new ProductinKar(Convert.ToInt32(txtQuantity.Text), (double)product.Inkoopprijs , product.ID, product.Naam, (int)product.LeverancierID,(double)product.Marge,(double)product.BTW, product.Eenheid));
                        product.InStock = product.InStock - Convert.ToInt32(txtQuantity.Text);
                    }
                    else
                    {
                        MessagBoxInfo.Show("We have " + product.InStock.ToString(), MessagBoxInfo.CmessageBoxTitle.Error);
                    }
                }
                ctx.SaveChanges();
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
                if (lbProduct.SelectedValue != null)
                {
                    var product = ctx.Products.Select(x => x).Where(x => x.ID == (int)lbProduct.SelectedValue).FirstOrDefault();
                    //txtPrice.Text = (product as Product).nettoPrice().ToString();
                    txtTaxPrice.Text = Convert.ToDouble((product.Inkoopprijs + product.Marge) * (1 + Convert.ToDouble(product.BTW) / 100)).ToString() + "€";
                    txtInfo.Text = product.Specifications;
                }
            };

        }
        public class ProductinKar
        {
            public int Quantity { get; set; }
            public double Price { get; set; }
            public int ProductinKarID { get; set; }
            public string ProductInKarNaam { get; set; }

            public int SupplierId { get; set; }
            public double Margin1 { get; set; }
            public double Btw { get; set; }
            public string Unit { get; set; }


            public ProductinKar(int quantity, double price, int productinKarID, string productInKarNaam, int supplierId, double margin, double btw, string unit)
            {
                Quantity = quantity;
                Price = price;
                ProductinKarID = productinKarID;
                ProductInKarNaam = productInKarNaam;
                SupplierId = supplierId;
                Margin1 = margin;
                Btw = btw;
                Unit = unit;
            }
            public ProductinKar()
            {

            }
            public double sellPrice()
            {
                double sellPrice = Convert.ToDouble((Price + Margin1) * (1 + Convert.ToDouble(Btw) / 100));
                return sellPrice;
            }
            public double nettoPrice()
            {
                double netto = Convert.ToDouble(Margin1 + Price);
                return netto;
            }
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
        private void updateTotalPrice()
        {
            double totalprice = 0;

            foreach (var item in myProductinKars)
            {
                totalprice += ((item.Price + item.Margin1)*(1 + item.Btw / 100)) * item.Quantity;

            }
            txtTotalPriceWithTax.Text = totalprice.ToString();

        }
        private void btnMotherboard_Click(object sender, RoutedEventArgs e)
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                var product = ctx.Products.Select(x => x).Where(x => x.CategorieID == 1);
                lbProduct.DisplayMemberPath = "Naam";
                lbProduct.SelectedValuePath = "ID";
                lbProduct.ItemsSource = product.ToList();
            }
            txtQuantity.Text = 0.ToString();
            txtTaxPrice.Text = string.Empty;
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
            txtQuantity.Text = 0.ToString();
            txtTaxPrice.Text = string.Empty;
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
            txtQuantity.Text = 0.ToString();
            txtTaxPrice.Text = string.Empty;
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
            txtQuantity.Text = 0.ToString();
            txtTaxPrice.Text = string.Empty;
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
            txtQuantity.Text = 0.ToString();
            txtTaxPrice.Text = string.Empty;
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
            txtQuantity.Text = 0.ToString();
            txtTaxPrice.Text = string.Empty;
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
            txtQuantity.Text = 0.ToString();
            txtTaxPrice.Text = string.Empty;
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
            txtQuantity.Text = 0.ToString();
            txtTaxPrice.Text = string.Empty;
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
            txtQuantity.Text = 0.ToString();
            txtTaxPrice.Text = string.Empty;
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
            txtQuantity.Text = 0.ToString();
            txtTaxPrice.Text = string.Empty;

        }

        private void btnPlusQuantity_Click(object sender, RoutedEventArgs e)
        {
            if (lbProduct.SelectedValue != null && txtQuantity.Text != string.Empty)
            {
                int number = Convert.ToInt32(txtQuantity.Text);
                number++;
                txtQuantity.Text = number.ToString();
            }
        }

        private void btnMinuszQuantity_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(txtQuantity.Text) > 0 && lbProduct.SelectedValue != null)
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
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                if (lbShoppingCart.SelectedIndex != -1)
                {
                    var product = ctx.Products.Select(x => x).Where(x => x.ID == (int)lbShoppingCart.SelectedValue).FirstOrDefault();
                    System.Windows.Forms.DialogResult result = MyMessageBox.Show("are you sure you want delete from shopping cart", MyMessageBox.CMessageBoxButton.Yes, MyMessageBox.CMessageBoxButton.No);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {

                        product.InStock = Convert.ToInt32((lbShoppingCart.SelectedItem as ProductinKar).Quantity) + product.InStock;
                        myProductinKars.RemoveAt(lbShoppingCart.SelectedIndex);
                        ctx.SaveChanges();
                    }
                }
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

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                if (cmbKlant.SelectedValue != null && lbShoppingCart.Items != null)
                {


                    ctx.Bestellings.Add(new Bestelling()
                    {
                        PersoneelslidID = loggedInUser.ID,
                        KlantID = (int)cmbKlant.SelectedValue,
                        LeverancierID = null,
                        DatumOpgemaakt = DateTime.Now
                    });

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
                        ctx.SaveChanges();
                    }
                }
                myProductinKars.Clear();
                txtInfo.Text = string.Empty;
                txtQuantity.Text = string.Empty;
                txtTaxPrice.Text = string.Empty;
                txtTotalPrice.Text = string.Empty;
                txtTotalPriceWithTax.Text = string.Empty;
                updateShoppingList();
            }
        }

       
        private void btnAll_Click(object sender, RoutedEventArgs e)
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                var product = ctx.Products.Select(x => x);
                lbProduct.DisplayMemberPath = "Naam";
                lbProduct.SelectedValuePath = "ID";
                lbProduct.ItemsSource = product.ToList();
            }
            txtQuantity.Text = 0.ToString();
            txtTaxPrice.Text = string.Empty;
        }

        private void btnShoppingCart_Click(object sender, RoutedEventArgs e)
        {
            shoppingPanel.Visibility = Visibility.Visible;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                foreach (var item in myProductinKars)
                {
                    var product = ctx.Products.Select(x => x).Where(x => x.ID == item.ProductinKarID).FirstOrDefault();
                    product.InStock = product.InStock + item.Quantity;
                    ctx.SaveChanges();
                }
            }
            MainMenu mainMenu = new MainMenu(loggedInUser);
            mainMenu.ShowDialog();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                foreach (var item in myProductinKars)
                {
                    var product = ctx.Products.Select(x => x).Where(x => x.ID == item.ProductinKarID).FirstOrDefault();
                    product.InStock = product.InStock + item.Quantity;
                    ctx.SaveChanges();
                }
            }
            MainWindow mainwindow = new MainWindow();
            var myWindow = Window.GetWindow(this);
            myWindow.Close();
            mainwindow.ShowDialog();
        }

        private void btnEcs_Click(object sender, RoutedEventArgs e)
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                foreach (var item in myProductinKars)
                {
                    var product = ctx.Products.Select(x => x).Where(x => x.ID == item.ProductinKarID).FirstOrDefault();
                    product.InStock = product.InStock + item.Quantity;
                    ctx.SaveChanges();
                }
            }
            System.Windows.Application.Current.Shutdown();
        }

        private void btnMini_Click(object sender, RoutedEventArgs e)
        {
            var myWindow = Window.GetWindow(this);
            myWindow.WindowState = WindowState.Minimized;
        }

        private void btnPrintWord_Click(object sender, RoutedEventArgs e)
        {
            int number = 0;
            string fileName = "Test";
            var doc = DocX.Create(fileName);
            doc.InsertParagraph("Test");
            for (int i = 0; i <= myProductinKars.Count(); i++)
            {
                number++;
            }
            MessageBox.Show(number.ToString());
            Xceed.Document.NET.Table table = doc.AddTable(number, 4);
            table.Alignment = Alignment.center;
            table.Design = TableDesign.ColorfulList;

            table.Rows[0].Cells[0].Paragraphs.First().Append("Product");
            table.Rows[0].Cells[1].Paragraphs.First().Append("Quantity");
            table.Rows[0].Cells[2].Paragraphs.First().Append("BTW");
            table.Rows[0].Cells[3].Paragraphs.First().Append("Price");
            for (int i = 0; i < myProductinKars.Count(); i++)
            {
                table.Rows[i + 1].Cells[0].Paragraphs.First().Append(myProductinKars[i].ProductInKarNaam);
                table.Rows[i + 1].Cells[1].Paragraphs.First().Append(myProductinKars[i].Quantity.ToString() + " " + myProductinKars[i].Unit);
                table.Rows[i + 1].Cells[2].Paragraphs.First().Append(myProductinKars[i].Btw.ToString() + "%");
                table.Rows[i + 1].Cells[3].Paragraphs.First().Append((myProductinKars[i].Price * myProductinKars[i].Quantity).ToString() + "€");
            }
            doc.InsertTable(table);


            doc.Save();
        }
    }
}
