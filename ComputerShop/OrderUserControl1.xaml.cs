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
    /// Interaction logic for OrderUserControl1.xaml
    /// </summary>
    public partial class OrderUserControl1 : UserControl
    {
        public OrderUserControl1()
        {
            InitializeComponent();
            fillTheList();


        }
        private  void fillTheList()
        {
            using (ComputerWareHousProject ctx = new ComputerWareHousProject())
            {
                var order = ctx.Bestellings.Select(x => x);
                listViewOrder.SelectedValuePath = "BestellingID";
                listViewOrder.ItemsSource = order.ToList();
            }
        }
    }
}
