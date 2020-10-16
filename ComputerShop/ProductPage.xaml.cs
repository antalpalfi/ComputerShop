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
        public List<Product> myProductList = new List<Product>();
        public ProductPage()
        {
            InitializeComponent();
            
            using (ComputerWareHousProject ctx = new ComputerWareHousProject())
            {
                var allProd = ctx.Products.Select(p => p).ToList();
                foreach (var item in allProd)
                {
                    myProductList.Add(item);
                }
                productListView.SelectedValuePath = "ProductID";
                productListView.ItemsSource = myProductList;
            }
        }

        private void productListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show(productListView.SelectedValue.ToString());
        }
    }
}
