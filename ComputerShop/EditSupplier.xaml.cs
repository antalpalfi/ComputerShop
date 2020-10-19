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
using System.Windows.Shapes;

namespace ComputerShop
{
    /// <summary>
    /// Interaction logic for EditSupplier.xaml
    /// </summary>
    public partial class EditSupplier : Window
    {
        public EditSupplier()
        {
            InitializeComponent();
            SupplierUserControl1xaml supl = new SupplierUserControl1xaml();
            if (supl.supportList.SelectedValue != null)
            {
                int selected = supl.getSelectedValue();
                MessageBox.Show(selected.ToString());

                using (ComputerWareHousProject ctx = new ComputerWareHousProject())
                {
                    var select = ctx.Leveranciers.Select(x => x).Where(y => y.LeverancierID == selected).FirstOrDefault();
                    txtCompany.Text = select.Company.ToString();
                }
            }
        }

        private void btnEsc_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
