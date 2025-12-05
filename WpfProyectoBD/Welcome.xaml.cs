using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class Welcome : Window
    {
        public ObservableCollection<Producto> ListaProd { get; set; }
        public Producto prodSel { get; set; }

        private readonly string rutaArchLogin = "C:\\signupPrueba\\productos.txt";

        public Welcome()
        {
            InitializeComponent();

            ListaProd = new ObservableCollection<Producto>();
            CargarProductos();

            DataContext = this;
            lstProd.ItemsSource = ListaProd;
        }

        private void CargarProductos()
        {
            if (File.Exists(rutaArchLogin))
            {
                try
                {
                    string[] lineas = File.ReadAllLines(rutaArchLogin);
                    foreach (string linea in lineas)
                    {
                        string[] partes = linea.Split('|');
                        if (partes.Length == 3)
                        {
                            string nombre = partes[0];
                            string categoria = partes[2];
                            if (double.TryParse(partes[1], out double precio))
                            {
                                ListaProd.Add(new Producto(nombre, precio, categoria));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al cargar los productos: {ex.Message}", "Error de Carga", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                ListaProd.Add(new Producto("FRIGIDER", 3450.80, ""));
                ListaProd.Add(new Producto("REFRIGERADOR", 5300, ""));

                GuardarProductos();
            }
        }

        private void GuardarProductos()
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(rutaArchLogin));

                StringBuilder sb = new StringBuilder();
                foreach (Producto p in ListaProd)
                {
                    sb.AppendLine($"{p.NomProd}|{p.PrecProd}|{p.CatProd}");
                }

                File.WriteAllText(rutaArchLogin, sb.ToString(), Encoding.UTF8);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar los productos: {ex.Message}", "Error de Guardado", MessageBoxButton.OK, MessageBoxImage.Error);
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
            if (prodSel != null)
            {
                ListaProd.Remove(prodSel);
                GuardarProductos();

                txtPrecio.Clear();
                txtProducto.Clear();
                txtCategoria.Clear();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un producto para eliminar.", "Advertencia");
            }
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            string nombre = txtProducto.Text.Trim();
            string categoria = txtCategoria.Text.Trim();

            if (!double.TryParse(txtPrecio.Text, out double prec))
            {
                MessageBox.Show("ERROR: El precio ingresado no es un número válido.", "Error de Entrada");
                return;
            }

            if (prec > 0 && nombre != "")
            {
                ListaProd.Add(new Producto(nombre, prec, categoria));
                GuardarProductos();

                txtPrecio.Clear();
                txtProducto.Clear();
                txtCategoria.Clear();
            }
            else
            {
                MessageBox.Show("ERROR: EL PRECIO DEBE SER MAYOR A CERO, O EL NOMBRE ES VACIO. PONGA ALGO MAS BOL*** >:V", "Error de Validación");
            }
        }

        private void lstProd_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (prodSel != null)
            {
                txtProducto.Text = prodSel.NomProd;
                txtPrecio.Text = prodSel.PrecProd.ToString();
                txtCategoria.Text = prodSel.CatProd;
            }
        }

        private void btnSOLICITUDES_Click(object sender, RoutedEventArgs e)
        {
            SolicitudesRecibidas SolRec = new SolicitudesRecibidas();
            SolRec.Show();
        }
    }
}