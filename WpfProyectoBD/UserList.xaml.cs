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

namespace WpfProyectoBD
{
    public partial class UserList : Window
    {
        private readonly string rutaArchLogin = "C:\\signupPrueba\\signupPrueba2.txt";

        public List<Usuario> ListaUsuarios { get; set; }

        public Usuario UsuarioSeleccionado { get; set; }

        public UserList()
        {
            InitializeComponent();
            ListaUsuarios = new List<Usuario>();
            CargarUsuarios();

            lvUsuarios.ItemsSource = ListaUsuarios;
            this.DataContext = this;
        }

        private void CargarUsuarios()
        {
            ListaUsuarios.Clear();
            if (File.Exists(rutaArchLogin))
            {
                try
                {
                    string[] lineas = File.ReadAllLines(rutaArchLogin);
                    foreach (string linea in lineas)
                    {
                        string[] partes = linea.Split('|');
                        if (partes.Length >= 3)
                        {
                            string nombre = partes[0];
                            string email = partes[1];
                            string contrasena = partes[2];

                            ListaUsuarios.Add(new Usuario(nombre, email, contrasena));
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al cargar los usuarios: {ex.Message}", "Error de Carga", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Archivo de usuarios no encontrado.", "Advertencia");
            }
        }

        private void GuardarUsuarios()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                foreach (Usuario u in ListaUsuarios)
                {
                    sb.AppendLine($"{u.Nombre}|{u.Email}|{u.Contrasena}");
                }

                File.WriteAllText(rutaArchLogin, sb.ToString(), Encoding.UTF8);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar los usuarios: {ex.Message}", "Error de Guardado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnREMOVERUSUARIO_Click(object sender, RoutedEventArgs e)
        {
            UsuarioSeleccionado = lvUsuarios.SelectedItem as Usuario;

            if (UsuarioSeleccionado != null)
            {
                MessageBoxResult result = MessageBox.Show($"¿Está seguro de que desea eliminar al usuario: {UsuarioSeleccionado.Nombre}?",
                                                          "Confirmar Eliminación", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    ListaUsuarios.Remove(UsuarioSeleccionado);
                    GuardarUsuarios();
                    lvUsuarios.Items.Refresh();
                    MessageBox.Show("Usuario eliminado exitosamente.", "Éxito");
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un usuario para eliminar.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void lvUsuarios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UsuarioSeleccionado = lvUsuarios.SelectedItem as Usuario;
        }
    }
}