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
using System.Windows.Threading;

namespace ComputerShop
{
    /// <summary>
    /// Interaction logic for Overwiew.xaml
    /// </summary>
    public partial class Overwiew : Window
    {
        public Overwiew()
        {
            InitializeComponent();
            DispatcherTimer timeNow = new DispatcherTimer();
            timeNow.Tick += new EventHandler(UpdateTimer_Tick);
            timeNow.Interval = new TimeSpan(0, 0, 1);
            timeNow.Start();
        }
        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            txtDateTime.Text = DateTime.Now.ToString("g");
        }
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListView.SelectedIndex;
            MoveCursorMenu(index);

            switch (index)
            {
                case 0:
                    gridPages.Children.Clear();
                    gridPages.Children.Add(new SupplierOverview());
                    break;
                case 1:
                    gridPages.Children.Clear();
                    gridPages.Children.Add(new CustomerOverview());
                    break;
                case 2:
                    gridPages.Children.Clear();
                    gridPages.Children.Add(new ProductPage());
                    break;
                case 3:
                    gridPages.Children.Clear();
                    gridPages.Children.Add(new CategoryUserControl1());
                    break;
                case 4:
                    gridPages.Children.Clear();
                    gridPages.Children.Add(new PersonMembersUserControl1());
                    break;
                case 5:
                    gridPages.Children.Clear();
                    gridPages.Children.Add(new OrderUserControl1());
                    break;
                case 6:
                    gridPages.Children.Clear();
                    gridPages.Children.Add(new OrderProductUserControl1());
                    break;
                case 7:
                    MainWindow mainWindow = new MainWindow();
                    this.Close();
                    mainWindow.ShowDialog();
                    break;
                case 8:
                    //MainMenu mainMenu = new MainMenu(selectUser)
                    //this.Close();
                    //mainMenu.ShowDialog();
                    break;
                default:
                    break;
            }
        }

        private void MoveCursorMenu(int index)
        {
            transationSlide.OnApplyTemplate();
            sideGridCursor.Margin = new Thickness(0, (50 + (60 * index)), 0, 0);
        }

        private void btnEcs_Click(object sender, RoutedEventArgs e)
        {
            {
                System.Windows.Application.Current.Shutdown();
            }
        }
    }
}
