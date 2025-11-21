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
    /// <summary>
    /// Lógica de interacción para Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }

        private readonly string rutaArchLogin = "C:\\signupPrueba\\signupPrueba2.txt";

        private void btnCREARCUENTA_Click(object sender, RoutedEventArgs e)
        {
            if (txtNOM.Text == "" || TxtEMAIL.Text == "" || pwdCONTRA.Password == "")
            {
                lblMensaje.Content = "Debe llenar todos los campos obligatorios >:(";
                lblMensaje.Foreground = Brushes.Red;
                return;
            }

            if (!Regex.IsMatch(TxtEMAIL.Text.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                lblMensaje.Content = "Ingrese una dirección de correo válida.";
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
