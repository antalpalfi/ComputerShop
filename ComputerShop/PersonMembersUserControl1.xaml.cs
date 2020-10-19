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
    /// Interaction logic for PersonMembersUserControl1.xaml
    /// </summary>
    public partial class PersonMembersUserControl1 : UserControl
    {
        public PersonMembersUserControl1()
        {
            InitializeComponent();
            fillTheList();
        }
        private void fillTheList()
        {
            using (ComputerWareHousProject ctx = new ComputerWareHousProject())
            {
                var personLid = ctx.Personeelslids.Select(x => x);
                listViewPersonLid.SelectedValuePath = "PersoneelslidID";
                listViewPersonLid.ItemsSource = personLid.ToList();
            }
        }
    }
}
