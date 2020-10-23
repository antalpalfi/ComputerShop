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
    /// Interaction logic for OrderProductUserControl1.xaml
    /// </summary>
    public partial class OrderProductUserControl1 : UserControl
    {
        public OrderProductUserControl1()
        {
            InitializeComponent();
            fillTheList();
        }
        private void fillTheList()
        {
             using(IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                var orderopro = ctx.BestellingProducts.Select(x => new {Id = x.BestellingProductID, BestelinId = x.BestellingID,
                ProductName = x.Product.Naam , Quantity = x.Pieces});
                listViewOrderProduct.SelectedValuePath = "Id";
                listViewOrderProduct.ItemsSource = orderopro.ToList();
            }
        }
    }
}
