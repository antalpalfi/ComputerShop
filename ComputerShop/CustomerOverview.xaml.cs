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
    /// Interaction logic for CustomerOverview.xaml
    /// </summary>
    public partial class CustomerOverview : UserControl
    {
        public CustomerOverview()
        {
            InitializeComponent();
            List<string> sortCustomer = new List<string>()
            {
                "A->Z",
                "Z->A",
                "Bought ↑",
                "Bought ↓",

            };
            cmbFilter.ItemsSource = sortCustomer;
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                var supl = ctx.Klants.Select(l => l);
                listViewCustomer.ItemsSource = supl.ToList();
            }
        }
        private void updateFilter()
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                switch (cmbFilter.SelectedItem)
                {
                    case "A->Z":
                        var leveranciersQuery = ctx.Klants.Select(x => x).OrderBy(x => x.Voornaam);
                        listViewCustomer.ItemsSource = leveranciersQuery.ToList();
                        break;
                    case "Z->A":
                        var leverancierZA = ctx.Klants.Select(x => x).OrderByDescending(x => x.Voornaam);
                        listViewCustomer.ItemsSource = leverancierZA.ToList();
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
