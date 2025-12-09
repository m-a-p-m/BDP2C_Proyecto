using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using Path = System.IO.Path;

namespace WpfProyectoBD
{
    public partial class Welcome : Window
    {
        public ObservableCollection<Servicio> ListaServ { get; set; }
        public Servicio servSel { get; set; }

        private readonly string rutaArchLogin = "C:\\signupPrueba\\productos.txt";

        public Welcome()
        {
            InitializeComponent();
            ListaServ = new ObservableCollection<Servicio>();
            CargarServicios();
            DataContext = this;
            lstProd.ItemsSource = ListaServ;
        }

        private void CargarServicios()
        {
            ListaServ.Clear();
            if (File.Exists(rutaArchLogin))
            {
                try
                {
                    string[] lineas = File.ReadAllLines(rutaArchLogin);
                    foreach (string linea in lineas)
                    {
                        string[] partes = linea.Split('|');
                        if (partes.Length >= 5)
                        {
                            string nombre = partes[0];
                            string precioPuro = partes[1].Trim();
                            string categoria = partes[2];
                            string hora = partes[3];
                            string fecha = partes[4];

                            if (double.TryParse(precioPuro, out double precio))
                            {
                                Servicio nuevoServicio;

                                switch (categoria.ToUpper())
                                {
                                    case "EVENTO":
                                        nuevoServicio = new Eventos(nombre, precio, hora, fecha, categoria);
                                        break;
                                    case "CLASE":
                                        nuevoServicio = new Clases(nombre, precio, hora, fecha);
                                        break;
                                    default:
                                        nuevoServicio = new Servicio(nombre, precio, categoria, hora, fecha);
                                        break;
                                }
                                ListaServ.Add(nuevoServicio);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al cargar los servicios: {ex.Message}", "Error de Carga", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                try
                {
                    ListaServ.Add(new Clases("CLASE NATACION", 60.00, "10:00-11:00", "01/01/26"));
                    ListaServ.Add(new Eventos("FIESTA FIN DE AÑO", 150.00, "20:00-01:00", "31/12/25", "Evento"));
                    ListaServ.Add(new Servicio("SERVICIO ALEATORIO", 120.00, "SERVICIO", "14:00-16:00", "16/12/25"));

                    GuardarServicios();
                    MessageBox.Show("Archivo de servicios no encontrado. Se ha creado con datos de ejemplo (incluyendo clases y eventos).", "Información");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Advertencia: Archivo de servicios no encontrado, y hubo un error al intentar crearlo con datos de ejemplo: {ex.Message}", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void GuardarServicios()
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(rutaArchLogin));

                StringBuilder sb = new StringBuilder();
                foreach (Servicio s in ListaServ)
                {
                    sb.AppendLine($"{s.NomServ}|{s.PrecServ}|{s.CatServ}|{s.HoraServ}|{s.FechaServ}");
                }

                File.WriteAllText(rutaArchLogin, sb.ToString(), Encoding.UTF8);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar los servicios: {ex.Message}", "Error de Guardado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCERRAR_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnVISTAUSUARIO_Click(object sender, RoutedEventArgs e)
        {
            UserList usrLst = new UserList();
            usrLst.Show();
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (servSel != null)
            {
                try
                {
                    ListaServ.Remove(servSel);
                    GuardarServicios();

                    txtPrecio.Clear();
                    txtProducto.Clear();
                    txtHora.Clear();
                    txtFecha.Clear();
                    MessageBox.Show("Servicio eliminado exitosamente.", "Éxito");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al intentar eliminar el servicio: {ex.Message}", "Error de Eliminación", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un servicio para eliminar.", "Advertencia");
            }
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            string nombre = txtProducto.Text.Trim();
            string categoria = string.Empty;

                if (cmbCategoria.SelectedItem is ComboBoxItem item)
                {
                    categoria = item.Content.ToString();
                }
            string hora = txtHora.Text.Trim();
            string fecha = txtFecha.Text.Trim();

            if (!double.TryParse(txtPrecio.Text, out double prec) || prec <= 0)
            {
                MessageBox.Show("ERROR: El precio ingresado no es un número válido o debe ser mayor a cero.", "Error de Entrada");
                return;
            }

            if (string.IsNullOrEmpty(nombre))
            {
                MessageBox.Show("ERROR: El nombre del servicio no puede estar vacío.", "Error de Validación");
                return;
            }

            string patronHora = @"^([0-1][0-9]|2[0-3]):[0-5][0-9]-([0-1][0-9]|2[0-3]):[0-5][0-9]$";
            if (!Regex.IsMatch(hora, patronHora))
            {
                MessageBox.Show("ERROR: El formato de Hora debe ser HH:MM-HH:MM (ej: 20:00-22:00).", "Error de Validación de Hora");
                return;
            }

            string patronFecha = @"^\d{1,2}/\d{1,2}/\d{2}$";
            if (!Regex.IsMatch(fecha, patronFecha))
            {
                MessageBox.Show("ERROR: El formato de Fecha debe ser D/M/YY (ej: 2/2/25 o 15/12/25).", "Error de Validación de Fecha");
                return;
            }

            try
            {
                Servicio nuevoServicio;

                switch (categoria.ToUpper())
                {
                    case "EVENTO":
                        nuevoServicio = new Eventos(nombre, prec, hora, fecha, categoria);
                        break;
                    case "CLASE":
                        nuevoServicio = new Clases(nombre, prec, hora, fecha);
                        break;
                    default:
                        nuevoServicio = new Servicio(nombre, prec, categoria, hora, fecha);
                        break;
                }

                ListaServ.Add(nuevoServicio);
                GuardarServicios();

                txtPrecio.Clear();
                txtProducto.Clear();
                txtHora.Clear();
                txtFecha.Clear();
                MessageBox.Show($"Servicio de tipo {nuevoServicio.CatServ} agregado exitosamente.", "Éxito");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al intentar agregar el servicio: {ex.Message}", "Error de Adición", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (servSel == null)
            {
                MessageBox.Show("Debe seleccionar un servicio de la lista para editar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string nombre = txtProducto.Text.Trim();
            string categoria = string.Empty;
            if (cmbCategoria.SelectedItem is ComboBoxItem item)
            {
                categoria = item.Content.ToString();
            }
            string hora = txtHora.Text.Trim();
            string fecha = txtFecha.Text.Trim();

            if (!double.TryParse(txtPrecio.Text, out double prec) || prec <= 0)
            {
                MessageBox.Show("ERROR: El precio ingresado no es un número válido o debe ser mayor a cero.", "Error de Entrada");
                return;
            }

            if (string.IsNullOrEmpty(nombre))
            {
                MessageBox.Show("ERROR: El nombre del servicio no puede estar vacío.", "Error de Validación");
                return;
            }

            string patronHora = @"^([0-1][0-9]|2[0-3]):[0-5][0-9]-([0-1][0-9]|2[0-3]):[0-5][0-9]$";
            if (!Regex.IsMatch(hora, patronHora))
            {
                MessageBox.Show("ERROR: El formato de Hora debe ser HH:MM-HH:MM (ej: 20:00-22:00).", "Error de Validación de Hora");
                return;
            }

            string patronFecha = @"^\d{1,2}/\d{1,2}/\d{2}$";
            if (!Regex.IsMatch(fecha, patronFecha))
            {
                MessageBox.Show("ERROR: El formato de Fecha debe ser D/M/YY (ej: 2/2/25 o 15/12/25).", "Error de Validación de Fecha");
                return;
            }

            try
            {
                Servicio servicioEditado;

                switch (categoria.ToUpper())
                {
                    case "EVENTO":
                        servicioEditado = new Eventos(nombre, prec, hora, fecha, categoria);
                        break;
                    case "CLASE":
                        servicioEditado = new Clases(nombre, prec, hora, fecha);
                        break;
                    default:
                        servicioEditado = new Servicio(nombre, prec, categoria, hora, fecha);
                        break;
                }

                int index = ListaServ.IndexOf(servSel);
                if (index != -1)
                {
                    ListaServ[index] = servicioEditado;
                    servSel = ListaServ[index];
                }

                GuardarServicios();

                MessageBox.Show($"El servicio '{nombre}' ha sido editado correctamente.", "Edición Exitosa", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al intentar editar el servicio: {ex.Message}", "Error de Edición", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void lstProd_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            servSel = lstProd.SelectedItem as Servicio;

            if (servSel != null)
            {
                txtProducto.Text = servSel.NomServ;
                txtPrecio.Text = servSel.PrecServ.ToString();
                cmbCategoria.Text = servSel.CatServ;
                txtHora.Text = servSel.HoraServ;
                txtFecha.Text = servSel.FechaServ;
            }
        }

        private void btnSOLICITUDES_Click(object sender, RoutedEventArgs e)
        {
            SolicitudesRecibidas SolRec = new SolicitudesRecibidas();
            SolRec.Show();
        }
    }
}