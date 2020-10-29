using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ComputerShop
{
    /// <summary>
    /// Interaction logic for OrderUserControl1.xaml
    /// </summary>
    public partial class OrderUserControl1 : UserControl
    {
        public Personeelslid loggedInUser { get; set; }
        public OrderUserControl1(Personeelslid loggedInPerson)
        {
            InitializeComponent();
            loggedInUser = loggedInPerson;
            if (loggedInUser.Usertype == "Storekeeper")
            {
                btnAdd.Visibility = Visibility.Hidden;
                btnRemove.Visibility = Visibility.Hidden;
            }
            btnAdd.Visibility = Visibility.Hidden;
            updateTheList();
            hidePanel();
            updateComboboxs();
        }
        private void updateTheList()
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                var order = ctx.Bestellings.Select(x => new
                {
                    Id = x.ID,
                    Datum = x.DatumOpgemaakt,
                    Person = x.Personeelslid.Voornaam + " " + x.Personeelslid.Achternaam,
                    Leveran = x.Leverancier.Company,
                    Klant = x.Klant.Voornaam + " " + x.Klant.Achternaam
                });
                listViewOrder.SelectedValuePath = "Id";
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
        // nem szugseges
        //private void btnAdd_Click(object sender, RoutedEventArgs e)
        //{
        //    updateComboboxs();
        //    listViewOrder.IsEnabled = false;
        //    viewListRow.Height = new GridLength(414, GridUnitType.Star);
        //    EditRow.Height = new GridLength(117, GridUnitType.Star);
        //    Editpanel.Visibility = Visibility.Visible;
        //    Editpanel.IsEnabled = true;
        //    btnSave.Visibility = Visibility.Hidden;
        //    btnAddNew.Visibility = Visibility.Visible;
        //    dtDatum.Text = string.Empty;
        //    cmbPersonmember.SelectedValue = -1;
        //    cmbSupplier.SelectedValue = -1;
        //    cmbKlant.SelectedValue = -1;

        //}
        // Nem szugseges
        //private void btnAddNew_Click(object sender, RoutedEventArgs e)
        //{
        //    if (listViewOrder.SelectedValue != null)
        //    {

        //        using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
        //        {
        //            ctx.Bestellings.Add(new Bestelling()
        //            {
        //                DatumOpgemaakt = Convert.ToDateTime(dtDatum.Text),
        //                PersoneelslidID = (int)cmbPersonmember.SelectedValue,
        //                LeverancierID = (int)cmbSupplier.SelectedValue,
        //                KlantID = (int)cmbKlant.SelectedValue
        //            });
        //            MessagBoxInfo.Show("New order seccessfully created.", MessagBoxInfo.CmessageBoxTitle.Info);
        //            ctx.SaveChanges();
        //            hidePanel();
        //            updateTheList();
        //        }
        //    }
        //}
        private void hidePanel()
        {
            viewListRow.Height = new GridLength(531, GridUnitType.Star);
            EditRow.Height = new GridLength(0, GridUnitType.Star);
            Editpanel.Visibility = Visibility.Hidden;
            Editpanel.IsEnabled = false;
            listViewOrder.IsEnabled = true;
            dtDatum.Text = string.Empty;
            cmbPersonmember.SelectedValue = -1;
            cmbSupplier.SelectedValue = -1;
            cmbKlant.SelectedValue = -1;


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

            updateEditForm();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                if (listViewOrder.SelectedValue != null && chekText(true))
                {
                    do
                    {
                        try
                        {
                            var select = ctx.Bestellings.Select(x => x).Where(x => x.ID == (int)listViewOrder.SelectedValue).FirstOrDefault();
                            select.KlantID = (int)cmbKlant.SelectedValue;
                            select.PersoneelslidID = (int)cmbPersonmember.SelectedValue;
                            select.LeverancierID = (int)cmbSupplier.SelectedValue;
                            select.DatumOpgemaakt = Convert.ToDateTime(dtDatum.Text);

                            System.Windows.Forms.DialogResult result = MyMessageBox.Show("Are you sure you want to change the data?", MyMessageBox.CMessageBoxButton.Yes, MyMessageBox.CMessageBoxButton.No);
                            if (result == System.Windows.Forms.DialogResult.Yes)
                            {
                                ctx.SaveChanges();
                                hidePanel();
                                updateTheList();
                                MessagBoxInfo.Show("Succesful", MessagBoxInfo.CmessageBoxTitle.Info);
                            }
                        }
                        catch
                        {
                            MessagBoxInfo.Show("Something wring", MessagBoxInfo.CmessageBoxTitle.Error);
                        }
                    } while (false);
                }
                else MessagBoxInfo.Show("Something missing", MessagBoxInfo.CmessageBoxTitle.Error);
            }
        }

        private void listViewOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            updateEditForm();
        }
        private void updateEditForm()
        {
            using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            {
                if (listViewOrder.SelectedValue != null)
                {
                    var select = ctx.Bestellings.Select(x => x).Where(x => x.ID == (int)listViewOrder.SelectedValue).FirstOrDefault();
                    cmbKlant.SelectedValue = select.KlantID;
                    cmbPersonmember.SelectedValue = select.PersoneelslidID;
                    cmbSupplier.SelectedValue = select.LeverancierID;
                    dtDatum.SelectedDate = select.DatumOpgemaakt;
                }
            }
        }
        private bool chekText(bool noerror)
        {
            if (cmbKlant.SelectedValue != null || cmbPersonmember.SelectedValue != null &&
                cmbSupplier.SelectedValue != null && dtDatum.Text != string.Empty)
            {
                noerror = true;
            }
            else noerror = false;
            return noerror;
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (listViewOrder.SelectedValue != null)
            {
                using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
                {
                    var bestelingProd = ctx.BestellingProducts.Select(x => x).ToList();
                    if (bestelingProd.Contains(listViewOrder.SelectedValue))
                    {
                        System.Windows.Forms.DialogResult result = MyMessageBox.Show("Are you sure you want to delete the order and all the datas? ", MyMessageBox.CMessageBoxButton.Yes, MyMessageBox.CMessageBoxButton.No);
                        if (result == System.Windows.Forms.DialogResult.Yes)
                        {
                            var bestProd = ctx.BestellingProducts.RemoveRange(ctx.BestellingProducts.Where(x => x.BestellingID == (int)listViewOrder.SelectedValue));
                            var best = ctx.Bestellings.RemoveRange(ctx.Bestellings.Where(x => x.ID == (int)listViewOrder.SelectedValue));
                            MessagBoxInfo.Show("Data successfully deleted", MessagBoxInfo.CmessageBoxTitle.Info);
                            ctx.SaveChanges();
                            updateTheList();
                        }
                    }
                    else
                    {
                        var best1 = ctx.Bestellings.RemoveRange(ctx.Bestellings.Where(x => x.ID == (int)listViewOrder.SelectedValue));
                        ctx.SaveChanges();
                        updateTheList();
                    } 
                }
            }
        }
    }
}
