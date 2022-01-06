using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
    /// Lógica de interacción para ListadoPerros.xaml
    /// </summary>
    public partial class ListadoPerros : Window
    {
        private VentanaPrincipal seleccion;
        private string user;
        private List<Perro> listadoPerros;
        private int contador;
        private BitmapImage imgCross = new BitmapImage(new Uri("/Resources/close.png", UriKind.Relative));
        private BitmapImage imgEdit = new BitmapImage(new Uri("/Resources/editar.png", UriKind.Relative));
        private BitmapImage imgTick = new BitmapImage(new Uri("/Resources/check.png", UriKind.Relative));
        private string fotografia;
        private string familiatxt;
        private FontFamily font;
        private List<Voluntario> listadoVoluntarios;
        private List<Socio> listadoSocios;
        public ListadoPerros(string usuario,string familia,List<Perro> listPerros, List<Voluntario> listVoluntarios, List<Socio> listSocios)

        {
            listadoVoluntarios = listVoluntarios;
            listadoSocios = listSocios;
            InitializeComponent();
            txtUsuario.Text = usuario;
            user = usuario;
            listadoPerros = listPerros;
            DataContext = listadoPerros;
            familiatxt = familia;
            font = new FontFamily(familiatxt);
            lstPerros.FontFamily = font;
            headergrpboxPerro.FontFamily = font;
            lblNombre.FontFamily = font;
            txtNombrePerro.FontFamily = font;
            lblGenero.FontFamily=font;
            txtGenero.FontFamily = font;
            lblRaza.FontFamily = font;
            txtRaza.FontFamily = font;
            lblTamano.FontFamily=font;
            txtTamano.FontFamily = font;
            txtPeso.FontFamily = font;
            lblPeso.FontFamily = font;
            txtEdad.FontFamily = font;
            lblEdad.FontFamily = font;
            lblFotografia.FontFamily = font;
            lblDatos.FontFamily = font;
            txtDatos.FontFamily = font;
            lblEsterilizado.FontFamily = font;
            lblVacunado.FontFamily = font;
            lblPPP.FontFamily = font;
            lblPadrino.FontFamily = font;
            headergrpboxPadrino.FontFamily = font;
            lblNombrePadrino.FontFamily = font;
            txtNombrePadrino.FontFamily = font;
            lblAportacion.FontFamily = font;
            txtAportacion.FontFamily = font;
            lblTelefono.FontFamily = font;
            txtTelefono.FontFamily = font;
            lblUsuario.FontFamily = font;
            lblUF.FontFamily = font;
            txtUsuario.FontFamily = font;
            txtUF.FontFamily = font;
            headergrpboxGestion.FontFamily = font;
            btnAnadir.FontFamily = font;
            btnEditar.FontFamily = font;
            btnEliminar.FontFamily = font;
            btnLimpiar.FontFamily = font;
            btnAtras.FontFamily = font;
        }

        public List<Perro> CargarContenidoXML()
        {
            List<Perro> listado = new List<Perro>();
            XmlDocument doc = new XmlDocument();
            var fichero = Application.GetResourceStream(new Uri("Datos/Perros.xml", UriKind.Relative));
            doc.Load(fichero.Stream);

            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                var nuevoPerro = new Perro("", null, "", "", 0, 0.0, 0, "", false, false, false, false, "", 0.0, "");
                nuevoPerro.nombre = node.Attributes["nombre"].Value;
                nuevoPerro.foto = new Uri(node.Attributes["foto"].Value, UriKind.Relative);
                nuevoPerro.genero = node.Attributes["genero"].Value;
                nuevoPerro.raza = node.Attributes["raza"].Value;
                nuevoPerro.tamano = Convert.ToInt32(node.Attributes["tamano"].Value);
                nuevoPerro.peso = Convert.ToDouble(node.Attributes["peso"].Value);
                nuevoPerro.edad = Convert.ToInt32(node.Attributes["edad"].Value);
                nuevoPerro.datosInteresantes = node.Attributes["datosInteresantes"].Value;
                nuevoPerro.esterilizado = Convert.ToBoolean(node.Attributes["esterilizado"].Value);
                nuevoPerro.vacunado = Convert.ToBoolean(node.Attributes["vacunado"].Value);
                nuevoPerro.PPP = Convert.ToBoolean(node.Attributes["PPP"].Value);
                nuevoPerro.Padrino = Convert.ToBoolean(node.Attributes["Padrino"].Value);
                nuevoPerro.nombrePadrino = node.Attributes["nombrePadrino"].Value;
                nuevoPerro.aportacion = Convert.ToDouble(node.Attributes["aportacion"].Value);
                nuevoPerro.telefono = node.Attributes["telefono"].Value;

                listado.Add(nuevoPerro);
            }
            return listado;
        }

        private void txtTamano_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txtPeso_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void txtTelefono_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txtEdad_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void txtAportacion_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.-]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            if (lstPerros.SelectedIndex >= 0)
            {
                bool confirmacion = Confirmacion();
                if (confirmacion)
                {
                    listadoPerros.RemoveAt(lstPerros.SelectedIndex);
                    lstPerros.Items.Refresh();
                    Limpiar();
                }
                else
                {
                    MessageBox.Show("De acuerdo, no se eliminará al perro", "Alerta del sistema");
                }

            }
            else
            {
                MessageBox.Show("¡Debe seleccionar un perro antes de pulsar esa opción!", "Error al borrar");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (txtNombrePerro.Text != "" && txtGenero.Text != "" && txtRaza.Text != "" && txtPeso.Text != "" && txtTamano.Text != "" &&
                txtEdad.Text != "" && fotografia != "" && txtDatos.Text != "")
            {
                if (btnPadrino.IsChecked == true && Confirmacion())
                {

                    var nuevoPerro = new Perro(txtNombrePerro.Text, new Uri(fotografia, UriKind.Absolute), txtGenero.Text, txtRaza.Text, Convert.ToInt32(txtTamano.Text), Convert.ToDouble(txtPeso.Text), Convert.ToInt32(txtEdad.Text),
                        txtDatos.Text, Convert.ToBoolean(btnEsterilizado.IsChecked), Convert.ToBoolean(btnVacunado.IsChecked), Convert.ToBoolean(btnPPP.IsChecked), Convert.ToBoolean(btnPadrino.IsChecked), txtNombrePadrino.Text, 
                        Convert.ToDouble(txtAportacion.Text), txtTelefono.Text);
                    listadoPerros.Add(nuevoPerro);
                    lstPerros.Items.Refresh();
                }
                else
                {
                    var nuevoPerro = new Perro(txtNombrePerro.Text, new Uri(fotografia, UriKind.Absolute), txtGenero.Text, txtRaza.Text, Convert.ToInt32(txtTamano.Text), Convert.ToDouble(txtPeso.Text), Convert.ToInt32(txtEdad.Text),
                        txtDatos.Text, Convert.ToBoolean(btnEsterilizado.IsChecked), Convert.ToBoolean(btnVacunado.IsChecked), Convert.ToBoolean(btnPPP.IsChecked), Convert.ToBoolean(btnPadrino.IsChecked), "", 0.0, "");
                    listadoPerros.Add(nuevoPerro);
                    lstPerros.Items.Refresh();
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
        private void txtNombrePerro_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

        private void txtGenero_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

        private void txtRaza_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
        private void btnPadrino_CheckedChanged(object sender, RoutedEventArgs e)
        {
            if (btnPadrino.IsChecked == false)
            {
                txtNombrePadrino.IsReadOnly = true;
                txtAportacion.IsReadOnly = true;
                txtTelefono.IsReadOnly = true;
                grpBoxPadrino.Opacity = 0.7;
                txtNombrePadrino.Text = "";
                txtAportacion.Text = "";
                txtTelefono.Text = "";
            }
            else
            {
                txtNombrePadrino.IsReadOnly = false;
                txtAportacion.IsReadOnly = false;
                txtTelefono.IsReadOnly = false;
                grpBoxPadrino.Opacity = 1.0;
            }
        }

        private void lstPerros_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtNombrePerro.IsReadOnly = true;
            txtGenero.IsReadOnly = true;
            txtRaza.IsReadOnly = true;
            txtTamano.IsReadOnly = true;
            txtPeso.IsReadOnly = true;
            txtEdad.IsReadOnly = true;
            btnFotoAnadir.IsEnabled = false;
            txtDatos.IsReadOnly = true;
            btnEditar.IsEnabled = true;
            btnEsterilizado.IsEnabled = false;
            btnVacunado.IsEnabled = false;
            btnPPP.IsEnabled = false;
            btnPadrino.IsEnabled = false;
            txtNombrePadrino.IsReadOnly = true;
            txtAportacion.IsReadOnly = true;
            txtTelefono.IsReadOnly = true;
            grpBoxPerro.Opacity = 0.7;
            grpBoxPadrino.Opacity = 0.7;
            btnAnadir.IsEnabled = false;
            btnEditar.IsEnabled = true;
            btnEliminar.IsEnabled = true;
            contador = 0;
            txtEdit.Text = "Editar";
            imgEditar.Source = imgEdit;
            brdPerro.Visibility = Visibility.Visible;
            if (lstPerros.SelectedIndex == -1)
            {
                brdPerro.Visibility = Visibility.Hidden;
                txtNombrePerro.IsReadOnly = false;
                txtGenero.IsReadOnly = false;
                txtRaza.IsReadOnly = false;
                txtTamano.IsReadOnly = false;
                txtPeso.IsReadOnly = false;
                txtEdad.IsReadOnly = false;
                btnFotoAnadir.IsEnabled = true;
                txtDatos.IsReadOnly = false;
                btnEditar.IsEnabled = false;
                btnEsterilizado.IsEnabled = true;
                btnVacunado.IsEnabled = true;
                btnPPP.IsEnabled = true;
                btnPadrino.IsEnabled = true;
                txtNombrePadrino.IsReadOnly = false;
                txtAportacion.IsReadOnly = false;
                txtTelefono.IsReadOnly = false;
                grpBoxPerro.Opacity = 1.0;
                grpBoxPadrino.Opacity = 1.0;
                btnAnadir.IsEnabled = true;
                btnEditar.IsEnabled = false;
                btnEliminar.IsEnabled = false;
            }
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            contador += 1;
            if (contador % 2 != 0)
            {
                imgEditar.Source = imgCross;
                txtEdit.Text = "Guardar";
                lstPerros.IsEnabled = false;
                txtNombrePerro.IsReadOnly = false;
                txtGenero.IsReadOnly = false;
                txtRaza.IsReadOnly = false;
                txtTamano.IsReadOnly = false;
                txtPeso.IsReadOnly = false;
                txtEdad.IsReadOnly = false;
                txtDatos.IsReadOnly = false;
                btnEsterilizado.IsEnabled = true;
                btnVacunado.IsEnabled = true;
                btnPPP.IsEnabled = true;
                btnPadrino.IsEnabled = true;
                btnFotoAnadir.IsEnabled = true;
                grpBoxPerro.Opacity = 1.0;
                btnEditar.ToolTip = "Pulse para guardar la edición del voluntario";
                if (btnPadrino.IsChecked == true)
                {
                    grpBoxPadrino.Opacity = 1.0;
                    txtNombrePadrino.IsReadOnly = false;
                    txtAportacion.IsReadOnly = false;
                    txtTelefono.IsReadOnly = false;
                }
                else
                {
                    grpBoxPadrino.Opacity = 0.7;
                    txtNombrePadrino.IsReadOnly = true;
                    txtAportacion.IsReadOnly = true;
                    txtTelefono.IsReadOnly = true;
                }
            }
            else if (contador % 2 == 0)
            {
                if (Confirmacion())
                {
                    var nuevoPerro = new Perro("", null, "", "", 0, 0.0, 0, "", false, false, false, false, "", 0.0, "");
                    imgEditar.Source = imgEdit;
                    txtEdit.Text = "Editar";
                    lstPerros.IsEnabled = true;
                    txtNombrePerro.IsReadOnly = false;
                    txtGenero.IsReadOnly = false;
                    txtRaza.IsReadOnly = false;
                    txtTamano.IsReadOnly = false;
                    txtPeso.IsReadOnly = false;
                    txtEdad.IsReadOnly = false;
                    txtDatos.IsReadOnly = false;
                    btnEsterilizado.IsEnabled = false;
                    btnVacunado.IsEnabled = false;
                    btnPPP.IsEnabled = false;
                    btnPadrino.IsEnabled = false;
                    grpBoxPerro.Opacity = 0.7;
                    grpBoxPadrino.Opacity = 0.7;
                    txtNombrePadrino.IsReadOnly = false;
                    txtAportacion.IsReadOnly = false;
                    txtTelefono.IsReadOnly = false;
                    btnFotoAnadir.IsEnabled = false;

                    if (btnPadrino.IsChecked == true)
                    {
                        if (txtNombrePadrino.Text != "" && txtAportacion.Text != "" && txtTelefono.Text!="")
                        {
                            if (fotografia == null)
                            {
                                nuevoPerro = new Perro(txtNombrePerro.Text, new Uri(txtHide.Text, UriKind.Relative), txtGenero.Text, txtRaza.Text, Convert.ToInt32(txtTamano.Text), Convert.ToDouble(txtPeso.Text), Convert.ToInt32(txtEdad.Text),
                                txtDatos.Text, Convert.ToBoolean(btnEsterilizado.IsChecked), Convert.ToBoolean(btnVacunado.IsChecked), Convert.ToBoolean(btnPPP.IsChecked), Convert.ToBoolean(btnPadrino.IsChecked), txtNombrePadrino.Text,
                                Convert.ToDouble(txtAportacion.Text), txtTelefono.Text);
                            }
                            else
                            {
                                nuevoPerro = new Perro(txtNombrePerro.Text, new Uri(fotografia, UriKind.Absolute), txtGenero.Text, txtRaza.Text, Convert.ToInt32(txtTamano.Text), Convert.ToDouble(txtPeso.Text), Convert.ToInt32(txtEdad.Text),
                                txtDatos.Text, Convert.ToBoolean(btnEsterilizado.IsChecked), Convert.ToBoolean(btnVacunado.IsChecked), Convert.ToBoolean(btnPPP.IsChecked), Convert.ToBoolean(btnPadrino.IsChecked), txtNombrePadrino.Text,
                                Convert.ToDouble(txtAportacion.Text), txtTelefono.Text);
                            }
                            listadoPerros.RemoveAt(lstPerros.SelectedIndex);
                            listadoPerros.Insert(lstPerros.SelectedIndex, nuevoPerro);
                            lstPerros.Items.Refresh();
                        }
                        
                    }
                    else
                    {
                        if (fotografia == null)
                        {
                            nuevoPerro = new Perro(txtNombrePerro.Text, new Uri(txtHide.Text, UriKind.Relative), txtGenero.Text, txtRaza.Text, Convert.ToInt32(txtTamano.Text), Convert.ToDouble(txtPeso.Text), Convert.ToInt32(txtEdad.Text),
                           txtDatos.Text, Convert.ToBoolean(btnEsterilizado.IsChecked), Convert.ToBoolean(btnVacunado.IsChecked), Convert.ToBoolean(btnPPP.IsChecked), Convert.ToBoolean(btnPadrino.IsChecked), "", 0.0, "");
                        }
                        else
                        {
                            nuevoPerro = new Perro(txtNombrePerro.Text, new Uri(fotografia, UriKind.Absolute), txtGenero.Text, txtRaza.Text, Convert.ToInt32(txtTamano.Text), Convert.ToDouble(txtPeso.Text), Convert.ToInt32(txtEdad.Text),
                           txtDatos.Text, Convert.ToBoolean(btnEsterilizado.IsChecked), Convert.ToBoolean(btnVacunado.IsChecked), Convert.ToBoolean(btnPPP.IsChecked), Convert.ToBoolean(btnPadrino.IsChecked), "", 0.0, "");
                        }
                        listadoPerros.RemoveAt(lstPerros.SelectedIndex);
                        listadoPerros.Insert(lstPerros.SelectedIndex, nuevoPerro);
                        lstPerros.Items.Refresh();
                    }
                
                }
                else
                {
                    MessageBox.Show("No se llevarán a cabo los cambios");
                }
            }
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
        private void Limpiar()
        {
            txtNombrePerro.Text = "";
            txtGenero.Text = "";
            txtRaza.Text = "";
            txtTamano.Text = "";
            txtPeso.Text = "";
            txtEdad.Text = "";
            txtDatos.Text = "";
            btnEsterilizado.IsChecked = false;
            btnVacunado.IsChecked = false;
            btnPPP.IsChecked = false;
            btnPadrino.IsChecked = false;
            txtNombrePadrino.Text = "";
            txtAportacion.Text = "";
            txtTelefono.Text = "";
            lstPerros.SelectedIndex = -1;
            btnEditar.IsEnabled = false;
            btnEliminar.IsEnabled = false;
            grpBoxPerro.Opacity = 1.0;
            grpBoxPadrino.Opacity = 0.7;
            txtNombrePerro.IsReadOnly = false;
            txtGenero.IsReadOnly = false;
            txtRaza.IsReadOnly = false;
            txtTamano.IsReadOnly = false;
            txtPeso.IsReadOnly = false;
            txtEdad.IsReadOnly = false;
            txtDatos.IsReadOnly = false;
            txtNombrePadrino.IsReadOnly = true;
            txtAportacion.IsReadOnly = true;
            txtTelefono.IsReadOnly = true;
            btnEsterilizado.IsEnabled = true;
            btnVacunado.IsEnabled = true;
            btnPPP.IsEnabled = true;
            btnPadrino.IsEnabled = true;
            btnAnadir.IsEnabled = true;
            btnFotoAnadir.IsEnabled = true;
            imgRelleno.Source = null;
        }

        private void btnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
            imgEditar.Source = imgEdit;
            txtEdit.Text = "Editar";
            contador = 0;
            btnEditar.IsEnabled = false;
            btnEliminar.IsEnabled = false;
        }
        private void btnAtras_Click(object sender, RoutedEventArgs e)
        {
            seleccion = new VentanaPrincipal(user, familiatxt,listadoPerros,listadoVoluntarios,listadoSocios);
            seleccion.Show();
            this.Hide();
        }
    }
}
