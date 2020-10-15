using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ComputerShop
{
    /// <summary>
    /// Interaction logic for MessageBox.xaml
    /// </summary>
    public partial class MyMessageBox : Window
    {
        public MyMessageBox()
        {
            InitializeComponent();
        }
        static MyMessageBox myMessageBox;
        static DialogResult result = System.Windows.Forms.DialogResult.No;
        public enum CMessageBoxButton
        {
            Yes,
            No,
           
        }
        public enum CmessageBoxTitle
        {
            Info,
            Warning
            
        }
        public static DialogResult Show(string message,/* CmessageBoxTitle titel*/ CMessageBoxButton btnYes, CMessageBoxButton btnNo)
        {
            myMessageBox = new MyMessageBox();
            myMessageBox.txtMessagebx.Text = message;
            myMessageBox.btnYes.Content = myMessageBox.GetMessageButton(btnYes);
            myMessageBox.btnNo.Content = myMessageBox.GetMessageButton(btnNo);
            //myMessageBox.txtTitel.Text = myMessageBox.GetTitle(titel);
            //icon
            myMessageBox.ShowDialog();
            return result;

        }
        //public string GetTitle(CmessageBoxTitle value)
        //{
        //    return Enum.GetName(typeof(CmessageBoxTitle), value);
        //}

        public string GetMessageButton(CMessageBoxButton value)
        {
            return Enum.GetName(typeof(CMessageBoxButton), value);
        }
        private void gBaar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch { } 
        }


        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        DoubleAnimation anim;
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Closing -= Window_Closing;
            e.Cancel = true;
            anim = new DoubleAnimation(0, duration: (Duration)TimeSpan.FromSeconds(0.3));
            anim.Completed += (s,_) => this.Close();
            this.BeginAnimation(UIElement.OpacityProperty, anim);
        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            result = System.Windows.Forms.DialogResult.Yes;
            myMessageBox.Close();
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            result = System.Windows.Forms.DialogResult.No;
            myMessageBox.Close();
        }
    }
}
