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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfProyectoBD
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private readonly string rutaArchLogin = "C:\\signupPrueba\\signupPrueba2.txt";

        private void btnENTRAR_Click(object sender, RoutedEventArgs e)
        {
            if (TxtEMAIL.Text == "" || pwdCONTRA.Password == "")
            {
                lblMensaje.Content = "Debe llenar todos los campos obligatorios >:(";
                lblMensaje.Foreground = Brushes.Red;
                return;
            }

            if (!TxtEMAIL.Text.Contains("@") || !TxtEMAIL.Text.Contains("."))
            {
                lblMensaje.Content = "Ingrese un correo electrónico válido :c.";
                lblMensaje.Foreground = Brushes.Red;
                return;
            }

            try
            {
                if (!File.Exists(rutaArchLogin))
                {
                    lblMensaje.Foreground = Brushes.Red;
                    lblMensaje.Content = "El archivo de usuarios no existe :C";
                    return;
                }

                string emailIngresado = TxtEMAIL.Text.Trim();
                string passwordIngresada = pwdCONTRA.Password;

                var contenidoArch = File.ReadAllLines(rutaArchLogin);
                bool encontrado = false;
                string nombreUsuario = string.Empty;

                foreach (var linea in contenidoArch)
                {
                    var partes = linea.Split('|');

                    if (partes.Length >= 3)
                    {
                        string emailGuardado = partes[1].Trim();
                        string passwordGuardada = partes[2].Trim();

                        if (emailGuardado.Equals(emailIngresado, StringComparison.OrdinalIgnoreCase) &&
                            passwordGuardada.Equals(passwordIngresada))
                        {
                            encontrado = true;
                            nombreUsuario = partes[0].Trim();
                            break;
                        }
                    }
                }

                if (encontrado)
                {
                    lblMensaje.Foreground = Brushes.Green;
                    lblMensaje.Content = "Acceso concedido. Bienvenido/a!";
                    Welcome winP = new Welcome();
                    winP.Show();
                    this.Close();
                }
                else
                {
                    lblMensaje.Foreground = Brushes.Red;
                    lblMensaje.Content = "USUARIO NO AUTORIZADO. Credenciales incorrectas. >:A";
                }
            }
            catch (Exception ex)
            {
                lblMensaje.Foreground = Brushes.Red;
                lblMensaje.Content = "Error al intentar iniciar sesión.";
                Console.WriteLine("ERROR: " + ex.Message);
            }
        }

        private void btnREGISTRARSE_Click(object sender, RoutedEventArgs e)
        {
            Register winSingUp = new Register();
            winSingUp.Show();
            this.Close();
        }

        private void btnVOLVER_Click(object sender, RoutedEventArgs e)
        {
            UserWindow usrWin = new UserWindow();
            usrWin.Show();
            this.Close();
        }
    }
}
