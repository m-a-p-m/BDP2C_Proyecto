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
using Path = System.IO.Path;

namespace WpfProyectoBD
{
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }

        private readonly string rutaArchLogin = "C:\\signupPrueba\\signup.txt";

        private bool CorreoExiste(string email) //Verifica si el correo que el usuario intente registrar, no este ya en el archivo de signup.
        {
            if (!File.Exists(rutaArchLogin))
            {
                return false;
            }

            try
            {
                string[] lineas = File.ReadAllLines(rutaArchLogin, Encoding.UTF8);

                foreach (string linea in lineas)
                {
                    string[] partes = linea.Split('|');

                    if (partes.Length >= 2)
                    {
                        string correoExistente = partes[1].Trim();

                        if (correoExistente.Equals(email.Trim(), StringComparison.OrdinalIgnoreCase))
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al leer el archivo de registros: {ex.Message}", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return false;
        }

        private void btnCREARCUENTA_Click(object sender, RoutedEventArgs e)
        {
            if (txtNOM.Text == "" || TxtEMAIL.Text == "" || pwdCONTRA.Password == "")
            {
                lblMensaje.Content = "Debe llenar todos los campos obligatorios.";
                lblMensaje.Foreground = Brushes.Red;
                return;
            }

            if (!Regex.IsMatch(txtNOM.Text.Trim(), @"^[\p{L}\s]+$"))
            {
                lblMensaje.Content = "El nombre solo puede contener letras y espacios.";
                lblMensaje.Foreground = Brushes.Red;
                return;
            }

            if (txtNOM.Text.Length <= 3)
            {
                lblMensaje.Content = "El nombre de usuario debe tener mas de 3 caracteres.";
                lblMensaje.Foreground= Brushes.Red;
                return;
            }

            if (pwdCONTRA.Password.Length <= 6)
            {
                lblMensaje.Content = "La contraseña debe tener más de 6 caracteres.";
                lblMensaje.Foreground = Brushes.Red;
                return;
            }

            if (!Regex.IsMatch(TxtEMAIL.Text.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                lblMensaje.Content = "Ingrese una dirección de correo válida.";
                lblMensaje.Foreground = Brushes.Red;
                return;
            }

            if (CorreoExiste(TxtEMAIL.Text))
            {
                lblMensaje.Content = "El correo electrónico ya está registrado.";
                lblMensaje.Foreground = Brushes.Red;
                return;
            }

            string datosParaGuardar = $"{txtNOM.Text}|{TxtEMAIL.Text}|{pwdCONTRA.Password}{Environment.NewLine}";

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(rutaArchLogin));
                File.AppendAllText(rutaArchLogin, datosParaGuardar, Encoding.UTF8);

                lblMensaje.Foreground = Brushes.Aqua;
                lblMensaje.Content = $"Bienvenido/a {txtNOM.Text}!!! Registro exitoso.";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar los datos: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnLIMPIAR_Click(object sender, RoutedEventArgs e)
        {
            txtNOM.Text = "";
            TxtEMAIL.Text = "";
            pwdCONTRA.Password = "";
        }

        private void btnVOLVER_Click(object sender, RoutedEventArgs e)
        {
            MainWindow login = new MainWindow();
            login.Show();
            this.Close();
        }
    }
}