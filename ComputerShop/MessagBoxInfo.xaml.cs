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
    /// Interaction logic for MessagBoxInfo.xaml
    /// </summary>
    public partial class MessagBoxInfo : Window
    {
        public MessagBoxInfo()
        {
            InitializeComponent();
        }
        static MessagBoxInfo myMessageBoxInfo;
        static DialogResult result = System.Windows.Forms.DialogResult.No;

        //public enum CMessageBoxButton
        //{
        //    Ok,

        //}
        public enum CmessageBoxTitle
        {
            Info,
            Warning,
            Error

        }
        public static DialogResult Show(string message, CmessageBoxTitle title)
        {

            myMessageBoxInfo = new MessagBoxInfo();
            myMessageBoxInfo.txtMessagebx.Text = message;
            //myMessageBoxInfo.btnOk = myMessageBoxInfo.GetMessageButton(btnOk);
            myMessageBoxInfo.txtTitel.Text = myMessageBoxInfo.GetTitle(title);
            //icon
            switch (title)
            {
                case CmessageBoxTitle.Info:
                    myMessageBoxInfo.iconMsg.Kind = MaterialDesignThemes.Wpf.PackIconKind.InfoCircle;
                    myMessageBoxInfo.iconMsg.Foreground = Brushes.Blue;
                    break;

                case CmessageBoxTitle.Warning:
                    myMessageBoxInfo.iconMsg.Kind = MaterialDesignThemes.Wpf.PackIconKind.Warning;
                    myMessageBoxInfo.iconMsg.Foreground = Brushes.Yellow;
                    break;

                case CmessageBoxTitle.Error:
                    myMessageBoxInfo.iconMsg.Kind = MaterialDesignThemes.Wpf.PackIconKind.Error;
                    myMessageBoxInfo.iconMsg.Foreground = Brushes.DarkRed;
                    break;

                default:
                    break;
            }
            myMessageBoxInfo.ShowDialog();
            return result;

        }
        public string GetTitle(CmessageBoxTitle value)
        {
            return Enum.GetName(typeof(CmessageBoxTitle), value);
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
            anim.Completed += (s, _) => this.Close();
            this.BeginAnimation(UIElement.OpacityProperty, anim);
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            result = System.Windows.Forms.DialogResult.OK;
            myMessageBoxInfo.Close();
        }
       
    }
}

