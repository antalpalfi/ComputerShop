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
        public OrderProductUserControl1(Personeelslid loggedInPerson)
        {
            InitializeComponent();
            fillTheList();
            if (loggedInPerson.Usertype == "Storekeeper")
            {
                btnRemove.Visibility = Visibility.Hidden;
            }
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

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (listViewOrderProduct.SelectedValue != null)
            {
                using  (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
                {
                    System.Windows.Forms.DialogResult result = MyMessageBox.Show("Are you sure want to delet the product?", MyMessageBox.CMessageBoxButton.Yes, MyMessageBox.CMessageBoxButton.No);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        var bestPro = ctx.BestellingProducts.RemoveRange(ctx.BestellingProducts.Where(x => x.BestellingProductID == (int)listViewOrderProduct.SelectedValue)).FirstOrDefault();
                        ctx.SaveChanges();
                        MessagBoxInfo.Show("Data successfully deleted", MessagBoxInfo.CmessageBoxTitle.Info);
                        fillTheList();
                    }
                }
            }
        }
    }
}
