using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
using System.Windows.Threading;

namespace ComputerShop
{
    /// <summary>
    /// Interaction logic for Shopping.xaml
    /// </summary>
    public partial class Shopping : Window
    {
        
        public Personeelslid loggedInUser { get; set; }
        public Shopping(Personeelslid loggedInPerson)
        {
            InitializeComponent();
            DispatcherTimer timeNow = new DispatcherTimer();
            timeNow.Tick += new EventHandler(UpdateTimer_Tick);
            timeNow.Interval = new TimeSpan(0, 0, 1);
            timeNow.Start();
            loggedInUser = loggedInPerson;
            switch (loggedInPerson.Usertype)
            {
                case "Seller":
                    btnCust.IsEnabled = false;
                  
                    break;
                case "Administrator":
                    btnSupl.IsEnabled = true;
                    btnCust.IsEnabled = true;
                    break;
                case "Storekeeper":
                    btnSupl.IsEnabled = false;
                    break;
            }
        }
        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            txtDateTime.Text = DateTime.Now.ToString("g");
        }

       

        private void btnEsc_Click(object sender, RoutedEventArgs e)
        {
            //using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
            //{
            //    foreach (var item in myProductinKars)
            //    {
            //        var product = ctx.Products.Select(x => x).Where(x => x.ID == item.ProductinKarID).FirstOrDefault();
            //        product.InStock = product.InStock + item.Quantity;
            //        ctx.SaveChanges();
            //    }
            //}
            //System.Windows.Application.Current.Shutdown();
        }

       
        private void btnCust_Click(object sender, RoutedEventArgs e)
        {
            gridPages.Children.Clear();
            gridPages.Children.Add(new ShoppingCustomerUSC(loggedInUser));
        }

        private void btnSupl_Click(object sender, RoutedEventArgs e)
        {
            gridPages.Children.Clear();
            gridPages.Children.Add(new ShoppingSupplierUSC(loggedInUser));
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            MessagBoxInfo.Show("Antal Palfi\n antalpalfi90@gmail.com", MessagBoxInfo.CmessageBoxTitle.Info);
        }
        private void btnSmyle_Click(object sender, RoutedEventArgs e)
        {
            HappySmylexaml mySmyle = new HappySmylexaml(loggedInUser);
            this.Close();
            mySmyle.ShowDialog();
        }
        //private void btnBack_Click(object sender, RoutedEventArgs e)
        //{
        //    using (IndividueelProjectEntities2 ctx = new IndividueelProjectEntities2())
        //    {
        //        foreach (var item in myProductinKars)
        //        {
        //            var product = ctx.Products.Select(x => x).Where(x => x.ID == item.ProductinKarID).FirstOrDefault();
        //            product.InStock = product.InStock + item.Quantity;
        //            ctx.SaveChanges();
        //        }
        //    }
        //    MainMenu mainMenu = new MainMenu(loggedInUser);
        //    this.Close();
        //    mainMenu.ShowDialog();
        //}
    }
}
