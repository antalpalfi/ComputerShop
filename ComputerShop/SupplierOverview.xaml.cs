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
    /// Interaction logic for SupplierOverview.xaml
    /// </summary>
    public partial class SupplierOverview : UserControl
    {
        public SupplierOverview()
        {
            InitializeComponent();
            List<string> sortSupli = new List<string>()
            {
                "A->Z",
                "Z->A",
                "Deliver ↑",
                "Deliver ↓",

            };
            cmbFilter.ItemsSource = sortSupli;
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                var supl = ctx.Leveranciers.Select(l => l);
                supportList.SelectedValuePath = "ID";
                supportList.ItemsSource = supl.ToList();
            }
        }
        private void updateFilter()
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                switch (cmbFilter.SelectedItem)
                {
                    case "A->Z":
                        var leveranciersQuery = ctx.Leveranciers.Select(x => x).OrderBy(x => x.Company);
                        supportList.ItemsSource = leveranciersQuery.ToList();
                        break;
                    case "Z->A":
                        var leverancierZA = ctx.Leveranciers.Select(x => x).OrderByDescending(x => x.Company);
                        supportList.ItemsSource = leverancierZA.ToList();
                        break;
                    default:
                        break;
                }
            }
        }

        private void cmbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateFilter();
        }
    }
}
