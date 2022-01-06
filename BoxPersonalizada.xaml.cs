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

namespace ProyectoIPO
{
    /// <summary>
    /// Lógica de interacción para BoxPersonalizada.xaml
    /// </summary>
    public partial class BoxPersonalizada : Window
    {
        private BoxPersonalizada newMessageBox;
        public BoxPersonalizada()
        {
            InitializeComponent();
        }
        public void ShowBox(string txtMessage, string txtTitle)
        {
            newMessageBox = new BoxPersonalizada();
            newMessageBox.lblTxt.Content = txtMessage;
            newMessageBox.Title = txtTitle;
            newMessageBox.ShowDialog();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
