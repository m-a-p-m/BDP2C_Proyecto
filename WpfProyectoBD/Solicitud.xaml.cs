using System;
using System.Collections.Generic;
using System.IO;
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
using Path = System.IO.Path;

namespace WpfProyectoBD
{
    /// <summary>
    /// Lógica de interacción para Solicitud.xaml
    /// </summary>
    public partial class Solicitud : Window
    {
        public Solicitud()
        {
            InitializeComponent();
        }

        private readonly string rutaArchLogin = "C:\\signupPrueba\\solicitudes.txt";

        private void btnENVSOLICITUD_Click(object sender, RoutedEventArgs e)
        {
            if (txtNombre.Text == "" || txtSolicitud.Text == "")
            {
                MessageBox.Show("Debe llenar todos los campos obligatorios.");
                return;
            }

            if (txtSolicitud.Text.Length <= 3)
            {
                MessageBox.Show("Su solicitud es demasiado corta.");
                return;
            }

            string datosParaGuardar = $"{txtNombre.Text}|{txtSolicitud.Text}{Environment.NewLine}";

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(rutaArchLogin));
                File.AppendAllText(rutaArchLogin, datosParaGuardar, Encoding.UTF8);

                MessageBox.Show ($"{txtNombre.Text}, su solicitud ha sido recibida, gracias.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar los datos: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnLMPSOLICITUD_Click(object sender, RoutedEventArgs e)
        {
            txtSolicitud.Text = "";
            txtNombre.Text = "";
        }
    }
}
