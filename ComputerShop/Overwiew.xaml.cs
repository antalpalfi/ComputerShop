using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ComputerShop
{
    /// <summary>
    /// Interaction logic for Overwiew.xaml
    /// </summary>
    public partial class Overwiew : Window
    {
        public Personeelslid loggedInUser { get; set; }
        public Overwiew(Personeelslid loggedInPerson)
        {
            InitializeComponent();
            loggedInUser = loggedInPerson;
            DispatcherTimer timeNow = new DispatcherTimer();
            timeNow.Tick += new EventHandler(UpdateTimer_Tick);
            timeNow.Interval = new TimeSpan(0, 0, 1);
            timeNow.Start();
        }
        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            txtDateTime.Text = DateTime.Now.ToString("g");
        }
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListView.SelectedIndex;
            MoveCursorMenu(index);

            switch (index)
            {
                case 0:
                    gridPages.Children.Clear();
                    gridPages.Children.Add(new SupplierOverview());
                    break;
                case 1:
                    gridPages.Children.Clear();
                    gridPages.Children.Add(new CustomerOverview());
                    break;
                case 2:
                    //gridPages.Children.Clear();
                    //gridPages.Children.Add(new ProductPage());
                    break;
                case 3:
                    gridPages.Children.Clear();
                    gridPages.Children.Add(new CategoryUserControl1());
                    break;
                case 4:
                    gridPages.Children.Clear();
                    //gridPages.Children.Add(new PersonMembersUserControl1());
                    break;
                case 5:
                    gridPages.Children.Clear();
                    //gridPages.Children.Add(new OrderUserControl1());
                    break;
                case 6:
                    MainWindow mainWindow = new MainWindow();
                    this.Close();
                    mainWindow.ShowDialog();
                    break;
                case 7:
                    MainMenu mainmenu = new MainMenu(loggedInUser);
                    this.Close();
                    mainmenu.ShowDialog();
                    break;
               
                default:
                    break;
            }
        }

        private void MoveCursorMenu(int index)
        {
            transationSlide.OnApplyTemplate();
            sideGridCursor.Margin = new Thickness(0, (50 + (60 * index)), 0, 0);
        }

        private void btnEcs_Click(object sender, RoutedEventArgs e)
        {
            {
                System.Windows.Application.Current.Shutdown();
            }
        }

        private void btnJasonCreat_Click(object sender, RoutedEventArgs e)
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                var products = ctx.Products.Select(x => x);
                List<object> listProducts = new List<object>();
                foreach (var item in products)
                {
                    Product productList = new Product()
                    {
                        ID = item.ID,
                        Naam = item.Naam,
                        Marge = item.Marge,
                        Eenheid = item.Eenheid,
                        BTW = item.BTW,
                        LeverancierID = item.LeverancierID,
                        CategorieID = item.CategorieID,
                        Inkoopprijs = item.Inkoopprijs,
                        InStock  = item.InStock
                    };
                    listProducts.Add(productList);
                }
                JsonCreat(listProducts);
                MessagBoxInfo.Show("Template created", MessagBoxInfo.CmessageBoxTitle.Info);
            }
        }

        private void JsonCreat(List<object> listProducts)
        {
            if (!File.Exists("json_supplier.Json"))
            {
                using (FileStream mystream = File.Create("json_supplier.Json"))
                {

                }
            }
            var jsonSup = JsonConvert.SerializeObject(listProducts, Formatting.Indented);
            File.WriteAllText("json_supplier.Json", jsonSup);
            //MessagBoxInfo.Show(jsonSup.ToString(), MessagBoxInfo.CmessageBoxTitle.Info);
        }

        private void btneditJson_Click(object sender, RoutedEventArgs e)
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                var jsonPro = File.ReadAllText("json_supplier.Json");
                List<Product> listProducts = JsonConvert.DeserializeObject<List<Product>>(jsonPro);
                foreach (var item in listProducts)
                {
                    var selectProduct = ctx.Products.Single(x => x.ID == item.ID);
                    selectProduct.Naam = item.Naam;
                    selectProduct.Marge = item.Marge;
                    selectProduct.Eenheid = item.Eenheid;
                    selectProduct.BTW = item.BTW;
                    selectProduct.LeverancierID = item.LeverancierID;
                    selectProduct.CategorieID = item.CategorieID;
                    selectProduct.Inkoopprijs = item.Inkoopprijs;
                    selectProduct.InStock = item.InStock;
                    ctx.SaveChanges();
                }
            }
            MessagBoxInfo.Show("Successfully edited", MessagBoxInfo.CmessageBoxTitle.Info);
        }
    }
}
