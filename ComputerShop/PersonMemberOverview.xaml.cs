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
    /// Interaction logic for PersonMemberOverview.xaml
    /// </summary>
    public partial class PersonMemberOverview : UserControl
    {
        public PersonMemberOverview()
        {
            InitializeComponent();
            updateTheList();
            List<string> sortProduct = new List<string>()
            {
                "A->Z",
                "Z->A",
                "UserType ↑",
                "UserType ↓",
            };
            cmbPerson.ItemsSource = sortProduct;
        }
        private void updateTheList()
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                var personLid = ctx.Personeelslids.Select(x => x);
                listViewPersonLid.SelectedValuePath = "ID";
                listViewPersonLid.ItemsSource = personLid.ToList();
            }
        }
        private void updateFilter()
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                switch (cmbPerson.SelectedItem)
                {
                    case "A->Z":
                        var person = ctx.Personeelslids.Select(x => x).OrderBy(x => x.Voornaam);
                        listViewPersonLid.ItemsSource = person.ToList();
                        break;
                    case "Z->A":
                        var person1 = ctx.Personeelslids.Select(x => x).OrderByDescending(x => x.Voornaam);
                        listViewPersonLid.ItemsSource = person1.ToList();
                        break;
                    case "UserType ↑":
                        var userType = ctx.Personeelslids.Select(x => x).OrderBy(x => x.Usertype);
                        listViewPersonLid.ItemsSource = userType.ToList();
                        break;

                    case "UserType ↓":
                        var userType1 = ctx.Personeelslids.Select(x => x).OrderByDescending(x => x.Usertype);
                        listViewPersonLid.ItemsSource = userType1.ToList();
                        break;
                    default:
                        break;
                }
            }
        }

        private void cmbPerson_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateFilter();
        }
    }
}
