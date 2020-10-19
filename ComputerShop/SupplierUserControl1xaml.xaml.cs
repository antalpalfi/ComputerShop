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
    /// Interaction logic for SupplierUserControl1xaml.xaml
    /// </summary>
    public partial class SupplierUserControl1xaml : UserControl
    {
       
        public SupplierUserControl1xaml()
        {
            InitializeComponent();
            FillTheList();
        }
        private void FillTheList()
        {
            using (ComputerWareHousProject ctx = new ComputerWareHousProject())
            {
                var supl = ctx.Leveranciers.Select(l => l);
                supportList.SelectedValuePath = "LeverancierID";
                supportList.ItemsSource = supl.ToList();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NewSuplier suplier = new NewSuplier();
            suplier.ShowDialog();
            FillTheList();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            using (ComputerWareHousProject ctx = new ComputerWareHousProject())
            {
                string company = "";
                var name = ctx.Leveranciers.Where(x => x.LeverancierID == (int)supportList.SelectedValue);
                foreach (var item in name)
                {
                    company+= item.Company.ToString();
                }
                var selec = ctx.Leveranciers.RemoveRange(ctx.Leveranciers.Where(x => x.LeverancierID == (int)supportList.SelectedValue)).FirstOrDefault();
                System.Windows.Forms.DialogResult result = MyMessageBox.Show("Are you sure you want to delete" + " " + company + "\n" + "and all data with it?", MyMessageBox.CMessageBoxButton.Yes, MyMessageBox.CMessageBoxButton.No);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    ctx.SaveChanges();
                    FillTheList();
                }
            }
        }
        public  int getSelectedValue()
        {
            int selectedValue = Convert.ToInt32(supportList.SelectedValue);
            return selectedValue;
        }

        private void btnEdit_Click_1(object sender, RoutedEventArgs e)
        {
            EditSupplier supl = new EditSupplier();
            supl.ShowDialog();
        }

        private void supportList_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show(supportList.SelectedValue.ToString());
        }
    }
}
