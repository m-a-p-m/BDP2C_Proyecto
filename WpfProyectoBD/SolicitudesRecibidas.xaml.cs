using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace WpfProyectoBD
{

    public partial class SolicitudesRecibidas : Window
    {
        private readonly string rutaArchSolicitudes = "C:\\signupPrueba\\solicitudes.txt";

        public SolicitudesRecibidas()
        {
            InitializeComponent();
            CargarSolicitudes();
        }

        private void CargarSolicitudes()
        {
            if (!File.Exists(rutaArchSolicitudes))
            {
                MessageBox.Show("El archivo de solicitudes no existe.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var listaSolicitudes = new List<SolicitudData>();

            try
            {
                string[] lineas = File.ReadAllLines(rutaArchSolicitudes);

                foreach (string linea in lineas)
                {
                    string[] partes = linea.Split('|');

                    if (partes.Length >= 2)
                    {
                        listaSolicitudes.Add(new SolicitudData
                        {
                            Nombre = partes[0],
                            Solicitud = partes[1].TrimEnd('\r', '\n')
                        });
                    }
                }

                lvSolicitudes.ItemsSource = listaSolicitudes;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al leer el archivo de solicitudes: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}