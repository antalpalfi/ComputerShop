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
            List<Klant> myKlant = new List<Klant>();
            myKlant.Add(new Klant() { Voornaam = "Antal", Achternaam = "Palfi", Straatnaam = "Teststraat", Huisnummer = 11, Bus = 1, Postcode = 8000, Gemeente = "Brugge", Telefoonnummer = "025480", Emailadres = "antalpalfi90@gmail.com", AangemaaktOp = DateTime.Now, Opmerking = "Hallo hallo" });
            
            using(ComputerWareHousProject ctx = new ComputerWareHousProject())
            {
                var cusList = ctx.Klants.Select(x => x).ToList();
                customerList.SelectedValuePath = "KlantID";
                //customerList.ItemsSource = myKlant;
                foreach (var item in cusList)
                {
                    myKlant.Add(item);
                }
                customerList.ItemsSource = myKlant;
            }
        }
    }
}
