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
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private string usuario = "Admin";
        private string password = "Protectora";
        private BitmapImage imgCheck = new BitmapImage(new Uri("/Resources/check.png", UriKind.Relative));
        private BitmapImage imgCross = new BitmapImage(new Uri("/Resources/close.png", UriKind.Relative));
        private VentanaPrincipal Seleccion;
        private string familia = "Segoe UI";
        private List<Voluntario> listadoVoluntarios = new List<Voluntario>();
        private List<Perro> listadoPerros = new List<Perro>();
        private List<Socio> listadoSocios = new List<Socio>();
        public Login()
        {
            ListadoPerros listadoperr = new ListadoPerros("", "", null, null, null);
            ListadoVoluntarios listadoVol = new ListadoVoluntarios("", "", null, null, null);
            ListadoSocios listadoSoc = new ListadoSocios("", "", null, null, null);
            listadoPerros = listadoperr.CargarContenidoXML();
            listadoVoluntarios = listadoVol.CargarContenidoXML();
            listadoSocios = listadoSoc.CargarContenidoXML();
            familia = "Segoe UI";
            InitializeComponent();
        }
        private Boolean ComprobarEntrada(string ValorIntroducido, string valorValido, Control compononenteEntrada, Image imagenFeedBack)
        {
            Boolean valido = false;
            compononenteEntrada.BorderThickness = new Thickness(2);
            if (ValorIntroducido.Equals(valorValido))
            {
                compononenteEntrada.BorderBrush = Brushes.Green;
                compononenteEntrada.Background = Brushes.LightGreen;
                imagenFeedBack.Source = imgCheck;
                valido = true;
            }
            else
            {
                compononenteEntrada.BorderBrush = Brushes.Red;
                compononenteEntrada.Background = Brushes.Red;
                imagenFeedBack.Source = imgCross;
            }
            return valido;
        }
        private void Login_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBox.Show("Gracias por usar nuestra aplicación!", "Despedida");
        }
        private void txtUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                if (!String.IsNullOrEmpty(txtUsuario.Text) && ComprobarEntrada(txtUsuario.Text, usuario, txtUsuario, imgCheckUsuario))
                {
                    passContrasena.IsEnabled = true;
                    passContrasena.Focus();
                    txtUsuario.IsEnabled = false;
                }
                
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ComprobarEntrada(txtUsuario.Text, usuario,txtUsuario, imgCheckUsuario) && ComprobarEntrada(passContrasena.Password, password, passContrasena, imgCheckContrasena))
            {
                familia = familiaTXT.Text;
                Seleccion = new VentanaPrincipal(usuario,familia,listadoPerros,listadoVoluntarios,listadoSocios);
                Seleccion.Show();
                this.Hide();
            }
        }

        private void passContrasena_KeyUp(object sender, KeyEventArgs e)
        {
            if (ComprobarEntrada(passContrasena.Password, password, passContrasena, imgCheckContrasena))
            {
                loginbtn.Focus();
            }
        }

        private void familiaTXT_Loaded(object sender, RoutedEventArgs e)
        {
            this.familiaTXT.SelectedIndex = 47;
        }
    }
}
