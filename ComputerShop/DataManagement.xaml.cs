﻿using System;
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
    /// Interaction logic for DataManagement.xaml
    /// </summary>
    public partial class DataManagement : Window
    {
        public DataManagement()
        {
            InitializeComponent();
            DispatcherTimer timeNow = new DispatcherTimer();
            timeNow.Tick += new EventHandler(UpdateTimer_Tick);
            timeNow.Interval = new TimeSpan(0, 0, 1);
            timeNow.Start();
        }
        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            txtDateTime.Text = DateTime.Now.ToString();
        }


        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListView.SelectedIndex;
            MoveCursorMenu(index);
            switch (index)
            {
                case 0:
                    gridPages.Children.Clear();
                    gridPages.Children.Add(new Supplier());
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}