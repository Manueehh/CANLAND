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
    /// Lógica de interacción para VentanaPrincipal.xaml
    /// </summary>
    public partial class VentanaPrincipal : Window
    {
        private int contador = 0;
        private ListadoPerros listaPerros;
        private BitmapImage imagRollOver = new BitmapImage(new Uri("/Resources/perrofeliz.png", UriKind.Relative));
        private string usuarioLog;
        private ListadoVoluntarios listaVoluntarios;
        private ListadoSocios listaSocios;
        private string familiatxt;
        private FontFamily font;
        private List<Voluntario> listadoVoluntarios;
        private List<Perro> listadoPerros;
        private List<Socio> listadoSocios;
        public VentanaPrincipal(string usuario, string familia, List<Perro> listPerro, List<Voluntario> listVolunt, List<Socio> listSocios)
        {
            listadoVoluntarios = listVolunt;
            listadoPerros = listPerro;
            listadoSocios = listSocios;
            familiatxt = familia;
            InitializeComponent();
            usuarioLog = usuario;
            txtUsuario.Text = usuarioLog;
            font = new FontFamily(familiatxt);
            txtAcariciar.FontFamily = font;
            txtFecha.FontFamily = font;
            txtPerros.FontFamily = font;
            txtSalir.FontFamily = font;
            txtSocios.FontFamily = font;
            txtUsuario.FontFamily = font;
            txtVoluntarios.FontFamily = font;
            gstnPerros.FontFamily = font;
            gstnSocios.FontFamily = font;
            gstnVoluntarios.FontFamily = font;
            lblBienvenida.FontFamily = font;
            lblUF.FontFamily = font;
            lblUsuario.FontFamily = font;
        }
        private bool Confirmacion()
        {
            bool confirmacion = false;
            MessageBoxResult result = MessageBox.Show("¿Segur@ de que quiere realizar la acción? Los cambios son irreversibles", "Confirmación", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    confirmacion = true;
                    break;
                case MessageBoxResult.No:
                    confirmacion = false;
                    break;
            }
            return confirmacion;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            perroimg.Source = imagRollOver;
            contador += 1;
            switch (contador)
            {
                case 2:
                    corazon1.Visibility = Visibility.Visible;
                    break;
                case 3:
                    corazon2.Visibility = Visibility.Visible;
                    break;
                case 4:
                    corazon3.Visibility = Visibility.Visible;
                    break;
                case 5:
                    corazon4.Visibility = Visibility.Visible;
                    break;
                case 6:
                    corazon5.Visibility = Visibility.Visible;
                    MessageBox.Show("¡Gracias por hacer al perro feliz!", "Agradecimiento");
                    break;
            }
        }

        private void btnPerros_Click(object sender, RoutedEventArgs e)
        {
            listaPerros = new ListadoPerros(usuarioLog,familiatxt,listadoPerros,listadoVoluntarios,listadoSocios);
            listaPerros.Show();
            this.Hide();
        }

        private void btnVoluntarios_Click(object sender, RoutedEventArgs e)
        {
            listaVoluntarios = new ListadoVoluntarios(usuarioLog,familiatxt,listadoPerros,listadoVoluntarios,listadoSocios);
            listaVoluntarios.Show();
            this.Hide();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            BoxPersonalizada boxP = new BoxPersonalizada();
            boxP.ShowBox("¿Está segur@ de que quiere salir? \nDoggo le echará de menos", "Salida");
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BoxAyuda boxayuda = new BoxAyuda();
            boxayuda.Show();
        }

        private void btnSocios_Click(object sender, RoutedEventArgs e)
        {
            listaSocios = new ListadoSocios(usuarioLog, familiatxt,listadoPerros,listadoVoluntarios,listadoSocios);
            listaSocios.Show();
            Hide();
        }
    }
}
