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
    /// Interaction logic for CustomerUserControl.xaml
    /// </summary>
    public partial class CustomerUserControl : UserControl
    {
        public CustomerUserControl()
        {
            InitializeComponent();
            FilltheList();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            using (ComputerWareHousProject ctx = new ComputerWareHousProject())
            {
                string klantName = "";
                var name = ctx.Klants.Where(x => x.KlantID == (int)listViewCustomer.SelectedValue);
                foreach (var item in name)
                {
                    klantName += item.Achternaam.ToString();
                    klantName += item.Voornaam.ToString();
                }
                var klant = ctx.Klants.RemoveRange(ctx.Klants.Where(x => x.KlantID == (int)listViewCustomer.SelectedValue)).FirstOrDefault();
                System.Windows.Forms.DialogResult result = MyMessageBox.Show("Are you sure you want to delete " + klantName + "\n " + "and all data with it?", MyMessageBox.CMessageBoxButton.Yes, MyMessageBox.CMessageBoxButton.No);
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    ctx.SaveChanges();
                    FilltheList();
                }
            }
        }
        private void FilltheList()
        {
            using (ComputerWareHousProject ctx = new ComputerWareHousProject())
            {
                var cusList = ctx.Klants.Select(x => x);
                listViewCustomer.SelectedValuePath = "KlantID";
                listViewCustomer.ItemsSource = cusList.ToList();

            }
        }
    }
}
