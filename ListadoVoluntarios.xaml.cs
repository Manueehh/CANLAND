using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;

namespace ProyectoIPO
{
    /// <summary>
    /// Lógica de interacción para ListadoVoluntarios.xaml
    /// </summary>
    public partial class ListadoVoluntarios : Window
    {
        private string fotografia;
        private string usuario;
        private int contador = 0;
        private BitmapImage imgCross = new BitmapImage(new Uri("/Resources/close.png", UriKind.Relative));
        private BitmapImage imgEdit = new BitmapImage(new Uri("/Resources/editar.png", UriKind.Relative));
        private BitmapImage imgTick = new BitmapImage(new Uri("/Resources/check.png", UriKind.Relative));
        private VentanaPrincipal seleccion;
        private List<Voluntario> lista;
        private string familiatxt;
        private FontFamily font;
        private List<Perro> listadoPerros;
        private List<Socio> listadoSocios;
        public ListadoVoluntarios(string userLog,string familia, List<Perro> lstPerros, List<Voluntario> listVolunt, List<Socio> lstSocios)
        {
            listadoPerros = lstPerros;
            lista = listVolunt;
            listadoSocios = lstSocios;
            DataContext = lista;
            usuario = userLog;
            familiatxt = familia;
            InitializeComponent();
            font = new FontFamily(familiatxt);
            lstVoluntarios.FontFamily = font;
            headergrpboxDatos.FontFamily = font;
            lblNombre.FontFamily = font;
            txtNombre.FontFamily = font;
            lblApellido.FontFamily = font;
            txtApellido.FontFamily = font;
            lblEdad.FontFamily = font;
            txtEdad.FontFamily = font;
            lblDNI.FontFamily = font;
            txtDNI.FontFamily = font;
            lblFotografia.FontFamily = font;
            headergrpboxContacto.FontFamily = font;
            lblCorreo.FontFamily = font;
            txtCorreo.FontFamily = font;
            lblTelefono.FontFamily = font;
            txtTelefono.FontFamily = font;
            headergrpboxDisponibilidad.FontFamily = font;
            lblHorario.FontFamily = font;
            txtHorario.FontFamily = font;
            lblZona.FontFamily = font;
            txtZona.FontFamily = font;
            headergrpboxGestion.FontFamily = font;
            btnAnadir.FontFamily = font;
            btnEditar.FontFamily = font;
            btnEliminar.FontFamily = font;
            btnLimpiar.FontFamily = font;
            lblusuario.FontFamily = font;
            lblUF.FontFamily = font;
            txtUser.FontFamily = font;
            txtUF.FontFamily = font;
            btnAtras.FontFamily = font;
            txtUser.Text = usuario;
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            contador += 1;
            if (contador % 2 == 0)
            {
                if (Confirmacion())
                {
                    btnEditar.IsEnabled = false;
                    var nuevoVoluntario = new Voluntario("", "", 0, "", "", "", null, "", "");
                    imgEditar.Source = imgEdit;
                    lstVoluntarios.IsEnabled = true;
                    txtEditar.Text = "Editar";
                    grpBoxContacto.Opacity = 0.7;
                    grpBoxDatos.Opacity = 0.7;
                    grpBoxDisponibilidad.Opacity = 0.7;
                    txtNombre.IsReadOnly = true;
                    txtApellido.IsReadOnly = true;
                    txtEdad.IsReadOnly = true;
                    txtDNI.IsReadOnly = true;
                    txtCorreo.IsReadOnly = true;
                    txtTelefono.IsReadOnly = true;
                    txtHorario.IsReadOnly = true;
                    txtZona.IsReadOnly = true;
                    btnFotoAnadir.IsEnabled = false;

                    if (fotografia == null)
                    {
                        nuevoVoluntario = new Voluntario(txtNombre.Text, txtApellido.Text, Convert.ToInt32(txtEdad.Text), txtCorreo.Text, txtDNI.Text, txtTelefono.Text,
                        new Uri(txtHide.Text, UriKind.Relative), txtHorario.Text, txtZona.Text);
                    }
                    else
                    {
                        nuevoVoluntario = new Voluntario(txtNombre.Text, txtApellido.Text, Convert.ToInt32(txtEdad.Text), txtCorreo.Text, txtDNI.Text, txtTelefono.Text,
                        new Uri(fotografia, UriKind.Absolute), txtHorario.Text, txtZona.Text);
                    }
                    lista.RemoveAt(lstVoluntarios.SelectedIndex);
                    lista.Insert(lstVoluntarios.SelectedIndex, nuevoVoluntario);
                    lstVoluntarios.Items.Refresh();
                }
                
            }
            else if (contador % 2 != 0)
            {
                fotografia = null;
                imgEditar.Source = imgCross;
                txtEditar.Text = "Guardar";
                lstVoluntarios.IsEnabled = false;
                grpBoxContacto.Opacity = 1.0;
                grpBoxDatos.Opacity = 1.0;
                grpBoxDisponibilidad.Opacity = 1.0;
                txtNombre.IsReadOnly = false;
                txtApellido.IsReadOnly = false;
                txtEdad.IsReadOnly = false;
                txtDNI.IsReadOnly = false;
                txtCorreo.IsReadOnly = false;
                txtTelefono.IsReadOnly = false;
                txtHorario.IsReadOnly = false;
                txtZona.IsReadOnly = false;
                btnFotoAnadir.IsEnabled = true;
                btnEditar.ToolTip = "Pulse para guardar la edición del voluntario";
            }
        }

        private void btnAtras_Click(object sender, RoutedEventArgs e)
        {
            seleccion = new VentanaPrincipal(usuario, familiatxt, listadoPerros,lista,listadoSocios);
            seleccion.Show();
            this.Hide();
        }
        public List<Voluntario> CargarContenidoXML()
        {
            List<Voluntario> listado = new List<Voluntario>();
            XmlDocument doc = new XmlDocument();
            var fichero = Application.GetResourceStream(new Uri("Datos/Voluntarios.xml", UriKind.Relative));
            doc.Load(fichero.Stream);

            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                var nuevoVoluntario = new Voluntario("", "", 0, "", "", "", null, "", "");
                nuevoVoluntario.nombre = node.Attributes["nombre"].Value;
                nuevoVoluntario.apellido = node.Attributes["apellido"].Value;
                nuevoVoluntario.edad = Convert.ToInt32(node.Attributes["edad"].Value);
                nuevoVoluntario.Correo = node.Attributes["Correo"].Value;
                nuevoVoluntario.DNI = node.Attributes["DNI"].Value;
                nuevoVoluntario.telefono = node.Attributes["telefono"].Value;
                nuevoVoluntario.foto = new Uri(node.Attributes["foto"].Value, UriKind.Relative);
                nuevoVoluntario.horario = node.Attributes["horario"].Value;
                nuevoVoluntario.zona = node.Attributes["zona"].Value;
                listado.Add(nuevoVoluntario);
            }
            return listado;
        }

        private void lstVoluntarios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnEditar.IsEnabled = true;
            grpBoxDatos.Opacity = 0.7;
            grpBoxContacto.Opacity = 0.7;
            grpBoxDisponibilidad.Opacity = 0.7;
            txtNombre.IsReadOnly = true;
            txtApellido.IsReadOnly = true;
            txtEdad.IsReadOnly = true;
            txtDNI.IsReadOnly = true;
            txtCorreo.IsReadOnly = true;
            txtTelefono.IsReadOnly = true;
            txtHorario.IsReadOnly = true;
            txtZona.IsReadOnly = true;
            btnAnadir.IsEnabled = false;
            btnFotoAnadir.IsEnabled = false;
            imgRelleno.Source = null;
            btnEliminar.IsEnabled = true;
            contador = 0;
            txtEditar.Text = "Editar";
            imgEditar.Source = imgEdit;
            if (lstVoluntarios.SelectedIndex == -1)
            {
                btnEditar.IsEnabled = false;
                btnEliminar.IsEnabled = false;
                btnEditar.IsEnabled = false;
                grpBoxDatos.Opacity = 1.0;
                grpBoxContacto.Opacity = 1.0;
                grpBoxDisponibilidad.Opacity = 1.0;
                txtNombre.IsReadOnly = false;
                txtApellido.IsReadOnly = false;
                txtEdad.IsReadOnly = false;
                txtDNI.IsReadOnly = false;
                txtCorreo.IsReadOnly = false;
                txtTelefono.IsReadOnly = false;
                txtHorario.IsReadOnly = false;
                txtZona.IsReadOnly = false;
                btnAnadir.IsEnabled = true;
                btnFotoAnadir.IsEnabled = true;
                imgRelleno.Source = null;
                btnEliminar.IsEnabled = false;
            }
        }


        private void btnAnadir_Click(object sender, RoutedEventArgs e)
        {
            if (txtNombre.Text != "" && txtApellido.Text != "" && txtEdad.Text != "" && txtDNI.Text != "" && fotografia != "" &&
                txtCorreo.Text != "" && txtTelefono.Text != "" && txtHorario.Text != "" && txtZona.Text != "")
            {
                if (Confirmacion())
                {
                    var nuevoVoluntario = new Voluntario(txtNombre.Text, txtApellido.Text, Convert.ToInt32(txtEdad.Text), txtCorreo.Text, txtDNI.Text, txtTelefono.Text,
                    new Uri(fotografia, UriKind.Absolute), txtHorario.Text, txtZona.Text);
                    lista.Add(nuevoVoluntario);
                    lstVoluntarios.Items.Refresh();
                }
            }
            else
            {
                MessageBox.Show("Hay algun campo vacío, asegurese de rellenar todos los campos correctamente", "Error");
            }
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

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
            imgEditar.Source = imgEdit;
            txtEditar.Text = "Editar";
            contador = 0;
            btnEditar.IsEnabled = false;
            btnEliminar.IsEnabled = false;
        }
        private void Limpiar()
        {
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtEdad.Text = "";
            txtDNI.Text = "";
            txtCorreo.Text = "";
            txtTelefono.Text = "";
            txtHorario.Text = "";
            txtZona.Text = "";
            lstVoluntarios.SelectedIndex = -1;
            btnEditar.IsEnabled = false;
            grpBoxDatos.Opacity = 1.0;
            grpBoxContacto.Opacity = 1.0;
            grpBoxDisponibilidad.Opacity = 1.0;
            txtNombre.IsReadOnly = false;
            txtApellido.IsReadOnly = false;
            txtEdad.IsReadOnly = false;
            txtDNI.IsReadOnly = false;
            txtCorreo.IsReadOnly = false;
            txtTelefono.IsReadOnly = false;
            txtHorario.IsReadOnly = false;
            txtZona.IsReadOnly = false;
            btnAnadir.IsEnabled = true;
            btnFotoAnadir.IsEnabled = true;
            imgRelleno.Source = null;
        }

        private void btnFotoAnadir_Click(object sender, RoutedEventArgs e)
        {
            var abrirDialog = new OpenFileDialog();
            abrirDialog.Filter = "Images|*.jpg;*.png;*.gif;*.bmp;*.jpeg";
            if (abrirDialog.ShowDialog() == true)
            {
                try
                {
                    fotografia = abrirDialog.FileName;
                    var bitmap = new BitmapImage(new Uri(abrirDialog.FileName, UriKind.Absolute));
                    imgRelleno.Source = imgTick;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar la imagen " + ex.Message);
                }
            }
        }

        private void txtEdad_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txtTelefono_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (Confirmacion())
            {
                lista.RemoveAt(lstVoluntarios.SelectedIndex);
                lstVoluntarios.Items.Refresh();
                Limpiar();
            }
            else
            {
                MessageBox.Show("No se eliminará al voluntario", "Alerta del sistema");
            }
        }

        private void txtCorreo_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtCorreo.Text.Contains("@") == false)
            {
                txtCorreo.Background = Brushes.Red;
            }
            else
            {
                txtCorreo.Background = Brushes.White;
            }
            if (txtCorreo.Text.EndsWith(".com") == true || txtCorreo.Text.EndsWith(".es") == true)
            {
                txtCorreo.Background = Brushes.White;
            }
            else
            {
                txtCorreo.Background = Brushes.Red;
            }
        }

        private void txtDNI_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtDNI.Text.Length==9)
            {
                if ("01234567890".IndexOf(txtDNI.Text.Substring(txtDNI.Text.Length - 1)) > -1)
                {
                    txtDNI.Background = Brushes.Red;
                }
                else
                {
                    txtDNI.Background = Brushes.White;
                }
            }
        }

        private void txtNombre_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int ascci = Convert.ToInt32(Convert.ToChar(e.Text));
            if (ascci >= 65 && ascci <= 90 || ascci >= 97 && ascci <= 122 || ascci == 13)
            {
                e.Handled = false;
            }
            else
            {
                MessageBox.Show("No debe introducir números en este campo", "Error en el campo");
                e.Handled = true;
            }
        }

        private void txtApellido_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int ascci = Convert.ToInt32(Convert.ToChar(e.Text));
            if (ascci >= 65 && ascci <= 90 || ascci >= 97 && ascci <= 122 || ascci == 13)
            {
                e.Handled = false;
            }
            else
            {
                MessageBox.Show("No debe introducir números en este campo", "Error en el campo");
                e.Handled = true;
            }
        }
    }
}
