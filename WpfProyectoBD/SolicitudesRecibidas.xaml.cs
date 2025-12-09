using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Path = System.IO.Path;

namespace WpfProyectoBD {

    public partial class SolicitudesRecibidas : Window
    {
        private readonly string rutaArchSolicitudes = "C:\\signupPrueba\\solicitudes.txt";
        private readonly string rutaArchRespuestas = "C:\\signupPrueba\\respuestas.txt";

        public ObservableCollection<SolicitudData> ListaSolicitudes { get; set; }

        public SolicitudData SolicitudSeleccionada { get; set; }

        public SolicitudesRecibidas()
        {
            InitializeComponent();
            ListaSolicitudes = new ObservableCollection<SolicitudData>();
            DataContext = this;
            CargarSolicitudes();
        }

        private void GuardarSolicitudesRestantes()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                foreach (SolicitudData s in ListaSolicitudes)
                {
                    sb.AppendLine($"{s.Nombre}|{s.Solicitud}");
                }

                File.WriteAllText(rutaArchSolicitudes, sb.ToString(), Encoding.UTF8);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar el archivo de solicitudes: {ex.Message}", "Error de Guardado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CargarSolicitudes()
        {
            if (!File.Exists(rutaArchSolicitudes))
            {
                MessageBox.Show("El archivo de solicitudes no existe.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            ListaSolicitudes.Clear();

            try
            {
                string[] lineas = File.ReadAllLines(rutaArchSolicitudes);

                foreach (string linea in lineas)
                {
                    string[] partes = linea.Split('|');

                    if (partes.Length >= 2)
                    {
                        ListaSolicitudes.Add(new SolicitudData
                        {
                            Nombre = partes[0],
                            Solicitud = partes[1].Trim()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al leer el archivo de solicitudes: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ResponderSolicitud(string respuesta)
        {
            if (SolicitudSeleccionada == null)
            {
                MessageBox.Show("Por favor, selecciona una solicitud para responder.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(respuesta))
            {
                MessageBox.Show("El campo de respuesta no puede estar vacío.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                string nombreSolicitante = SolicitudSeleccionada.Nombre;

                Directory.CreateDirectory(Path.GetDirectoryName(rutaArchRespuestas));

                string lineaEncabezado = $"{SolicitudSeleccionada.Nombre}, {SolicitudSeleccionada.Solicitud}";
                string lineaRespuesta = respuesta.Trim();

                string contenidoRegistro = lineaEncabezado + Environment.NewLine +
                                           lineaRespuesta + Environment.NewLine +
                                           Environment.NewLine;

                File.AppendAllText(rutaArchRespuestas, contenidoRegistro, Encoding.UTF8);

                ListaSolicitudes.Remove(SolicitudSeleccionada);
                GuardarSolicitudesRestantes();

                txtRespuesta.Clear();
                SolicitudSeleccionada = null;

                MessageBox.Show($"Respuesta enviada y solicitud de {nombreSolicitante} eliminada correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar la respuesta o eliminar la solicitud: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnResponder_Click(object sender, RoutedEventArgs e)
        {
            ResponderSolicitud(txtRespuesta.Text.Trim());
        }

        private void lvSolicitudes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}