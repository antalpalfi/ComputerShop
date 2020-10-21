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
            updateTheList();
            hidePanel();
            updateComboboxs();
        }
        private  void updateTheList()
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                var order = ctx.Bestellings.Select(x => x);
                listViewOrder.SelectedValuePath = "ID";
                listViewOrder.ItemsSource = order.ToList();
            }
        }
        private void updateComboboxs()
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                var klant = ctx.Klants.Select(x => new { Name = x.Voornaam + " " + x.Achternaam, klantID = x.ID });
                cmbKlant.SelectedValuePath = "klantID";
                cmbKlant.DisplayMemberPath = "Name";
                cmbKlant.ItemsSource = klant.ToList();

                var sup = ctx.Leveranciers.Select(x => x);
                cmbSupplier.SelectedValuePath = "ID";
                cmbSupplier.DisplayMemberPath = "Company";
                cmbSupplier.ItemsSource = sup.ToList();

                var person = ctx.Personeelslids.Select(x => new { PersonName = x.Voornaam + " " + x.Achternaam, peID = x.ID });
                cmbPersonmember.SelectedValuePath = "peID";
                cmbPersonmember.DisplayMemberPath = "PersonName";
                cmbPersonmember.ItemsSource = person.ToList();

            }
            cmbKlant.SelectedIndex = 0;
            cmbPersonmember.SelectedIndex = 0;
            cmbSupplier.SelectedIndex = 0;
        }

        private void btnEsc_Click(object sender, RoutedEventArgs e)
        {
            hidePanel();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            updateComboboxs();
            listViewOrder.IsEnabled = false;
            viewListRow.Height = new GridLength(414, GridUnitType.Star);
            EditRow.Height = new GridLength(117, GridUnitType.Star);
            Editpanel.Visibility = Visibility.Visible;
            Editpanel.IsEnabled = true;
            btnSave.Visibility = Visibility.Hidden;
            btnAddNew.Visibility = Visibility.Visible;
        }

        private void btnAddNew_Click(object sender, RoutedEventArgs e)
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                ctx.Bestellings.Add(new Bestelling()
                {
                    DatumOpgemaakt = Convert.ToDateTime(dtDatum.Text),
                    PersoneelslidID = (int)cmbPersonmember.SelectedValue,
                    LeverancierID = (int)cmbSupplier.SelectedValue,
                    KlantID = (int)cmbKlant.SelectedValue
                });
                MessagBoxInfo.Show("New order seccessfully created.", MessagBoxInfo.CmessageBoxTitle.Info);
                ctx.SaveChanges();
                hidePanel();
                updateTheList();
            }
        }
        private void hidePanel()
        {
            viewListRow.Height = new GridLength(531, GridUnitType.Star);
            EditRow.Height = new GridLength(0, GridUnitType.Star);
            Editpanel.Visibility = Visibility.Hidden;
            Editpanel.IsEnabled = false;
            listViewOrder.IsEnabled = true;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            listViewOrder.IsEnabled = true;
            viewListRow.Height = new GridLength(414, GridUnitType.Star);
            EditRow.Height = new GridLength(117, GridUnitType.Star);
            Editpanel.Visibility = Visibility.Visible;
            Editpanel.IsEnabled = true;
            btnSave.Visibility = Visibility.Visible;
            btnAddNew.Visibility = Visibility.Hidden;

            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                if (listViewOrder.SelectedValue != null)
                {
                    var select = ctx.Bestellings.Select(x => x).Where(x => x.ID == (int)listViewOrder.SelectedValue).FirstOrDefault();
                    cmbKlant.SelectedValue = select.KlantID;
                    cmbPersonmember.SelectedValue = select.PersoneelslidID;
                    cmbSupplier.SelectedValue = select.LeverancierID;
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                if (listViewOrder.SelectedValue != null)
                {
                    var select = ctx.Bestellings.Select(x => x).Where(x => x.ID == (int)listViewOrder.SelectedValue).FirstOrDefault();
                    select.KlantID = (int)cmbKlant.SelectedValue;
                    select.PersoneelslidID = (int)cmbPersonmember.SelectedValue;
                    select.LeverancierID = (int)cmbSupplier.SelectedValue;
                    select.DatumOpgemaakt = Convert.ToDateTime(dtDatum.Text);
                }
                System.Windows.Forms.DialogResult result = MyMessageBox.Show("Are you sure you want to change the data?", MyMessageBox.CMessageBoxButton.Yes, MyMessageBox.CMessageBoxButton.No);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    ctx.SaveChanges();
                }
                hidePanel();
            }
        }

        private void listViewOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                if (listViewOrder.SelectedValue != null)
                {
                    var select = ctx.Bestellings.Select(x => x).Where(x => x.ID == (int)listViewOrder.SelectedValue).FirstOrDefault();
                    cmbKlant.SelectedValue = select.KlantID;
                    cmbPersonmember.SelectedValue = select.PersoneelslidID;
                    cmbSupplier.SelectedValue = select.LeverancierID;
                }
            }
        }
    }
}
