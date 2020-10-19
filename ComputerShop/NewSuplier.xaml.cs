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
    /// Interaction logic for NewSuplier.xaml
    /// </summary>
    public partial class NewSuplier : Window
    {
        public NewSuplier()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
          
            if (txtCompany.Text != "" && txtContactPerson.Text != "" && txtEmail.Text != "" && txtHouseNumber.Text != "" && txtMailbox.Text != "" && txtPhone.Text != "" && txtStreet.Text != "" && txtTown.Text != "" && txtZipcode.Text != "")
            {
                using (ComputerWareHousProject ctx = new ComputerWareHousProject())
                {
                    var sup = ctx.Leveranciers.Where(x => x.Company == txtCompany.Text && x.Contactpersoon == txtContactPerson.Text).Count();

                    if (sup == 0)
                    {
                            ctx.Leveranciers.Add(new Leverancier()
                            {
                                Contactpersoon = txtContactPerson.Text,
                                Company = txtCompany.Text,
                                Telefoonnummer = txtPhone.Text,
                                Emailadres = txtEmail.Text,
                                Straatnaam = txtStreet.Text,
                                Huisnummer = Convert.ToInt32(txtHouseNumber.Text),
                                Bus = Convert.ToInt32(txtMailbox.Text),
                                Postcode = Convert.ToInt32(txtZipcode.Text),
                                Gemeente = txtTown.Text
                            });
                            ctx.SaveChanges();
                            string newSuppplier = $"{txtCompany.Text}\n" +
                                $"{txtContactPerson.Text}\n" +
                                $"{txtPhone.Text}\n" +
                                $"{txtEmail.Text}\n" +
                                $"{txtStreet.Text} {txtHouseNumber.Text} {txtMailbox.Text}\n" +
                                $"{txtZipcode.Text}\n" +
                                $"{txtTown.Text}";
                            System.Windows.Forms.DialogResult result = MessagBoxInfo.Show(newSuppplier, MessagBoxInfo.CmessageBoxTitle.Info);
                        if (result == System.Windows.Forms.DialogResult.OK)
                        {
                            this.Close();
                        }
                    }
                    else 
                    {
                            MessagBoxInfo.Show("Company is already exist",MessagBoxInfo.CmessageBoxTitle.Error);
                    }
                }
            }
            else MessagBoxInfo.Show("Lookup again",MessagBoxInfo.CmessageBoxTitle.Warning);
        }

        private void btnEsc_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
